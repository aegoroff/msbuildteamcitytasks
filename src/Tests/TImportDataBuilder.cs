/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System;
using MSBuild.TeamCity.Tasks.Messages;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TImportDataBuilder
    {
        [Test]
        public void NoTool()
        {
            const string type = "FxCop";
            const string path = "p";
            ImportDataMessageBuilder builder = new ImportDataMessageBuilder(null, path, type);
            Assert.That(builder.BuildMessage().ToString(), Is.EqualTo("##teamcity[importData type='FxCop' path='p']"));
        }

        [Test]
        public void WithTool()
        {
            const string type = "FxCop";
            const string path = "p";
            ImportDataMessageBuilder builder = new ImportDataMessageBuilder("ncover", path, type);
            Assert.That(builder.BuildMessage().ToString(),
                        Is.EqualTo("##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']"));
        }

        [Test]
        [ExpectedException( typeof(NotSupportedException) )]
        public void WithBadTool()
        {
            const string type = "FxCop";
            const string path = "p";
            ImportDataMessageBuilder builder = new ImportDataMessageBuilder("bad", path, type);
            Assert.That(builder.BuildMessage().ToString(),
                        Is.EqualTo("##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']"));
        }
    }
}