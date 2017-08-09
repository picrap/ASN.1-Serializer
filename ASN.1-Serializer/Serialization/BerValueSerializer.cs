// https://github.com/picrap/ASN.1-Serializer
// MIT License - feel free!

namespace Asn1.Serialization
{
    using System.IO;
    using Types;

    public abstract class BerValueSerializer
    {
        public abstract int GetSerializedContentLength(Asn1Value value, BerSerializer serializer);

        public abstract void SerializeContent(Asn1Value value, Stream outputStream, BerSerializer serializer);
    }

    public abstract class BerValueSerializer<TAsn1Value> : BerValueSerializer
        where TAsn1Value : Asn1Value
    {
        public sealed override int GetSerializedContentLength(Asn1Value value, BerSerializer serializer)
        {
            return GetSerializedContentLength((TAsn1Value)value, serializer);
        }

        public sealed override void SerializeContent(Asn1Value value, Stream outputStream, BerSerializer serializer)
        {
            SerializeContent((TAsn1Value)value, outputStream, serializer);
        }

        public abstract int GetSerializedContentLength(TAsn1Value value, BerSerializer serializer);

        public abstract void SerializeContent(TAsn1Value value, Stream outputStream, BerSerializer serializer);
    }
}