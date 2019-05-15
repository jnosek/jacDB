using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace jacDB.Client.Repl.Tests
{
    [TestClass]
    public class StorageTests : BaseTest
    {
        [TestMethod]
        public void KeepDataAfterClosing()
        {
            // arrange

            // PHASE 1: INSERT
            Input.WriteLine("insert 1 user1 person1@example.com");
            Input.WriteLine(".exit");
            // PHASE 2: READ
            Input.WriteLine("select");
            Input.WriteLine(".exit");
            Input.BaseStream.Position = 0;

            // act 
            RunCommandLoop();
            RunCommandLoop();

            // assert
            Output.BaseStream.Position = 0;
            Assert.AreEqual("jacDB> Executed.", Output.ReadLine());
            Assert.AreEqual("jacDB> ", Output.ReadLine());
            Assert.AreEqual("jacDB> (1, user1, person1@example.com)", Output.ReadLine());
            Assert.AreEqual("Executed.", Output.ReadLine());
            Assert.AreEqual("jacDB> ", Output.ReadLine());
        }
    }
}
