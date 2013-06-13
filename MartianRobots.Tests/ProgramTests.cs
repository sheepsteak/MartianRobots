using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace MartianRobots.Tests
{
    [TestClass]
    public class ProgramShould
    {
        private string filename;

        [TestInitialize]
        public void GivenAValidFile()
        {
            this.filename = "test.txt";
            var fileContents =
                "5 3\r\n" +
                "1 1 E\r\n" +
                "RFRFRFRF\r\n" +
                "3 2 N\r\n" +
                "FRRFLLFFRRFLL\r\n" +
                "0 3 W\r\n" +
                "LLFFFLFLFL\r\n";

            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "test.txt"), fileContents);
        }

        [TestMethod]
        public void WhenExecutedThenReturnCorrectOutput()
        {
            var existingOut = Console.Out;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            Program.Main(new string[] { "test.txt" });

            var expectedOutput =
                "1 1 E\r\n" +
                "3 3 N LOST\r\n" +
                "2 3 S\r\n";

            Assert.AreEqual(expectedOutput, stringWriter.ToString());

            Console.SetOut(existingOut);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(this.filename))
            {
                File.Delete(this.filename);
            }
        }
    }
}
