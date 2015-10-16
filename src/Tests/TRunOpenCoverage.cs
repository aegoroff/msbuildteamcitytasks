/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using System.IO;
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
        private const string NUnitPath = @"..\..\..\packages\NUnit.Runners.2.6.4\tools\nunit-console-x86.exe";
        private Mock<ITaskItem> item1;
        private Mock<ITaskItem> item2;
        private const string TargetWorkDir = ".";

        protected override void AfterSetup()
        {
            this.item1 = this.Mockery.CreateMock<ITaskItem>();
            this.item2 = this.Mockery.CreateMock<ITaskItem>();
            this.task = new RunOpenCoverage(this.Logger.MockObject);
        }

        [Test]
        public void RealRun()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.Normal, null)).WithAnyArguments();
            this.Logger.Expects.Exactly(9).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            this.item1.Expects.Exactly(2).GetProperty(_ => _.ItemSpec).Will(Return.Value("+[MSBuild.TeamCity.Tasks]*ImportData"));
            this.item2.Expects.Exactly(2).GetProperty(_ => _.ItemSpec).Will(Return.Value("-[System]*"));

            this.task.ToolPath = ValidPathToOpenCover;
            this.task.TargetPath = NUnitPath;
            var path = new Uri(this.GetType().Assembly.CodeBase).LocalPath;
            this.task.TargetArguments = $"/nologo /noshadow {path} /framework:net-4.0 /run=TGoogleTestArgumentsBuilder";
            this.task.TargetWorkDir = TargetWorkDir;
            this.task.ExcludeByfile = "*.Gen.cs";
            this.task.HideSkipped = "All";
            this.task.SkipAutoProps = true;
            this.task.XmlReportPath = Path.Combine(Path.GetDirectoryName(path), "opencover.xml");

            this.task.Filter = new[] { this.item1.MockObject, this.item2.MockObject };
            Assert.That(this.task.Execute());
        }

        [Test]
        public void RealRunNoReport()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.Normal, null)).WithAnyArguments();
            this.Logger.Expects.Exactly(9).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            this.item1.Expects.Exactly(2).GetProperty(_ => _.ItemSpec).Will(Return.Value("+[MSBuild.TeamCity.Tasks]*ImportData"));
            this.item2.Expects.Exactly(2).GetProperty(_ => _.ItemSpec).Will(Return.Value("-[System]*"));

            this.task.ToolPath = ValidPathToOpenCover;
            this.task.TargetPath = NUnitPath;
            var path = new Uri(this.GetType().Assembly.CodeBase).LocalPath;
            this.task.TargetArguments = $"/nologo /noshadow {path} /framework:net-4.0 /run=TGoogleTestArgumentsBuilder";
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