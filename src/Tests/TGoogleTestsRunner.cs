/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System;
using System.IO;
using System.Linq;
using MSBuild.TeamCity.Tasks.Internal;
using Microsoft.Build.Framework;
using NMock;
using NUnit.Framework;
using ILogger = MSBuild.TeamCity.Tasks.ILogger;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TGoogleTestsRunner
    {
        internal static readonly string CorrectExePath = Environment.CurrentDirectory + @"\..\..\..\External\_tst.exe";

        internal const string HasLoggedErrors = "HasLoggedErrors";

        private MockFactory mockery;
        private Mock<ILogger> logger;

        [SetUp]
        public void Setup()
        {
            mockery = new MockFactory();
            logger = mockery.CreateMock<ILogger>();
        }

        [TearDown]
        public void TearDown()
        {
            DeleteResult();
        }

        internal static void DeleteResult()
        {
            string xmlPath = TestResultPath;

            if (File.Exists(xmlPath))
            {
                File.Delete(xmlPath);
            }
        }

        private static string TestResultPath
        {
            get
            {
                string file = Path.GetFileNameWithoutExtension(CorrectExePath);
                string dir = Path.GetDirectoryName(Path.GetFullPath(CorrectExePath));
                return dir + @"\" + file + ".xml";
            }
        }

        [Test]
        public void CorrectExe()
        {
            logger.Expects.One.GetProperty(_=>_.HasLoggedErrors).Will(Return.Value(false));

            GoogleTestsRunner runner = new GoogleTestsRunner(logger.MockObject, false, CorrectExePath);

            Assert.That(runner.Import());
            Assert.That(runner.Messages.Count, Is.EqualTo(1));
        }

        [Test]
        public void CorrectExeWithExistingFile()
        {
            logger.Expects.Exactly(2).GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            GoogleTestsRunner runner = new GoogleTestsRunner(logger.MockObject, false, CorrectExePath);
            runner.Import();

            Assert.That(File.Exists(TestResultPath));
            Assert.That(runner.Import());
            Assert.That(runner.Messages.Count, Is.EqualTo(1));
        }

        [Test]
        public void CorrectExeUsingExecutionTimeout()
        {
            logger.Expects.One.GetProperty(_=>_.HasLoggedErrors).Will(Return.Value(false));

            GoogleTestsRunner runner = new GoogleTestsRunner(logger.MockObject, false, CorrectExePath)
                                           { ExecutionTimeoutMilliseconds = 200 };
            Assert.That(runner.Import());
            Assert.That(runner.Messages.Count, Is.EqualTo(1));
        }

        [Test]
        public void IncorrectExe()
        {
            logger.Expects.One.Method(_ => _.LogErrorFromException(null, true)).WithAnyArguments();
            logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(true));

            GoogleTestsRunner runner = new GoogleTestsRunner(logger.MockObject, false, "bad");

            Assert.That(runner.Import(), Is.False);
            Assert.That(runner.Messages, Is.Empty);
        }
    }
}