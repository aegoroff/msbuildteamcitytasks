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
        [TestCase(null, "##teamcity[importData type='FxCop' path='p']", false, null, "FxCop")]
        [TestCase("ncover", "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']", false, null, "FxCop")]
        [TestCase("bad", null, false, null, "FxCop", ExpectedException = typeof(NotSupportedException))]
        [TestCase(null, "##teamcity[importData type='FxCop' path='p' verbose='true']", true, null, "FxCop")]
        [TestCase(null, null, false, "fb", "FxCop", ExpectedException = typeof(NotSupportedException))]
        [TestCase(null, "##teamcity[importData type='findBugs' path='p' findBugsHome='fb']", false, "fb", "findBugs")]
        public void Test(string tool, string expected, bool verbose, string findBugsHome, string type)
        {
            const string path = "p";
            ImportDataMessageBuilder builder = new ImportDataMessageBuilder(tool, path, type, findBugsHome, verbose);
            Assert.That(builder.BuildMessage().ToString(), Is.EqualTo(expected));
        }
    }
}