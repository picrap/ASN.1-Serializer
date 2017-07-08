// https://github.com/picrap/ASN.1-Serializer
// MIT License - feel free!

namespace Asn1.Types
{
    public class Asn1Integer : Asn1Value
    {
        public const string TypeValue = "INTEGER";
        public const int TagValue = 2;

        public override string Type => TypeValue;
        public override int Tag => TagValue;

        public int Value { get; }

        public Asn1Integer(int value)
        {
            Value = value;
        }

        public override Asn1Value Clone()
        {
            return new Asn1Integer(Value);
        }
    }
}