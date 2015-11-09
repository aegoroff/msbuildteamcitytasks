/*
 * Created by: egr
 * Created at: 11.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System.Collections.Generic;
using FluentAssertions;
using MSBuild.TeamCity.Tasks;
using Xunit;

namespace Tests
{
    public class TTaskDefaultCtor
    {
        public static IEnumerable<object[]> Instances => new[]
        {
            new object[] { new BlockClose() },
            new object[] { new BlockOpen() },
            new object[] { new BuildNumber() },
            new object[] { new BuildProgressFinish() },
            new object[] { new BuildProgressMessage() },
            new object[] { new BuildProgressStart() },
            new object[] { new BuildStatus() },
            new object[] { new ImportData() },
            new object[] { new ImportGoogleTests() },
            new object[] { new NCover3Report() },
            new object[] { new NCoverReport() },
            new object[] { new PartCoverReport() },
            new object[] { new PublishArtifacts() },
            new object[] { new ReportBuildStatistic() },
            new object[] { new ReportMessage() },
            new object[] { new RunGoogleTests() },
            new object[] { new RunOpenCoverage() },
            new object[] { new EnableServiceMessages() },
            new object[] { new DisableServiceMessages() },
            new object[] { new CompilationStarted() },
            new object[] { new CompilationFinished() },
            new object[] { new SetParameter() },
            new object[] { new TestFailed() },
            new object[] { new TestFinished() },
            new object[] { new TestIgnored() },
            new object[] { new TestStarted() },
            new object[] { new TestStdErr() },
            new object[] { new TestStdOut() },
            new object[] { new TestSuiteFinished() },
            new object[] { new TestSuiteStarted() },
            new object[] { new BuildProblem() }
        };

        [Theory, MemberData("Instances")]
        public void Create(TeamCityTask task)
        {
            task.Should().NotBeNull();
        }
    }
}