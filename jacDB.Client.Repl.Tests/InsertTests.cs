using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace jacDB.Client.Repl.Tests
{
    [TestClass]
    public class InsertTests : BaseTest
    {
        [TestMethod]
        public void Basic()
        {
            // arrange
            Input.WriteLine("insert 1 user1 person1@example.com");
            Input.WriteLine("select");
            Input.WriteLine(".exit");
            Input.BaseStream.Position = 0;

            // act 
            RunCommandLoop();
            Output.BaseStream.Position = 0;

            // assert
            Assert.AreEqual("jacDB> Executed.", Output.ReadLine());
            Assert.AreEqual("jacDB> (1, user1, person1@example.com)", Output.ReadLine());
            Assert.AreEqual("Executed.", Output.ReadLine());
            Assert.AreEqual("jacDB> ", Output.ReadLine());
        }

        [TestMethod]
        public void InsertUntilFull()
        {
            // arrange
            for (var i = 0; i < 1401; i++)
            {
                Input.WriteLine($"insert {i} user{i} user{i}@example.com");
            }
            Input.WriteLine(".exit");
            Input.BaseStream.Position = 0;

            // act
            RunCommandLoop();
            Output.BaseStream.Position = 0;

            // assert
            var lines = Output.ReadToEnd().Split("\r\n");
            Assert.AreEqual("jacDB> Error: Table Full.", lines.SkipLast(2).Last());
        }

        [TestMethod]
        public void InsertMaxLengthFields()
        {
            // arrange
            var username = new string(Enumerable.Range(0, 32).Select(s => 'a').ToArray());
            var email = new string(Enumerable.Range(0, 255).Select(s => 'b').ToArray());
            Input.WriteLine($"insert 1 {username} {email}");
            Input.WriteLine(".exit");
            Input.BaseStream.Position = 0;

            // act
            RunCommandLoop();
            Output.BaseStream.Position = 0;

            // assert
            Assert.AreEqual("jacDB> Executed.", Output.ReadLine());
            Assert.AreEqual("jacDB> ", Output.ReadLine());
        }

        [TestMethod]
        public void InsertBeyondLengthFields()
        {
            // arrange
            var username = new string(Enumerable.Range(0, 33).Select(s => 'a').ToArray());
            var email = new string(Enumerable.Range(0, 256).Select(s => 'b').ToArray());
            Input.WriteLine($"insert 1 {username} {email}");
            Input.WriteLine(".exit");
            Input.BaseStream.Position = 0;

            // act
            RunCommandLoop();
            Output.BaseStream.Position = 0;

            // assert
            Assert.AreEqual("jacDB> String is too long", Output.ReadLine());
            Assert.AreEqual("jacDB> ", Output.ReadLine());
        }

        [TestMethod]
        public void InsertNegativeId()
        {
            // arrange
            Input.WriteLine($"insert -1 abc def");
            Input.WriteLine(".exit");
            Input.BaseStream.Position = 0;

            // act
            RunCommandLoop();
            Output.BaseStream.Position = 0;

            // assert
            Assert.AreEqual("jacDB> ID must be positive", Output.ReadLine());
            Assert.AreEqual("jacDB> ", Output.ReadLine());
        }
    }
}
