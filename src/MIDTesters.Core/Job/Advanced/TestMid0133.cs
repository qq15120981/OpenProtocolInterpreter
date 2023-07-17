﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenProtocolInterpreter.Job.Advanced;

namespace MIDTesters.Job.Advanced
{
    [TestClass]
    [TestCategory("Job"), TestCategory("Advanced Job")]
    public class TestMid0133 : DefaultMidTests<Mid0133>
    {
        [TestMethod]
        [TestCategory("Revision 1"), TestCategory("ASCII")]
        public void Mid0133Revision1()
        {
            string package = "00200133            ";
            var mid = _midInterpreter.Parse(package);

            Assert.AreEqual(typeof(Mid0133), mid.GetType());
            AssertEqualPackages(package, mid, true);
        }

        [TestMethod]
        [TestCategory("Revision 1"), TestCategory("ByteArray")]
        public void Mid0133ByteRevision1()
        {
            string package = "00200133            ";
            byte[] bytes = GetAsciiBytes(package);
            var mid = _midInterpreter.Parse(bytes);

            Assert.AreEqual(typeof(Mid0133), mid.GetType());
            AssertEqualPackages(bytes, mid, true);
        }
    }
}
