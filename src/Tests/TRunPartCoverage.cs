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


        [SetUp]
        public void ThisSetup()
        {
            Setup();
            _item1 = Mockery.NewMock<ITaskItem>();
            _item2 = Mockery.NewMock<ITaskItem>();
        }

        [Test]
        public void ToolPath()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           ToolPath = ValidPathToPartCover
                                       };

            Assert.That(task.Execute());
        }

        [Test]
        public void ToolPathInvalid()
        {
            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           ToolPath = "bad"
                                       };

            Assert.That(task.Execute(), Is.False);
        }

        [Test]
        public void ToolPathAndTargetPath()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           ToolPath = ValidPathToPartCover,
                                           TargetPath = TGoogleTestsRunner.CorrectExePath
                                       };

            Assert.That(task.Execute());
        }

        [Test]
        public void ToolPathAndTargetPathAndTargetArguments()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           ToolPath = ValidPathToPartCover,
                                           TargetPath = TGoogleTestsRunner.CorrectExePath,
                                           TargetArguments = TargetArguments
                                       };

            Assert.That(task.Execute());
        }

        [Test]
        public void ToolPathAndTargetPathAndTargetArgumentsAndTargetWorkDir()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           ToolPath = ValidPathToPartCover,
                                           TargetPath = TGoogleTestsRunner.CorrectExePath,
                                           TargetArguments = TargetArguments,
                                           TargetWorkDir = ".",
                                       };

            Assert.That(task.Execute());
        }

        [Test]
        public void AllPropertiesExceptExcludes()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            Expect.Once.On(_item1).GetProperty(ItemSpec).Will(Return.Value("a"));
            Expect.Once.On(_item2).GetProperty(ItemSpec).Will(Return.Value("b"));

            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           ToolPath = ValidPathToPartCover,
                                           TargetPath = TGoogleTestsRunner.CorrectExePath,
                                           TargetArguments = TargetArguments,
                                           TargetWorkDir = ".",
                                           Includes = new[] { _item1 }
                                       };

            Assert.That(task.Execute());
        }

        [Test]
        public void AllPropertiesExceptIncludes()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            Expect.Once.On(_item1).GetProperty(ItemSpec).Will(Return.Value("a"));
            Expect.Once.On(_item2).GetProperty(ItemSpec).Will(Return.Value("b"));

            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           ToolPath = ValidPathToPartCover,
                                           TargetPath = TGoogleTestsRunner.CorrectExePath,
                                           TargetArguments = TargetArguments,
                                           TargetWorkDir = ".",
                                           Excludes = new[] { _item2 }
                                       };

            Assert.That(task.Execute());
        }

        [Test]
        public void AllProperties()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            Expect.Once.On(_item1).GetProperty(ItemSpec).Will(Return.Value("a"));
            Expect.Once.On(_item2).GetProperty(ItemSpec).Will(Return.Value("b"));

            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           ToolPath = ValidPathToPartCover,
                                           TargetPath = TGoogleTestsRunner.CorrectExePath,
                                           TargetArguments = TargetArguments,
                                           TargetWorkDir = ".",
                                           Includes = new[] { _item1 },
                                           Excludes = new[] { _item2 }
                                       };

            Assert.That(task.Execute());
        }

        [Test]
        public void ToolPathProperty()
        {
            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           ToolPath = ValidPathToPartCover,
                                       };

            Assert.That(task.ToolPath, Is.EqualTo(ValidPathToPartCover));
        }

        [Test]
        public void TargetPathProperty()
        {
            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           TargetPath = TGoogleTestsRunner.CorrectExePath,
                                       };

            Assert.That(task.TargetPath, Is.EqualTo(TGoogleTestsRunner.CorrectExePath));
        }

        [Test]
        public void TargetArgumentsProperty()
        {
            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           TargetArguments = TargetArguments,
                                       };

            Assert.That(task.TargetArguments, Is.EqualTo(TargetArguments));
        }

        [Test]
        public void TargetWorkDirProperty()
        {
            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           TargetWorkDir = ".",
                                       };

            Assert.That(task.TargetWorkDir, Is.EqualTo("."));
        }

        [Test]
        public void IncludesProperty()
        {
            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           Includes = new[] { _item1 }
                                       };

            Assert.That(task.Includes, Is.EquivalentTo(new[] { _item1 }));
        }

        [Test]
        public void ExcludesProperty()
        {
            RunPartCoverage task = new RunPartCoverage(Logger)
                                       {
                                           Excludes = new[] { _item2 }
                                       };

            Assert.That(task.Excludes, Is.EquivalentTo(new[] { _item2 }));
        }
    }
}