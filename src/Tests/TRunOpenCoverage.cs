/*
 * Created by: egr
 * Created at: 08.09.2010
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
    public class TRunOpenCoverage : TTask
    {
        #region Constants and Fields

        private RunOpenCoverage task;
        private const string ValidPathToOpenCover = @"C:\Program Files (x86)\OpenCover";
        private Mock<ITaskItem> item1;
        private Mock<ITaskItem> item2;
        private const string TargetArguments = "--help";
        private const string TargetWorkDir = ".";

        #endregion

        #region Public Methods and Operators

        [SetUp]
        public void ThisSetup()
        {
            Setup();
            item1 = Mockery.CreateMock<ITaskItem>();
            item2 = Mockery.CreateMock<ITaskItem>();
            task = new RunOpenCoverage(Logger.MockObject);
        }

        [Test]
        public void ToolPath()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            task.ToolPath = ValidPathToOpenCover;
            Assert.That(task.Execute());
        }

        [Test]
        public void ToolPathInvalid()
        {
            Logger.Expects.One.Method(_ => _.LogErrorFromException(null, true)).WithAnyArguments();
            task.ToolPath = "bad";
            Assert.That(task.Execute(), Is.False);
        }

        [Test]
        public void ToolPathAndTargetPath()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            task.ToolPath = ValidPathToOpenCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            Assert.That(task.Execute());
        }

        [Test]
        public void FilterProperty()
        {
            task.Filter = new[] { item1.MockObject };
            Assert.That(task.Filter, Is.EquivalentTo(new[] { item1.MockObject }));
        }

        [Test]
        public void AllProperties()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            item1.Expects.Exactly(2).GetProperty(_ => _.ItemSpec).Will(Return.Value("a"));
            item2.Expects.Exactly(2).GetProperty(_ => _.ItemSpec).Will(Return.Value("b"));

            task.ToolPath = ValidPathToOpenCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            task.TargetArguments = TargetArguments;
            task.TargetWorkDir = TargetWorkDir;
            task.Filter = new[] { item1.MockObject, item2.MockObject };
            Assert.That(task.Execute());
        }

        #endregion
    }
}