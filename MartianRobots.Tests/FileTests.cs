using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace MartianRobots.Tests
{
    [TestClass]
    public class FileTests
    {
        [TestMethod]
        public void GivesInstructionsIfNoInputFileProvided()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            Program.Main(new string[] { });

            Assert.AreEqual("Please provide an input file.\r\n", stringWriter.ToString());
        }

        [TestMethod]
        public void GivesInstructionsIfInputFileDoesNotExist()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            Program.Main(new string[] { "fakeFile.txt" });

            Assert.AreEqual("Please provide a valid input file.\r\n", stringWriter.ToString());
        }

        [TestMethod]
        public void GivesInstructionsIfInputFileIsEmpty()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "test.txt"), string.Empty);
            Program.Main(new string[] { "test.txt" });

            File.Delete(Path.Combine(Environment.CurrentDirectory, "test.txt"));
            Assert.AreEqual("File is empty.\r\n", stringWriter.ToString());
        }
    }
}
