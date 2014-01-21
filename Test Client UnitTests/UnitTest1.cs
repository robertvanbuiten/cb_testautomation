using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBank
{
    [TestClass]
    public class ConfigNetworkTest
    {
        [TestMethod]
        public void Connect()
        {
            Framework.Factory();
            Framework.Start();

        }
    }
}
