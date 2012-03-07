/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2012 Alexander Egorov
 */

using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks;
using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TRunPartCoverage : TTask
    {
        private const string ValidPathToPartCover = @"C:\Program Files\Gubka Bob\PartCover .NET 2.3";

        private ITaskItem item1;
        private ITaskItem item2;
        private const string ItemSpec = "ItemSpec";
        private const string TargetArguments = "--help";
        private const string TargetWorkDir = ".";
        private RunPartCoverage task;


        [SetUp]
        public void ThisSetup()
        {
            Setup();
            item1 = Mockery.NewMock<ITaskItem>();
            item2 = Mockery.NewMock<ITaskItem>();
            task = new RunPartCoverage(Logger);
        }

        [Test]
        public void ToolPath()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            task.ToolPath = ValidPathToPartCover;
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
            task.ToolPath = ValidPathToPartCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            Assert.That(task.Execute());
        }

        [Test]
        public void ToolPathAndTargetPathAndTargetArguments()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            task.ToolPath = ValidPathToPartCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            task.TargetArguments = TargetArguments;
            Assert.That(task.Execute());
        }

        [Test]
        public void ToolPathAndTargetPathAndTargetArgumentsAndTargetWorkDir()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            task.ToolPath = ValidPathToPartCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            task.TargetArguments = TargetArguments;
            task.TargetWorkDir = TargetWorkDir;
            Assert.That(task.Execute());
        }

        [Test]
        public void AllPropertiesExceptExcludes()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            Expect.Once.On(item1).GetProperty(ItemSpec).Will(Return.Value("a"));

            task.ToolPath = ValidPathToPartCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            task.TargetArguments = TargetArguments;
            task.TargetWorkDir = TargetWorkDir;
            task.Includes = new[] { item1 };
            Assert.That(task.Execute());
        }

        [Test]
        public void AllPropertiesExceptIncludes()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            Expect.Once.On(item1).GetProperty(ItemSpec).Will(Return.Value("a"));
            Expect.Once.On(item2).GetProperty(ItemSpec).Will(Return.Value("b"));

            task.ToolPath = ValidPathToPartCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            task.TargetArguments = TargetArguments;
            task.TargetWorkDir = TargetWorkDir;
            task.Excludes = new[] { item1 };
            Assert.That(task.Execute());
        }

        [Test]
        public void AllProperties()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            Expect.Once.On(item1).GetProperty(ItemSpec).Will(Return.Value("a"));
            Expect.Once.On(item2).GetProperty(ItemSpec).Will(Return.Value("b"));

            task.ToolPath = ValidPathToPartCover;
            task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            task.TargetArguments = TargetArguments;
            task.TargetWorkDir = TargetWorkDir;
            task.Includes = new[] { item1 };
            task.Excludes = new[] { item2 };
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
            task.Includes = new[] { item1 };
            Assert.That(task.Includes, Is.EquivalentTo(new[] { item1 }));
        }

        [Test]
        public void ExcludesProperty()
        {
            task.Excludes = new[] { item2 };
            Assert.That(task.Excludes, Is.EquivalentTo(new[] { item2 }));
        }
    }
}