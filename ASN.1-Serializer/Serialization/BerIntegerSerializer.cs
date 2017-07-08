// https://github.com/picrap/ASN.1-Serializer
// MIT License - feel free!

namespace Asn1.Serialization
{
    using System;
    using System.IO;
    using Types;

    public class BerIntegerSerializer : BerValueSerializer<Asn1Integer>
    {
        public override int GetSerializedContentLength(Asn1Integer value, BerSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void SerializeContent(Asn1Integer value, Stream outputStream, BerSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}