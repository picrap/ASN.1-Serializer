// https://github.com/picrap/ASN.1-Serializer
// MIT License - feel free!

namespace Asn1.Tests
{
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Serialization;
    using Types;

    [TestClass]
    public class BerSerializationTests
    {
        [TestMethod]
        public void SerializeBooleanTest()
        {
            var b = new Asn1Boolean(true);
            var s = new BerSerializer();
            using (var m = new MemoryStream())
            {
                s.Serialize(b, m);
                var mb = m.ToArray();
                Assert.IsTrue(new byte[] { 1, 1, 1 }.SequenceEqual(mb));
            }
        }

        [TestMethod]
        public void DeserializeBooleanTest()
        {
            var s = new BerSerializer();
            using (var m = new MemoryStream(new byte[]{1,1,0}))
            {
                var b=s.Serialize()
            }
        }
    }
}