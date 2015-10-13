/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2013 Alexander Egorov
 */

using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks;
using NMock;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TRunOpenCoverage : TTask
    {
        private RunOpenCoverage task;
        private const string ValidPathToOpenCover = @"..\..\..\packages\OpenCover.4.6.166\tools";
        private Mock<ITaskItem> item1;
        private Mock<ITaskItem> item2;
        private const string TargetArguments = "--help";
        private const string TargetWorkDir = ".";

        protected override void AfterSetup()
        {
            this.item1 = this.Mockery.CreateMock<ITaskItem>();
            this.item2 = this.Mockery.CreateMock<ITaskItem>();
            this.task = new RunOpenCoverage(this.Logger.MockObject);
        }

        [Test]
        public void AllProperties()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            this.item1.Expects.Exactly(2).GetProperty(_ => _.ItemSpec).Will(Return.Value("a"));
            this.item2.Expects.Exactly(2).GetProperty(_ => _.ItemSpec).Will(Return.Value("b"));

            this.task.ToolPath = ValidPathToOpenCover;
            this.task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            this.task.TargetArguments = TargetArguments;
            this.task.TargetWorkDir = TargetWorkDir;
            this.task.ExcludeByfile = "*.Gen.cs";
            this.task.HideSkipped = "All";
            this.task.SkipAutoProps = true;
            this.task.Filter = new[] { this.item1.MockObject, this.item2.MockObject };
            Assert.That(this.task.Execute());
        }

        [Test]
        public void FilterProperty()
        {
            this.task.Filter = new[] { this.item1.MockObject };
            Assert.That(this.task.Filter, Is.EquivalentTo(new[] { this.item1.MockObject }));
        }

        [Test]
        public void ToolPath()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.task.ToolPath = ValidPathToOpenCover;
            Assert.That(this.task.Execute());
        }

        [Test]
        public void ToolPathAndTargetPath()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.task.ToolPath = ValidPathToOpenCover;
            this.task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            Assert.That(this.task.Execute());
        }

        [Test]
        public void ToolPathInvalid()
        {
            this.Logger.Expects.One.Method(_ => _.LogErrorFromException(null, true)).WithAnyArguments();
            this.task.ToolPath = "bad";
            Assert.That(this.task.Execute(), Is.False);
        }
    }
}