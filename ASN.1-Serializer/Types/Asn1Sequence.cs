// https://github.com/picrap/ASN.1-Serializer
// MIT License - feel free!

namespace Asn1.Types
{
    public class Asn1Sequence : Asn1Value
    {
        public const string TypeValue = "SEQUENCE";
        public const int TagValue = 16;

        public override string Type => TypeValue;
        public override int Tag => TagValue;

        public Asn1Value[] Values { get; }

        public Asn1Sequence( params  Asn1Value [] values)
        {
            Values = values;
        }

        public override Asn1Value Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}