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
        [TestCase(null, "##teamcity[importData type='FxCop' path='p']", false)]
        [TestCase("ncover", "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']", false)]
        [TestCase("bad", null, false, ExpectedException = typeof(NotSupportedException))]
        [TestCase(null, "##teamcity[importData type='FxCop' path='p' verbose='true']", true)]
        public void Test(string tool, string expected, bool verbose)
        {
            const string type = "FxCop";
            const string path = "p";
            ImportDataMessageBuilder builder = new ImportDataMessageBuilder(tool, path, type, verbose);
            Assert.That(builder.BuildMessage().ToString(), Is.EqualTo(expected));
        }
    }
}