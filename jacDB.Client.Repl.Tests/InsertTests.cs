using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace jacDB.Client.Repl.Tests
{
    [TestClass]
    public class InsertTests
    {
        private ReplService service;
        private StreamWriter input;
        private StreamReader output;

        [TestInitialize]
        public void Initialize()
        {
            input = new StreamWriter(new MemoryStream());
            input.AutoFlush = true;

            output = new StreamReader(new MemoryStream());

            service = new ReplService(input.BaseStream, output.BaseStream);
        }

        [TestCleanup]
        public void CleanUp()
        {
            service = null;
            input.Close();
            input.Dispose();

            output.Close();
            output.Dispose();
        }

        [TestMethod]
        public void Basic()
        {
            // arrange
            input.WriteLine("insert 1 user1 person1@example.com");
            input.WriteLine("select");
            input.WriteLine(".exit");
            input.BaseStream.Position = 0;

            // act 
            service.RunLoop();
            output.BaseStream.Position = 0;

            // assert
            Assert.AreEqual("jacDB> Executed.", output.ReadLine());
            Assert.AreEqual("jacDB> (1, user1, person1@example.com)", output.ReadLine());
            Assert.AreEqual("Executed.", output.ReadLine());
            Assert.AreEqual("jacDB> ", output.ReadLine());
        }

        [TestMethod]
        public void InsertUntilFull()
        {
            // arrange
            for (var i = 0; i < 1401; i++)
            {
                input.WriteLine($"insert {i} user{i} user{i}@example.com");
            }
            input.WriteLine(".exit");
            input.BaseStream.Position = 0;

            // act
            service.RunLoop();
            output.BaseStream.Position = 0;

            // assert
            var lines = output.ReadToEnd().Split("\r\n");
            Assert.AreEqual("jacDB> Error: Table Full.", lines.SkipLast(1).Last());
        }

        [TestMethod]
        public void InsertMaxLengthFields()
        {
            // arrange
            var username = new string(Enumerable.Range(0, 32).Select(s => 'a').ToArray());
            var email = new string(Enumerable.Range(0, 255).Select(s => 'b').ToArray());
            input.WriteLine($"insert 1 {username} {email}");
            input.WriteLine(".exit");
            input.BaseStream.Position = 0;

            // act
            service.RunLoop();
            output.BaseStream.Position = 0;

            // assert
            Assert.AreEqual("jacDB> Executed.", output.ReadLine());
            Assert.AreEqual("jacDB> ", output.ReadLine());
        }

        [TestMethod]
        public void InsertBeyondLengthFields()
        {
            // arrange
            var username = new string(Enumerable.Range(0, 33).Select(s => 'a').ToArray());
            var email = new string(Enumerable.Range(0, 256).Select(s => 'b').ToArray());
            input.WriteLine($"insert 1 {username} {email}");
            input.WriteLine(".exit");
            input.BaseStream.Position = 0;

            // act
            service.RunLoop();
            output.BaseStream.Position = 0;

            // assert
            Assert.AreEqual("jacDB> String is too long", output.ReadLine());
            Assert.AreEqual("jacDB> ", output.ReadLine());
        }
    }
}
