/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System;
using MSBuild.TeamCity.Tasks.Messages;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TImportDataBuilder
    {
        [TestCase(null, "##teamcity[importData type='FxCop' path='p']", false, null)]
        [TestCase("ncover", "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']", false, null)]
        [TestCase("bad", null, false, null, ExpectedException = typeof(NotSupportedException))]
        [TestCase(null, "##teamcity[importData type='FxCop' path='p' verbose='true']", true, null)]
        [TestCase("ncover", "##teamcity[importData type='findBugs' path='p' findBugsHome='fb']", false, "fb")]
        [TestCase("findBugs", "##teamcity[importData type='findBugs' path='p' findBugsHome='fb']", false, "fb")]
        [TestCase(null, "##teamcity[importData type='findBugs' path='p' findBugsHome='fb']", false, "fb")]
        public void Test(string tool, string expected, bool verbose, string findBugsHome)
        {
            const string type = "FxCop";
            const string path = "p";
            ImportDataMessageBuilder builder = new ImportDataMessageBuilder(tool, path, type, findBugsHome, verbose);
            Assert.That(builder.BuildMessage().ToString(), Is.EqualTo(expected));
        }
    }
}