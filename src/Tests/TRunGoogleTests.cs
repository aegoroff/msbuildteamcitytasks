/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2012 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks;
using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TRunGoogleTests : TTask
    {
        private const string TestFilter = "*";
        private const int ExecutionTimeoutMilliseconds = 200;
        private RunGoogleTests _task;

        [SetUp]
        public void Init()
        {
            _task = new RunGoogleTests(Logger);
        }

        [TearDown]
        public void ThisTeardown()
        {
            Teardown();
            TGoogleTestsRunner.DeleteResult();
        }

        [Test]
        public void OnlyRequired()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            Expect.Once.On(Logger).GetProperty(TGoogleTestsRunner.HasLoggedErrors).Will(Return.Value(false));

            _task.TestExePath = TGoogleTestsRunner.CorrectExePath;
            Assert.That(_task.Execute());
        }

        [Test]
        public void BadPath()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogError).WithAnyArguments();
            Expect.Once.On(Logger).GetProperty(TGoogleTestsRunner.HasLoggedErrors).Will(Return.Value(true));

            _task.TestExePath = "bad";
            Assert.That(_task.Execute(), Is.False);
        }

        [Test]
        public void ContinueOnFailures()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogError).WithAnyArguments();
            Expect.Once.On(Logger).GetProperty(TGoogleTestsRunner.HasLoggedErrors).Will(Return.Value(false));

            _task.TestExePath = "bad";
            _task.ContinueOnFailures = true;
            Assert.That(_task.Execute());
        }

        [Test]
        public void AllPropertiesSet()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            Expect.Once.On(Logger).GetProperty(TGoogleTestsRunner.HasLoggedErrors).Will(Return.Value(false));

            _task.TestExePath = TGoogleTestsRunner.CorrectExePath;
            _task.ContinueOnFailures = false;
            _task.RunDisabledTests = true;
            _task.CatchGtestExceptions = true;
            _task.TestFilter = TestFilter;
            _task.ExecutionTimeoutMilliseconds = ExecutionTimeoutMilliseconds;
            Assert.That(_task.Execute());
        }

        [Test]
        public void TestExePathProperty()
        {
            _task.TestExePath = TGoogleTestsRunner.CorrectExePath;
            Assert.That(_task.TestExePath, Is.EqualTo(TGoogleTestsRunner.CorrectExePath));
        }

        [Test]
        public void ContinueOnFailuresProperty()
        {
            _task.ContinueOnFailures = true;
            Assert.That(_task.ContinueOnFailures);
        }

        [Test]
        public void RunDisabledTestsProperty()
        {
            _task.RunDisabledTests = true;
            Assert.That(_task.RunDisabledTests);
        }

        [Test]
        public void CatchGtestExceptionsProperty()
        {
            _task.CatchGtestExceptions = true;
            Assert.That(_task.CatchGtestExceptions);
        }

        [Test]
        public void TestFilterProperty()
        {
            _task.TestFilter = TestFilter;
            Assert.That(_task.TestFilter, Is.EqualTo(TestFilter));
        }

        [Test]
        public void ExecutionTimeoutMillisecondsProperty()
        {
            _task.ExecutionTimeoutMilliseconds = ExecutionTimeoutMilliseconds;
            Assert.That(_task.ExecutionTimeoutMilliseconds, Is.EqualTo(ExecutionTimeoutMilliseconds));
        }
    }
}