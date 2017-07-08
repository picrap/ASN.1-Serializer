// https://github.com/picrap/ASN.1-Serializer
// MIT License - feel free!

namespace Asn1.Types
{
    public class Asn1Boolean : Asn1Value
    {
        public const string TypeValue = "BOOLEAN";
        public const int TagValue = 1;

        public override string Type => TypeValue;
        public override int Tag => TagValue;

        public bool Value { get; }

        public Asn1Boolean(bool value)
        {
            Value = value;
        }

        public override Asn1Value Clone()
        {
            return new Asn1Boolean(Value);
        }
    }
}