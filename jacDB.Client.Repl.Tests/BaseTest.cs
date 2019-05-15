using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace jacDB.Client.Repl.Tests
{
    public class BaseTest
    {
        private ReplService service;
        protected StreamWriter Input;
        protected StreamReader Output;

        [TestInitialize]
        public void Initialize()
        {
            Input = new StreamWriter(new MemoryStream());
            Input.AutoFlush = true;

            Output = new StreamReader(new MemoryStream());

            service = new ReplService("test.jacdb", Input.BaseStream, Output.BaseStream);
        }

        [TestCleanup]
        public void CleanUp()
        {
            service = null;
            Input.Close();
            Input.Dispose();

            Output.Close();
            Output.Dispose();

            File.Delete("test.jacdb");
        }

        protected void RunCommandLoop()
        {
            service.RunLoop();
        }
    }
}
