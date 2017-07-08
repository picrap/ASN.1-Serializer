// https://github.com/picrap/ASN.1-Serializer
// MIT License - feel free!

namespace Asn1.Types
{
    /// <summary>
    /// Base type for all values
    /// All values MUST be immutable
    /// </summary>
    public abstract class Asn1Value
    {
        /// <summary>
        /// Gets the type (literal, such as "BOOLEAN")
        /// </summary>
        public abstract string Type { get; }

        /// <summary>
        /// Gets the tag (such as 1)
        /// </summary>
        public abstract int Tag { get; }

        /// <summary>
        /// Clones the value
        /// </summary>
        /// <returns></returns>
        public abstract Asn1Value Clone();

        /// <summary>
        /// Replaces the old value with new value, if it exists in hierarchy
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public virtual Asn1Value Replace(Asn1Value oldValue, Asn1Value newValue)
        {
            if (oldValue == this)
                return newValue;
            return this;
        }
    }
}
