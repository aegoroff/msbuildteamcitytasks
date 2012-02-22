/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2012 Alexander Egorov
 */

using System;
using MSBuild.TeamCity.Tasks.Messages;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TImportDataBuilder
    {
        [TestCase(null, "##teamcity[importData type='FxCop' path='p']")]
        [TestCase("ncover", "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']")]
        [TestCase("bad", null, ExpectedException = typeof(NotSupportedException))]
        public void Test(string tool, string expected)
        {
            const string type = "FxCop";
            const string path = "p";
            ImportDataMessageBuilder builder = new ImportDataMessageBuilder(tool, path, type);
            Assert.That(builder.BuildMessage().ToString(), Is.EqualTo(expected));
        }
    }
}