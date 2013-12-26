/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2013 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks;
using Microsoft.Build.Framework;
using NMock;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TRunGoogleTests : TTask
    {
        private const string TestFilter = "*";
        private const int ExecutionTimeoutMilliseconds = 200;
        private RunGoogleTests task;

        [SetUp]
        public void Init()
        {
            task = new RunGoogleTests(Logger.MockObject);
        }

        protected override void AfterTeardown()
        {
            TGoogleTestsRunner.DeleteResult();
        }

        [Test]
        public void OnlyRequired()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            Logger.Expects.One.GetProperty(_=>_.HasLoggedErrors).Will(Return.Value(false));

            task.TestExePath = TGoogleTestsRunner.CorrectExePath;
            Assert.That(task.Execute());
        }

        [Test]
        public void BadPath()
        {
            Logger.Expects.One.Method(_ => _.LogErrorFromException(null, true)).WithAnyArguments();
            Logger.Expects.One.GetProperty(_=>_.HasLoggedErrors).Will(Return.Value(true));

            task.TestExePath = "bad";
            Assert.That(task.Execute(), Is.False);
        }

        [Test]
        public void ContinueOnFailures()
        {
            Logger.Expects.One.Method(_ => _.LogErrorFromException(null, true)).WithAnyArguments();
            Logger.Expects.One.GetProperty(_=>_.HasLoggedErrors).Will(Return.Value(false));

            task.TestExePath = "bad";
            task.ContinueOnFailures = true;
            Assert.That(task.Execute());
        }

        [Test]
        public void AllPropertiesSet()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            Logger.Expects.One.GetProperty(_=>_.HasLoggedErrors).Will(Return.Value(false));

            task.TestExePath = TGoogleTestsRunner.CorrectExePath;
            task.ContinueOnFailures = false;
            task.RunDisabledTests = true;
            task.CatchGtestExceptions = true;
            task.TestFilter = TestFilter;
            task.ExecutionTimeoutMilliseconds = ExecutionTimeoutMilliseconds;
            task.Verbose = true;
            task.WhenNoDataPublished = "error";
            Assert.That(task.Execute());
        }

        [Test]
        public void TestExePathProperty()
        {
            task.TestExePath = TGoogleTestsRunner.CorrectExePath;
            Assert.That(task.TestExePath, Is.EqualTo(TGoogleTestsRunner.CorrectExePath));
        }

        [Test]
        public void ContinueOnFailuresProperty()
        {
            task.ContinueOnFailures = true;
            Assert.That(task.ContinueOnFailures);
        }

        [Test]
        public void RunDisabledTestsProperty()
        {
            task.RunDisabledTests = true;
            Assert.That(task.RunDisabledTests);
        }

        [Test]
        public void CatchGtestExceptionsProperty()
        {
            task.CatchGtestExceptions = true;
            Assert.That(task.CatchGtestExceptions);
        }

        [Test]
        public void TestFilterProperty()
        {
            task.TestFilter = TestFilter;
            Assert.That(task.TestFilter, Is.EqualTo(TestFilter));
        }

        [Test]
        public void ExecutionTimeoutMillisecondsProperty()
        {
            task.ExecutionTimeoutMilliseconds = ExecutionTimeoutMilliseconds;
            Assert.That(task.ExecutionTimeoutMilliseconds, Is.EqualTo(ExecutionTimeoutMilliseconds));
        }
    }
}