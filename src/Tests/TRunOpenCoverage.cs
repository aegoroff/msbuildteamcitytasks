/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2012 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks;
using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TRunOpenCoverage : TTask
    {
        #region Constants and Fields

        private RunOpenCoverage task;
        private const string ValidPathToOpenCover = @"C:\Program Files (x86)\OpenCover";

        #endregion

        #region Public Methods and Operators

        [SetUp]
        public void ThisSetup()
        {
            Setup();
            task = new RunOpenCoverage(Logger);
        }

        [Test]
        public void ToolPath()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            task.ToolPath = ValidPathToOpenCover;
            Assert.That(task.Execute());
        }

        [Test]
        public void ToolPathInvalid()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogError).WithAnyArguments();
            task.ToolPath = "bad";
            Assert.That(task.Execute(), Is.False);
        }

        [Test]
        public void ToolPathAndTargetPath()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            task.ToolPath = ValidPathToOpenCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            Assert.That(task.Execute());
        }

        #endregion
    }
}