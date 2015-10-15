/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2015 Alexander Egorov
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
        protected override void AfterSetup()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
        }

        [Test]
        public void CommonPropertiesTest()
        {
            var task = new BlockOpen(Logger.MockObject)
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
            var task = new BlockOpen(Logger.MockObject)
                                 {
                                     Name = "n"
                                 };
            Assert.That(task.Name, Is.EqualTo("n"));
        }

        [Test]
        public void BlockOpen()
        {
            var task = new BlockOpen(Logger.MockObject)
                                 {
                                     Name = "n"
                                 };
            Assert.That(task.Execute());
        }

        [Test]
        public void BlockClose()
        {
            var task = new BlockClose(Logger.MockObject)
                                  {
                                      Name = "n"
                                  };
            Assert.That(task.Execute());
        }

        [Test]
        public void BuildNumber()
        {
            var task = new BuildNumber(Logger.MockObject)
                                   {
                                       Number = "1"
                                   };
            Assert.That(task.Execute());
            Assert.That(task.Number, Is.EqualTo("1"));
        }

        [Test]
        public void BuildProgressStart()
        {
            var task = new BuildProgressStart(Logger.MockObject)
                                          {
                                              Message = "m"
                                          };
            Assert.That(task.Execute());
            Assert.That(task.Message, Is.EqualTo("m"));
        }

        [Test]
        public void BuildProgressMessage()
        {
            var task = new BuildProgressMessage(Logger.MockObject)
                                            {
                                                Message = "m"
                                            };
            Assert.That(task.Execute());
            Assert.That(task.Message, Is.EqualTo("m"));
        }

        [Test]
        public void BuildProgressFinish()
        {
            var task = new BuildProgressFinish(Logger.MockObject)
                                           {
                                               Message = "m"
                                           };
            Assert.That(task.Execute());
            Assert.That(task.Message, Is.EqualTo("m"));
        }

        [Test]
        public void BuildStatus()
        {
            var task = new BuildStatus(Logger.MockObject)
                                   {
                                       Status = "SUCCESS",
                                       Text = "t"
                                   };
            Assert.That(task.Execute());
        }

        [Test]
        public void ReportMessage()
        {
            var task = new ReportMessage(Logger.MockObject)
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
            var task = new ReportBuildStatistic(Logger.MockObject)
                                            {
                                                Key = "k",
                                                Value = 1
                                            };
            Assert.That(task.Execute());
            Assert.That(task.Key, Is.EqualTo("k"));
            Assert.That(task.Value, Is.EqualTo(1));
        }

        [TestCase("dotNetCoverage", "ncover", false, false, null)]
        [TestCase("bad", null, false, false, null, ExpectedException = typeof(UnexpectedInvocationException))]
        [TestCase("mstest", "ncover", false, false, null, ExpectedException = typeof(UnexpectedInvocationException))]
        [TestCase("mstest", null, false, false, null)]
        [TestCase("dotNetCoverage", "ncover", true, false, null)]
        [TestCase("dotNetCoverage", "ncover", false, true, null)]
        [TestCase("dotNetCoverage", "ncover", true, true, null)]
        [TestCase("mstest", null, false, false, "info")]
        [TestCase("mstest", null, false, false, "nothing")]
        [TestCase("mstest", null, false, false, "warning")]
        [TestCase("mstest", null, false, false, "error")]
        [TestCase("mstest", null, false, false, "bad", ExpectedException = typeof(UnexpectedInvocationException))]
        public void ImportData(string type, string tool, bool verbose, bool parseOutOfDate, string whenNoDataPublished)
        {
            var task = new ImportData(Logger.MockObject)
                                  {
                                      Path = "p",
                                      Type = type,
                                      Tool = tool,
                                      Verbose = verbose,
                                      ParseOutOfDate = parseOutOfDate,
                                      WhenNoDataPublished = whenNoDataPublished
                                  };
            Assert.That(task.Execute());
            Assert.That(task.Path, Is.EqualTo("p"));
            Assert.That(task.Type, Is.EqualTo(type));
            Assert.That(task.Tool, Is.EqualTo(tool));
        }

        [Test]
        public void ImportGoogleTests()
        {
            Logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            var task = new ImportGoogleTests(Logger.MockObject)
                                         {
                                             ContinueOnFailures = true,
                                             TestResultsPath = TGoogleTestsPlainImporter.SuccessTestsPath,
                                             Verbose = true,
                                             WhenNoDataPublished = "error",
                                             ParseOutOfDate = true
                                         };
            Assert.That(task.Execute());
            Assert.That(task.ContinueOnFailures);
            Assert.That(task.TestResultsPath, Is.EqualTo(TGoogleTestsPlainImporter.SuccessTestsPath));
        }

        [Test]
        public void EnableServiceMessages()
        {
            var task = new EnableServiceMessages(Logger.MockObject);
            Assert.That(task.Execute());
        }

        [Test]
        public void DisableServiceMessages()
        {
            var task = new DisableServiceMessages(Logger.MockObject);
            Assert.That(task.Execute());
        }

        [Test]
        public void CompilationStarted()
        {
            var task = new CompilationStarted(Logger.MockObject);
            Assert.That(task.Execute());
        }

        [Test]
        public void CompilationFinished()
        {
            var task = new CompilationFinished(Logger.MockObject);
            Assert.That(task.Execute());
        }

        [Test]
        public void TestSuiteStarted()
        {
            var task = new TestSuiteStarted(Logger.MockObject)
                                        {
                                            Name = "n"
                                        };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestSuiteFinished()
        {
            var task = new TestSuiteFinished(Logger.MockObject)
                                         {
                                             Name = "n"
                                         };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestStarted()
        {
            var task = new TestStarted(Logger.MockObject)
                                   {
                                       Name = "n"
                                   };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestStartedCaptureStandardOutput()
        {
            var task = new TestStarted(Logger.MockObject)
                                   {
                                       Name = "n",
                                       CaptureStandardOutput = true
                                   };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestFinished()
        {
            var task = new TestFinished(Logger.MockObject)
                                    {
                                        Name = "n",
                                        Duration = 3.0
                                    };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestIgnored()
        {
            var task = new TestIgnored(Logger.MockObject)
                                   {
                                       Name = "n",
                                       Message = "Comment"
                                   };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestStdOut()
        {
            var task = new TestStdOut(Logger.MockObject)
                                  {
                                      Name = "n",
                                      Out = "out"
                                  };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestStdErr()
        {
            var task = new TestStdErr(Logger.MockObject)
                                  {
                                      Name = "n",
                                      Out = "out"
                                  };
            Assert.That(task.Execute());
        }

        [Test]
        public void TestFailedRequired()
        {
            var task = new TestFailed(Logger.MockObject)
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
            var task = new TestFailed(Logger.MockObject)
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
            var task = new SetParameter(Logger.MockObject)
            {
                Name = "n",
                Value = "v"
            };
            Assert.That(task.Execute());
        }
        
        [Test]
        public void BuildProblem()
        {
            var task = new BuildProblem(Logger.MockObject)
            {
                Description = "d",
                Identity = "i"
            };
            Assert.That(task.Execute());
        }
    }
}