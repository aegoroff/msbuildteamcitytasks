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
    public class TSimpleTasks : TTask
    {
        [Test]
        public void CommonPropertiesTest()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            BlockOpen task = new BlockOpen(Logger)
                                 {
                                     Name = "n",
                                     FlowId = "1",
                                     IsAddTimestamp = true
                                 };
            Assert.That(task.Execute());
        }

        [Test]
        public void BlockTaskNameProperty()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            BlockOpen task = new BlockOpen(Logger)
                                 {
                                     Name = "n"
                                 };
            Assert.That(task.Name, Is.EqualTo("n"));
        }

        [Test]
        public void BlockOpen()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            BlockOpen task = new BlockOpen(Logger)
                                 {
                                     Name = "n"
                                 };
            Assert.That(task.Execute());
        }

        [Test]
        public void BlockClose()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            BlockClose task = new BlockClose(Logger)
                                  {
                                      Name = "n"
                                  };
            Assert.That(task.Execute());
        }

        [Test]
        public void BuildNumber()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            BuildNumber task = new BuildNumber(Logger)
                                   {
                                       Number = "1"
                                   };
            Assert.That(task.Execute());
            Assert.That(task.Number, Is.EqualTo("1"));
        }

        [Test]
        public void BuildProgressStart()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            BuildProgressStart task = new BuildProgressStart(Logger)
                                          {
                                              Message = "m"
                                          };
            Assert.That(task.Execute());
            Assert.That(task.Message, Is.EqualTo("m"));
        }

        [Test]
        public void BuildProgressMessage()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            BuildProgressMessage task = new BuildProgressMessage(Logger)
                                            {
                                                Message = "m"
                                            };
            Assert.That(task.Execute());
            Assert.That(task.Message, Is.EqualTo("m"));
        }

        [Test]
        public void BuildProgressFinish()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            BuildProgressFinish task = new BuildProgressFinish(Logger)
                                           {
                                               Message = "m"
                                           };
            Assert.That(task.Execute());
            Assert.That(task.Message, Is.EqualTo("m"));
        }

        [Test]
        public void BuildStatus()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            BuildStatus task = new BuildStatus(Logger)
                                   {
                                       Status = "SUCCESS",
                                       Text = "t"
                                   };
            Assert.That(task.Execute());
        }

        [Test]
        public void ReportMessage()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            ReportMessage task = new ReportMessage(Logger)
                                     {
                                         Status = "WARNING",
                                         Text = "t",
                                         ErrorDetails = "ed"
                                     };
            Assert.That(task.Execute());
            Assert.That(task.Status, Is.EqualTo("WARNING"));
            Assert.That(task.Text, Is.EqualTo("t"));
            Assert.That(task.ErrorDetails, Is.EqualTo("ed"));
        }

        [Test]
        public void ReportBuildStatistic()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            ReportBuildStatistic task = new ReportBuildStatistic(Logger)
                                            {
                                                Key = "k",
                                                Value = 1
                                            };
            Assert.That(task.Execute());
            Assert.That(task.Key, Is.EqualTo("k"));
            Assert.That(task.Value, Is.EqualTo(1));
        }

        [Test]
        public void ImportData()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            ImportData task = new ImportData(Logger)
                                  {
                                      Path = "p",
                                      Type = "dotNetCoverage",
                                      Tool = "ncover",
                                  };
            Assert.That(task.Execute());
            Assert.That(task.Path, Is.EqualTo("p"));
            Assert.That(task.Type, Is.EqualTo("dotNetCoverage"));
            Assert.That(task.Tool, Is.EqualTo("ncover"));
        }

        [Test]
        public void ImportGoogleTests()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            Expect.Once.On(Logger).GetProperty(TGoogleTestsRunner.HasLoggedErrors).Will(Return.Value(false));

            ImportGoogleTests task = new ImportGoogleTests(Logger)
                                         {
                                             ContinueOnFailures = true,
                                             TestResultsPath = TGoogleTestsPlainImporter.SuccessTestsPath,
                                         };
            Assert.That(task.Execute());
            Assert.That(task.ContinueOnFailures);
            Assert.That(task.TestResultsPath, Is.EqualTo(TGoogleTestsPlainImporter.SuccessTestsPath));
        }

        [Test]
        public void EnableServiceMessages()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            EnableServiceMessages task = new EnableServiceMessages(Logger);
            Assert.That(task.Execute());
        }

        [Test]
        public void DisableServiceMessages()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            DisableServiceMessages task = new DisableServiceMessages(Logger);
            Assert.That(task.Execute());
        }

        [Test]
        public void CompilationStarted()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            CompilationStarted task = new CompilationStarted(Logger);
            Assert.That(task.Execute());
        }

        [Test]
        public void CompilationFinished()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            CompilationFinished task = new CompilationFinished(Logger);
            Assert.That(task.Execute());
        }

        [Test]
        public void TestSuiteStarted()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            TestSuiteStarted task = new TestSuiteStarted(Logger)
                                        {
                                            Name = "n"
                                        };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestSuiteFinished()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            TestSuiteFinished task = new TestSuiteFinished(Logger)
                                         {
                                             Name = "n"
                                         };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestStarted()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            TestStarted task = new TestStarted(Logger)
                                   {
                                       Name = "n"
                                   };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestStartedCaptureStandardOutput()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            TestStarted task = new TestStarted(Logger)
                                   {
                                       Name = "n",
                                       CaptureStandardOutput = true
                                   };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestFinished()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            TestFinished task = new TestFinished(Logger)
                                    {
                                        Name = "n",
                                        Duration = 3.0
                                    };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestIgnored()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            TestIgnored task = new TestIgnored(Logger)
                                   {
                                       Name = "n",
                                       Message = "Comment"
                                   };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestStdOut()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            TestStdOut task = new TestStdOut(Logger)
                                  {
                                      Name = "n",
                                      Out = "out"
                                  };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestStdErr()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            TestStdErr task = new TestStdErr(Logger)
                                  {
                                      Name = "n",
                                      Out = "out"
                                  };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestFailedRequired()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            TestFailed task = new TestFailed(Logger)
                                  {
                                      Name = "n",
                                      Message = "m",
                                      Details = "d"
                                  };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestFailedAll()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            TestFailed task = new TestFailed(Logger)
                                  {
                                      Name = "n",
                                      Message = "m",
                                      Details = "d",
                                      Actual = "1",
                                      Expected = "2",
                                  };
            Assert.That(task.Execute());
        }

        [Test]
        public void SetParameter()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

            SetParameter task = new SetParameter(Logger)
            {
                Name = "n",
                Value = "v"
            };
            Assert.That(task.Execute());
        }
    }
}