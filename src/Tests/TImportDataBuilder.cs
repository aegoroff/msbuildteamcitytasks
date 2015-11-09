/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using FluentAssertions;
using MSBuild.TeamCity.Tasks.Messages;
using Xunit;

namespace Tests
{
    public class TImportDataBuilder
    {
        [Theory]
        [InlineData(null, "##teamcity[importData type='FxCop' path='p']", false, null, ImportType.FxCop)]
        [InlineData("ncover", "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']", false, null,
            ImportType.DotNetCoverage)]
        [InlineData("ncover3", "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover3']", false, null,
            ImportType.DotNetCoverage)]
        [InlineData("partcover", "##teamcity[importData type='dotNetCoverage' path='p' tool='partcover']", false, null,
            ImportType.DotNetCoverage)]
        [InlineData(null, "##teamcity[importData type='FxCop' path='p' verbose='true']", true, null, ImportType.FxCop)]
        [InlineData(null, "##teamcity[importData type='findBugs' path='p' findBugsHome='fb']", false, "fb",
            ImportType.FindBugs)]
        public void Test(string tool, string expected, bool verbose, string findBugsHome, ImportType type)
        {
            var context = new ImportDataContext
            {
                Type = type,
                Verbose = verbose,
                Path = "p"
            };
            var builder = new ImportDataMessageBuilder(tool, context, findBugsHome);
            builder.BuildMessage().ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData("bad", "##teamcity[importData type='dotNetCoverage' path='p' tool='partcover']", false, null,
            ImportType.DotNetCoverage)]
        [InlineData("ncover", "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']", false, null,
            ImportType.FxCop)]
        [InlineData("bad", null, false, null, ImportType.FxCop)]
        [InlineData(null, null, false, "fb", ImportType.FxCop)]
        public void TestExceptions(string tool, string expected, bool verbose, string findBugsHome, ImportType type)
        {
            Assert.Throws<NotSupportedException>(
                delegate
                {
                    Test(tool, expected, verbose, findBugsHome, type);
                });
        }
    }
}