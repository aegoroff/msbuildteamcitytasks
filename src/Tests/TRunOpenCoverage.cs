/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2012 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks;
using Microsoft.Build.Framework;
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
        private ITaskItem item1;
        private ITaskItem item2;
        private const string ItemSpec = "ItemSpec";
        private const string TargetArguments = "--help";
        private const string TargetWorkDir = ".";

        #endregion

        #region Public Methods and Operators

        [SetUp]
        public void ThisSetup()
        {
            Setup();
            item1 = Mockery.NewMock<ITaskItem>();
            item2 = Mockery.NewMock<ITaskItem>();
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

        [Test]
        public void FilterProperty()
        {
            task.Filter = new[] { item1 };
            Assert.That(task.Filter, Is.EquivalentTo(new[] { item1 }));
        }

        [Test]
        public void AllProperties()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            Expect.Once.On(item1).GetProperty(ItemSpec).Will(Return.Value("a"));
            Expect.Once.On(item2).GetProperty(ItemSpec).Will(Return.Value("b"));

            task.ToolPath = ValidPathToOpenCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            task.TargetArguments = TargetArguments;
            task.TargetWorkDir = TargetWorkDir;
            task.Filter = new[] { item1, item2 };
            Assert.That(task.Execute());
        }

        #endregion
    }
}