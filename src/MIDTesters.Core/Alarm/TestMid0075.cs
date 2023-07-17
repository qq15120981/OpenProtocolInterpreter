﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenProtocolInterpreter.Alarm;

namespace MIDTesters.Alarm
{
    [TestClass]
    [TestCategory("Alarm")]
    public class TestMid0075 : DefaultMidTests<Mid0075>
    {
        [TestMethod]
        [TestCategory("ASCII")]
        public void Mid0075AllRevisions()
        {
            string pack = @"00200075001         ";
            var mid = _midInterpreter.Parse(pack);

            Assert.AreEqual(typeof(Mid0075), mid.GetType());
            AssertEqualPackages(pack, mid);
        }

        [TestMethod]
        [TestCategory("ByteArray")]
        public void Mid0075ByteAllRevisions()
        {
            string pack = @"00200075001         ";
            byte[] bytes = GetAsciiBytes(pack);
            var mid = _midInterpreter.Parse(bytes);

            Assert.AreEqual(typeof(Mid0075), mid.GetType());
            AssertEqualPackages(bytes, mid);
        }
    }
}
