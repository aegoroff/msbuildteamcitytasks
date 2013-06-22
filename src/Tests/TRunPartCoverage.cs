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
    public class TRunPartCoverage : TTask
    {
        private const string ValidPathToPartCover = @"C:\Program Files (x86)\Gubka Bob\PartCover .NET 2.3";

        private Mock<ITaskItem> item1;
        private Mock<ITaskItem> item2;
        private const string TargetArguments = "--help";
        private const string TargetWorkDir = ".";
        private RunPartCoverage task;


        [SetUp]
        public void ThisSetup()
        {
            Setup();
            item1 = Mockery.CreateMock<ITaskItem>();
            item2 = Mockery.CreateMock<ITaskItem>();
            task = new RunPartCoverage(Logger.MockObject);
        }

        [Test]
        public void ToolPath()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            task.ToolPath = ValidPathToPartCover;
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
            task.ToolPath = ValidPathToPartCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            Assert.That(task.Execute());
        }

        [Test]
        public void ToolPathAndTargetPathAndTargetArguments()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            task.ToolPath = ValidPathToPartCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            task.TargetArguments = TargetArguments;
            Assert.That(task.Execute());
        }

        [Test]
        public void ToolPathAndTargetPathAndTargetArgumentsAndTargetWorkDir()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            task.ToolPath = ValidPathToPartCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            task.TargetArguments = TargetArguments;
            task.TargetWorkDir = TargetWorkDir;
            Assert.That(task.Execute());
        }

        [Test]
        public void AllPropertiesExceptExcludes()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            item1.Expects.One.GetProperty(_ => _.ItemSpec).Will(Return.Value("a"));

            task.ToolPath = ValidPathToPartCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            task.TargetArguments = TargetArguments;
            task.TargetWorkDir = TargetWorkDir;
            task.Includes = new[] { item1.MockObject };
            Assert.That(task.Execute());
        }

        [Test]
        public void AllPropertiesExceptIncludes()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            item1.Expects.One.GetProperty(_ => _.ItemSpec).Will(Return.Value("a"));
            item2.Expects.One.GetProperty(_ => _.ItemSpec).Will(Return.Value("b"));

            task.ToolPath = ValidPathToPartCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            task.TargetArguments = TargetArguments;
            task.TargetWorkDir = TargetWorkDir;
            task.Excludes = new[] { item1.MockObject };
            Assert.That(task.Execute());
        }

        [Test]
        public void AllProperties()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            item1.Expects.One.GetProperty(_ => _.ItemSpec).Will(Return.Value("a"));
            item2.Expects.One.GetProperty(_ => _.ItemSpec).Will(Return.Value("b"));

            task.ToolPath = ValidPathToPartCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            task.TargetArguments = TargetArguments;
            task.TargetWorkDir = TargetWorkDir;
            task.Includes = new[] { item1.MockObject };
            task.Excludes = new[] { item2.MockObject };
            Assert.That(task.Execute());
        }

        [Test]
        public void ToolPathProperty()
        {
            task.ToolPath = ValidPathToPartCover;
            Assert.That(task.ToolPath, Is.EqualTo(ValidPathToPartCover));
        }

        [Test]
        public void TargetPathProperty()
        {
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            Assert.That(task.TargetPath, Is.EqualTo(TGoogleTestsRunner.CorrectExePath));
        }

        [Test]
        public void TargetArgumentsProperty()
        {
            task.TargetArguments = TargetArguments;
            Assert.That(task.TargetArguments, Is.EqualTo(TargetArguments));
        }

        [Test]
        public void TargetWorkDirProperty()
        {
            task.TargetWorkDir = TargetWorkDir;
            Assert.That(task.TargetWorkDir, Is.EqualTo(TargetWorkDir));
        }

        [Test]
        public void IncludesProperty()
        {
            task.Includes = new[] { item1.MockObject };
            Assert.That(task.Includes, Is.EquivalentTo(new[] { item1.MockObject }));
        }

        [Test]
        public void ExcludesProperty()
        {
            task.Excludes = new[] { item2.MockObject };
            Assert.That(task.Excludes, Is.EquivalentTo(new[] { item2.MockObject }));
        }
    }
}