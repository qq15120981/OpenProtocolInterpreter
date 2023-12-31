﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenProtocolInterpreter;
using System;

namespace MIDTesters
{
    public class DefaultMidTests<TMid> : MidTester where TMid : Mid
    {
        [TestMethod]
        [TestCategory("Default")]
        public void HasParameterlessConstructor()
        {
            var constructor = typeof(TMid).GetConstructor(new Type[] { });
            Assert.IsTrue(constructor != null);
        }

        [TestMethod]
        [TestCategory("Default")]
        public void HasHeaderParameterConstructor()
        {
            var constructor = typeof(TMid).GetConstructor(new Type[] { typeof(Header) });
            Assert.IsTrue(constructor != null);
        }
    }
}
