/*
 * Created by: egr
 * Created at: 09.09.2010
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
    public class TSimpleTasks : TTask
    {
        [Test]
        public void CommonPropertiesTest()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            BlockOpen task = new BlockOpen(Logger.MockObject)
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
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            BlockOpen task = new BlockOpen(Logger.MockObject)
                                 {
                                     Name = "n"
                                 };
            Assert.That(task.Name, Is.EqualTo("n"));
        }

        [Test]
        public void BlockOpen()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            BlockOpen task = new BlockOpen(Logger.MockObject)
                                 {
                                     Name = "n"
                                 };
            Assert.That(task.Execute());
        }

        [Test]
        public void BlockClose()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            BlockClose task = new BlockClose(Logger.MockObject)
                                  {
                                      Name = "n"
                                  };
            Assert.That(task.Execute());
        }

        [Test]
        public void BuildNumber()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            BuildNumber task = new BuildNumber(Logger.MockObject)
                                   {
                                       Number = "1"
                                   };
            Assert.That(task.Execute());
            Assert.That(task.Number, Is.EqualTo("1"));
        }

        [Test]
        public void BuildProgressStart()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            BuildProgressStart task = new BuildProgressStart(Logger.MockObject)
                                          {
                                              Message = "m"
                                          };
            Assert.That(task.Execute());
            Assert.That(task.Message, Is.EqualTo("m"));
        }

        [Test]
        public void BuildProgressMessage()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            BuildProgressMessage task = new BuildProgressMessage(Logger.MockObject)
                                            {
                                                Message = "m"
                                            };
            Assert.That(task.Execute());
            Assert.That(task.Message, Is.EqualTo("m"));
        }

        [Test]
        public void BuildProgressFinish()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            BuildProgressFinish task = new BuildProgressFinish(Logger.MockObject)
                                           {
                                               Message = "m"
                                           };
            Assert.That(task.Execute());
            Assert.That(task.Message, Is.EqualTo("m"));
        }

        [Test]
        public void BuildStatus()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            BuildStatus task = new BuildStatus(Logger.MockObject)
                                   {
                                       Status = "SUCCESS",
                                       Text = "t"
                                   };
            Assert.That(task.Execute());
        }

        [Test]
        public void ReportMessage()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            ReportMessage task = new ReportMessage(Logger.MockObject)
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
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            ReportBuildStatistic task = new ReportBuildStatistic(Logger.MockObject)
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
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            ImportData task = new ImportData(Logger.MockObject)
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
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            Logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            ImportGoogleTests task = new ImportGoogleTests(Logger.MockObject)
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
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            EnableServiceMessages task = new EnableServiceMessages(Logger.MockObject);
            Assert.That(task.Execute());
        }

        [Test]
        public void DisableServiceMessages()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            DisableServiceMessages task = new DisableServiceMessages(Logger.MockObject);
            Assert.That(task.Execute());
        }

        [Test]
        public void CompilationStarted()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            CompilationStarted task = new CompilationStarted(Logger.MockObject);
            Assert.That(task.Execute());
        }

        [Test]
        public void CompilationFinished()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            CompilationFinished task = new CompilationFinished(Logger.MockObject);
            Assert.That(task.Execute());
        }

        [Test]
        public void TestSuiteStarted()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            TestSuiteStarted task = new TestSuiteStarted(Logger.MockObject)
                                        {
                                            Name = "n"
                                        };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestSuiteFinished()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            TestSuiteFinished task = new TestSuiteFinished(Logger.MockObject)
                                         {
                                             Name = "n"
                                         };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestStarted()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            TestStarted task = new TestStarted(Logger.MockObject)
                                   {
                                       Name = "n"
                                   };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestStartedCaptureStandardOutput()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            TestStarted task = new TestStarted(Logger.MockObject)
                                   {
                                       Name = "n",
                                       CaptureStandardOutput = true
                                   };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestFinished()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            TestFinished task = new TestFinished(Logger.MockObject)
                                    {
                                        Name = "n",
                                        Duration = 3.0
                                    };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestIgnored()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            TestIgnored task = new TestIgnored(Logger.MockObject)
                                   {
                                       Name = "n",
                                       Message = "Comment"
                                   };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestStdOut()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            TestStdOut task = new TestStdOut(Logger.MockObject)
                                  {
                                      Name = "n",
                                      Out = "out"
                                  };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestStdErr()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            TestStdErr task = new TestStdErr(Logger.MockObject)
                                  {
                                      Name = "n",
                                      Out = "out"
                                  };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestFailedRequired()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            TestFailed task = new TestFailed(Logger.MockObject)
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
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            TestFailed task = new TestFailed(Logger.MockObject)
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
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            SetParameter task = new SetParameter(Logger.MockObject)
            {
                Name = "n",
                Value = "v"
            };
            Assert.That(task.Execute());
        }
        
        [Test]
        public void BuildProblem()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            BuildProblem task = new BuildProblem(Logger.MockObject)
            {
                Description = "d",
                Identity = "i"
            };
            Assert.That(task.Execute());
        }
    }
}