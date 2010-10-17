/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2010 Alexander Egorov
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

        private ITaskItem _item1;
        private ITaskItem _item2;
        private const string ItemSpec = "ItemSpec";
        private const string TargetArguments = "--help";
        private const string TargetWorkDir = ".";
        private RunPartCoverage _task;


        [SetUp]
        public void ThisSetup()
        {
            Setup();
            _item1 = Mockery.NewMock<ITaskItem>();
            _item2 = Mockery.NewMock<ITaskItem>();
            _task = new RunPartCoverage(Logger);
        }

        [Test]
        public void ToolPath()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            _task.ToolPath = ValidPathToPartCover;
            Assert.That(_task.Execute());
        }

        [Test]
        public void ToolPathInvalid()
        {
            _task.ToolPath = "bad";
            Assert.That(_task.Execute(), Is.False);
        }

        [Test]
        public void ToolPathAndTargetPath()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            _task.ToolPath = ValidPathToPartCover;
            _task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            Assert.That(_task.Execute());
        }

        [Test]
        public void ToolPathAndTargetPathAndTargetArguments()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            _task.ToolPath = ValidPathToPartCover;
            _task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            _task.TargetArguments = TargetArguments;
            Assert.That(_task.Execute());
        }

        [Test]
        public void ToolPathAndTargetPathAndTargetArgumentsAndTargetWorkDir()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            _task.ToolPath = ValidPathToPartCover;
            _task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            _task.TargetArguments = TargetArguments;
            _task.TargetWorkDir = TargetWorkDir;
            Assert.That(_task.Execute());
        }

        [Test]
        public void AllPropertiesExceptExcludes()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            Expect.Once.On(_item1).GetProperty(ItemSpec).Will(Return.Value("a"));
            Expect.Once.On(_item2).GetProperty(ItemSpec).Will(Return.Value("b"));

            _task.ToolPath = ValidPathToPartCover;
            _task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            _task.TargetArguments = TargetArguments;
            _task.TargetWorkDir = TargetWorkDir;
            _task.Includes = new[] { _item1 };
            Assert.That(_task.Execute());
        }

        [Test]
        public void AllPropertiesExceptIncludes()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            Expect.Once.On(_item1).GetProperty(ItemSpec).Will(Return.Value("a"));
            Expect.Once.On(_item2).GetProperty(ItemSpec).Will(Return.Value("b"));

            _task.ToolPath = ValidPathToPartCover;
            _task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            _task.TargetArguments = TargetArguments;
            _task.TargetWorkDir = TargetWorkDir;
            _task.Excludes = new[] { _item1 };
            Assert.That(_task.Execute());
        }

        [Test]
        public void AllProperties()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            Expect.Once.On(_item1).GetProperty(ItemSpec).Will(Return.Value("a"));
            Expect.Once.On(_item2).GetProperty(ItemSpec).Will(Return.Value("b"));

            _task.ToolPath = ValidPathToPartCover;
            _task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            _task.TargetArguments = TargetArguments;
            _task.TargetWorkDir = TargetWorkDir;
            _task.Excludes = new[] { _item1 };
            _task.Excludes = new[] { _item2 };
            Assert.That(_task.Execute());
        }

        [Test]
        public void ToolPathProperty()
        {
            _task.ToolPath = ValidPathToPartCover;
            Assert.That(_task.ToolPath, Is.EqualTo(ValidPathToPartCover));
        }

        [Test]
        public void TargetPathProperty()
        {
            _task.TargetPath = TGoogleTestsRunner.CorrectExePath;
            Assert.That(_task.TargetPath, Is.EqualTo(TGoogleTestsRunner.CorrectExePath));
        }

        [Test]
        public void TargetArgumentsProperty()
        {
            _task.TargetArguments = TargetArguments;
            Assert.That(_task.TargetArguments, Is.EqualTo(TargetArguments));
        }

        [Test]
        public void TargetWorkDirProperty()
        {
            _task.TargetWorkDir = TargetWorkDir;
            Assert.That(_task.TargetWorkDir, Is.EqualTo(TargetWorkDir));
        }

        [Test]
        public void IncludesProperty()
        {
            _task.Includes = new[] { _item1 };
            Assert.That(_task.Includes, Is.EquivalentTo(new[] { _item1 }));
        }

        [Test]
        public void ExcludesProperty()
        {
            _task.Excludes = new[] { _item2 };
            Assert.That(_task.Excludes, Is.EquivalentTo(new[] { _item2 }));
        }
    }
}