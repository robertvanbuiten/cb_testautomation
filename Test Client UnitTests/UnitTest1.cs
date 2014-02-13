using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreBank
{
    [TestClass]
    public class ConfigNetworkTest
    {
        [TestMethod]
        public void ConnectionSettings()
        {
            ConnectionSettings conn = new CoreBank.ConnectionSettings();
            Paths paths = new Paths();

            Framework.Factory();
            Framework.Init(conn, paths);

            Assert.AreEqual(Framework.Connection.Repository, "NETWORK");

        }

        [TestMethod]
        public void Connect()
        {
            ConnectionSettings conn = new CoreBank.ConnectionSettings();
            Paths paths = new Paths();

            Framework.Factory();
            Framework.Init(conn, paths);

            Assert.IsTrue(Framework.Ready);
        }

        [TestMethod]
        public void ConnectWithNetwork()
        {
            ConnectionSettings conn = new CoreBank.ConnectionSettings();
            Paths paths = new Paths();
                   
            Framework.Factory();
            Framework.Init(conn, paths);
            Framework.Start();

            Assert.IsTrue(Framework.Ready);
        }

         [TestMethod]
        public void GetConfig()
        {
            ConnectionSettings conn = new CoreBank.ConnectionSettings();
            Paths paths = new Paths();
                       
            Framework.Factory();
            Framework.Init(conn, paths);
            Framework.Start();

            Assert.IsTrue(Framework.GetConfig());

        }
    }
}
