// https://github.com/picrap/ASN.1-Serializer
// MIT License - feel free!

namespace Asn1.Serialization
{
    using System.IO;
    using System.Linq;
    using Types;

    public class BerSequenceSerializer : BerValueSerializer<Asn1Sequence>
    {
        public override int GetSerializedContentLength(Asn1Sequence value, BerSerializer serializer)
        {
            // content length is sum of all serialized lengths
            return value.Values.Sum(serializer.GetSerializedLength);
        }

        public override void SerializeContent(Asn1Sequence value, Stream outputStream, BerSerializer serializer)
        {
            foreach (var subValue in value.Values)
                serializer.Serialize(subValue, outputStream);
        }
    }
}