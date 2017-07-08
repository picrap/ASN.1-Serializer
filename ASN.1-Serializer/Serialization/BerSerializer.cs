// https://github.com/picrap/ASN.1-Serializer
// MIT License - feel free!

namespace Asn1.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Types;

    public class BerSerializer
    {
        private readonly IDictionary<Type, BerValueSerializer> _serializersByType = new Dictionary<Type, BerValueSerializer>();
        private readonly IDictionary<int, BerValueSerializer> _serializersByTag = new Dictionary<int, BerValueSerializer>();

        public BerSerializer()
        {
            RegisterSerializer(Asn1Boolean.TagValue, new BerBooleanSerializer());
            RegisterSerializer(Asn1Integer.TagValue, new BerIntegerSerializer());
            RegisterSerializer(Asn1Sequence.TagValue, new BerSequenceSerializer());
        }

        public void RegisterSerializer<TAsn1Value>(int tag, BerValueSerializer<TAsn1Value> valueSerializer)
            where TAsn1Value : Asn1Value
        {
            RegisterSerializer(typeof(TAsn1Value), tag, valueSerializer);
        }

        public void RegisterSerializer(Type asn1ValueType, int tag, BerValueSerializer valueSerializer)
        {
            _serializersByType[asn1ValueType] = valueSerializer;
            _serializersByTag[tag] = valueSerializer;
        }

        public int GetSerializedLength(Asn1Value value)
        {
            // full length is serialized tag length + serialized length length + serialized content length
            var contentLength = GetSerializer(value).GetSerializedContentLength(value, this);
            return GetTagBytes(value.Tag).Length + GetLengthBytes(contentLength).Length + contentLength;
        }

        /// <summary>
        /// Serializes the specified value to output <see cref="Stream"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="outputStream">The output stream.</param>
        public void Serialize(Asn1Value value, Stream outputStream)
        {
            var serializer = GetSerializer(value);
            var tagBytes = GetTagBytes(value.Tag);
            var contentLength = GetLengthBytes(serializer.GetSerializedContentLength(value, this));
            outputStream.Write(tagBytes, 0, tagBytes.Length);
            outputStream.Write(contentLength, 0, contentLength.Length);
            serializer.SerializeContent(value, outputStream, this);
        }

        protected virtual BerValueSerializer GetSerializer(Asn1Value value)
        {
            _serializersByType.TryGetValue(value.GetType(), out var serializer);
            return serializer;
        }

        protected virtual BerValueSerializer GetSerializer(int tag)
        {
            _serializersByTag.TryGetValue(tag, out var serializer);
            return serializer;
        }

        public byte[] GetTagBytes(int tag)
        {
            if (tag <= 30)
                return GetLowTagBytes(tag);
            return GetHighTagBytes(tag);
        }

        private byte[] GetLowTagBytes(int tag)
        {
            return new[] { (byte)tag };
        }

        private byte[] GetHighTagBytes(int tag)
        {
            var b = new List<byte> { 0b0011_1110 }; // bits 5-1 are set to 1
            for (;;)
            {
                if (tag < 128)
                {
                    b.Insert(1, (byte)tag);
                    break;
                }
                b.Insert(1, (byte)(tag & 0x7F | 0x80));
                tag >>= 7;
            }
            return b.ToArray();
        }

        public byte[] GetLengthBytes(int length)
        {
            if (length < 128)
                return GetShortLengthBytes(length);
            return GetLongLengthBytes(length);
        }

        private byte[] GetShortLengthBytes(int length)
        {
            return new[] { (byte)length };
        }

        private byte[] GetLongLengthBytes(int length)
        {
            var b = new List<byte>();
            do
            {
                b.Insert(0, (byte)(length & 0xFF));
                length >>= 8;
            } while (length > 0);
            b.Insert(0, (byte)(b.Count | 0x80));
            return b.ToArray();
        }
    }
}
