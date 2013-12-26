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
        [TestCase(null, "##teamcity[importData type='FxCop' path='p']", false, null, ImportType.FxCop)]
        [TestCase("ncover", "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']", false, null, ImportType.DotNetCoverage)]
        [TestCase("ncover", "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']", false, null, ImportType.FxCop, ExpectedException = typeof(NotSupportedException))]
        [TestCase("bad", null, false, null, ImportType.FxCop, ExpectedException = typeof(NotSupportedException))]
        [TestCase(null, "##teamcity[importData type='FxCop' path='p' verbose='true']", true, null, ImportType.FxCop)]
        [TestCase(null, null, false, "fb", ImportType.FxCop, ExpectedException = typeof(NotSupportedException))]
        [TestCase(null, "##teamcity[importData type='findBugs' path='p' findBugsHome='fb']", false, "fb", ImportType.FindBugs)]
        public void Test(string tool, string expected, bool verbose, string findBugsHome, ImportType type)
        {
            var context = new ImportDataContext
            {
                Type = type,
                Verbose = verbose,
                Path = "p"
            };
            ImportDataMessageBuilder builder = new ImportDataMessageBuilder(tool, context, findBugsHome);
            Assert.That(builder.BuildMessage().ToString(), Is.EqualTo(expected));
        }
    }
}