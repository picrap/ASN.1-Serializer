// https://github.com/picrap/ASN.1-Serializer
// MIT License - feel free!

namespace Asn1.Serialization
{
    using System.IO;
    using Types;

    public class BerBooleanSerializer : BerValueSerializer<Asn1Boolean>
    {
        public override int GetSerializedContentLength(Asn1Boolean value, BerSerializer serializer)
        {
            return 1;
        }

        public override void SerializeContent(Asn1Boolean value, Stream outputStream, BerSerializer serializer)
        {
            outputStream.WriteByte(value.Value ? (byte)1 : (byte)0);
        }
    }
}