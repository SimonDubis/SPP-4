using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using TestsGenerator;
using TestsGeneratorLib;
using TestsGeneratorLib.FileElements;

namespace TestProject
{
    public class Tests
    {
        StreamReader reader;
        [SetUp]
        public void Setup()
        {
            reader = new StreamReader("C:\\Users\\Asus\\Visual Studio\\TestsGenerator\\Example\\FirstFile.cs");
        }

        [Test]
        public void FileReadingTest()
        {
            string code = reader.ReadToEnd();
            Assert.NotNull(code);
        }

        [Test]
        public void CodeParserTest()
        {
            FileElement fileElement = null;
            string code = reader.ReadToEnd();
            CodeParser codeParser = new CodeParser();
            fileElement = codeParser.GetFileElement(code);
            Assert.NotNull(fileElement);
        }

        [Test]
        public void TestGeneratorTest()
        {
            Dictionary<string, string> tests;
            FileElement fileInfo = null;
            string code = reader.ReadToEnd();
            CodeParser codeParser = new CodeParser();
            fileInfo = codeParser.GetFileElement(code);
            Generator generator = new Generator();
            tests = generator.GenerateTests(fileInfo);
            Assert.IsNotEmpty(tests);
        }


        [Test]
        public void ClassCountTest()
        {
            CodeParser _codeParser = new CodeParser();
            string[] firstClassStringArray = File.ReadAllLines("C:\\Users\\Asus\\Visual Studio\\TestsGenerator\\Example\\FirstFile.cs");
            string _firstClassString = String.Join("\n", firstClassStringArray);
            FileElement fileElement = _codeParser.GetFileElement(_firstClassString);
            Console.WriteLine(_firstClassString);
            Assert.AreEqual(2, fileElement.Classes.Count);
        }

        [Test]
        public void CheckCountOfTests()
        {
            Generator _testGenerator = new Generator();
            CodeParser _codeParser = new CodeParser();
            string[] firstClassStringArray = File.ReadAllLines("C:\\Users\\Asus\\Visual Studio\\TestsGenerator\\Example\\FirstFile.cs");
            string _firstClassString = String.Join("\n", firstClassStringArray);
            FileElement fileElement = _codeParser.GetFileElement(_firstClassString);
            Dictionary<string, string> tests = _testGenerator.GenerateTests(fileElement);
            Assert.AreEqual(2, tests.Count);
        }
    }
}