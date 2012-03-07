/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2012 Alexander Egorov
 */

using System;
using System.IO;
using MSBuild.TeamCity.Tasks;
using MSBuild.TeamCity.Tasks.Internal;
using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TGoogleTestsRunner
    {
        internal static readonly string CorrectExePath = Environment.CurrentDirectory + @"\..\..\..\External\_tst.exe";

        internal const string HasLoggedErrors = "HasLoggedErrors";

        private Mockery mockery;
        private ILogger logger;

        [SetUp]
        public void Setup()
        {
            mockery = new Mockery();
            logger = mockery.NewMock<ILogger>();
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
            Expect.Once.On(logger).GetProperty(HasLoggedErrors).Will(Return.Value(false));

            GoogleTestsRunner runner = new GoogleTestsRunner(logger, false, CorrectExePath);
            ExecutionResult result = runner.Import();

            Assert.That(result.Status);
            Assert.That(result.Messages.Count, Is.EqualTo(1));
        }

        [Test]
        public void CorrectExeWithExistingFile()
        {
            Expect.Exactly(2).On(logger).GetProperty(HasLoggedErrors).Will(Return.Value(false));

            GoogleTestsRunner runner = new GoogleTestsRunner(logger, false, CorrectExePath);
            runner.Import();

            Assert.That(File.Exists(TestResultPath));

            ExecutionResult result = runner.Import();

            Assert.That(result.Status);
            Assert.That(result.Messages.Count, Is.EqualTo(1));
        }

        [Test]
        public void CorrectExeUsingExecutionTimeout()
        {
            Expect.Once.On(logger).GetProperty(HasLoggedErrors).Will(Return.Value(false));

            GoogleTestsRunner runner = new GoogleTestsRunner(logger, false, CorrectExePath)
                                           { ExecutionTimeoutMilliseconds = 200 };
            ExecutionResult result = runner.Import();

            Assert.That(result.Status);
            Assert.That(result.Messages.Count, Is.EqualTo(1));
        }

        [Test]
        public void IncorrectExe()
        {
            Expect.Once.On(logger).GetProperty(HasLoggedErrors).Will(Return.Value(true));
            Expect.Once.On(logger).Method(TTeamCityTaskImplementation.LogError).WithAnyArguments();

            GoogleTestsRunner runner = new GoogleTestsRunner(logger, false, "bad");
            ExecutionResult result = runner.Import();

            Assert.That(result.Status, Is.False);
            Assert.That(result.Messages, Is.Null);
        }
    }
}