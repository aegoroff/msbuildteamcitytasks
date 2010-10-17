/*
 * Created by: egr
 * Created at: 11.09.2010
 * © 2007-2010 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks;
using NUnit.Framework;

namespace Tests
{
    [TestFixture( typeof(BlockClose) )]
    [TestFixture( typeof(BlockOpen) )]
    [TestFixture( typeof(BuildNumber) )]
    [TestFixture( typeof(BuildProgressFinish) )]
    [TestFixture( typeof(BuildProgressMessage) )]
    [TestFixture( typeof(BuildProgressStart) )]
    [TestFixture( typeof(BuildStatus) )]
    [TestFixture( typeof(ImportData) )]
    [TestFixture( typeof(ImportGoogleTests) )]
    [TestFixture( typeof(NCover3Report) )]
    [TestFixture( typeof(NCoverReport) )]
    [TestFixture( typeof(PartCoverReport) )]
    [TestFixture( typeof(PublishArtifacts) )]
    [TestFixture( typeof(ReportBuildStatistic) )]
    [TestFixture( typeof(ReportMessage) )]
    [TestFixture( typeof(RunGoogleTests) )]
    [TestFixture( typeof(RunPartCoverage) )]
    [TestFixture( typeof(EnableServiceMessages) )]
    [TestFixture( typeof(DisableServiceMessages) )]
    [TestFixture( typeof(CompilationStarted) )]
    public class TTaskDefaultCtor<TTsk> where TTsk : TeamCityTask, new()
    {
        [Test]
        public void Create()
        {
            new TTsk();
        }
    }
}