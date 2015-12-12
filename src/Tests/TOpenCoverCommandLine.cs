/*
 * Created by: egr
 * Created at: 09.03.2012
 * © 2007-2015 Alexander Egorov
 */

using System.Collections.Generic;
using FluentAssertions;
using MSBuild.TeamCity.Tasks.Internal;
using Xunit;

namespace Tests
{
    public class TOpenCoverCommandLine
    {
        private const string Target = "t";
        private const string RegisterUserResult = "-register:user ";
        private const string TargetResult = RegisterUserResult + "-target:t";

        private const string TargetWorkDir = "d";
        private const string TargetWorkDirResult = RegisterUserResult + "-targetdir:d";

        private const string TargetArguments = "a";
        private const string TargetArgumentsResult = RegisterUserResult + "-targetargs:a";

        private const string Output = "o";
        private const string OutputResult = RegisterUserResult + "-output:o";

        private const string Filter = "+[*]*";
        private const string FilterResult = RegisterUserResult + "-filter:+[*]*";
        private const string Space = " ";
        private const string SkipAutoPropsResult = RegisterUserResult + "-skipautoprops";
        private const string ReturnTargetCodeResult = RegisterUserResult + "-returntargetcode";
        private readonly OpenCoverCommandLine commandLine;

        public TOpenCoverCommandLine()
        {
            this.commandLine = new OpenCoverCommandLine();
        }

        [Theory]
        [InlineData(Target, TargetResult)]
        [InlineData("t s", RegisterUserResult + "\"-target:t s\"")]
        public void TargetProperty(string target, string expected)
        {
            this.commandLine.Target = target;
            this.commandLine.ToString().Should().Be(expected);
        }

        [Fact]
        public void TargetWorkDirProperty()
        {
            this.commandLine.TargetWorkDir = TargetWorkDir;
            this.commandLine.ToString().Should().Be(TargetWorkDirResult);
        }

        [Fact]
        public void TargetArgumentsProperty()
        {
            this.commandLine.TargetArguments = TargetArguments;
            this.commandLine.ToString().Should().Be(TargetArgumentsResult);
        }

        [Fact]
        public void OutputProperty()
        {
            this.commandLine.Output = Output;
            this.commandLine.ToString().Should().Be(OutputResult);
        }

        [Fact]
        public void FilterProperty()
        {
            this.commandLine.Filter.Add(Filter);
            this.commandLine.ToString().Should().Be(FilterResult);
        }

        [Fact]
        public void SkipAutoPropsProperty()
        {
            this.commandLine.SkipAutoProps = true;
            this.commandLine.ToString().Should().Be(SkipAutoPropsResult);
        }

        [Fact]
        public void SkipAutoPropsPropertyFalse()
        {
            this.commandLine.SkipAutoProps = false;
            this.commandLine.ToString().Should().Be(RegisterUserResult.TrimEnd());
        }

        [Fact]
        public void ReturnTargetCodeResultProperty()
        {
            this.commandLine.ReturnTargetCode = true;
            this.commandLine.ToString().Should().Be(ReturnTargetCodeResult);
        }

        [Fact]
        public void ReturnTargetCodeResultPropertyFalse()
        {
            this.commandLine.ReturnTargetCode = false;
            this.commandLine.ToString().Should().Be(RegisterUserResult.TrimEnd());
        }

        [Fact]
        public void ManyFiltersProperty()
        {
            this.commandLine.Filter.Add(Filter);
            this.commandLine.Filter.Add("-[System]*");
            this.commandLine.ToString().Should().Be(RegisterUserResult + "\"-filter:+[*]* -[System]*\"");
        }

        [Fact]
        public void AllProperties()
        {
            this.commandLine.Target = Target;
            this.commandLine.TargetWorkDir = TargetWorkDir;
            this.commandLine.TargetArguments = TargetArguments;
            this.commandLine.Output = Output;
            this.commandLine.HideSkipped = "all";
            this.commandLine.ExcludeByfile = "*.Generated.cs";
            this.commandLine.SkipAutoProps = true;
            this.commandLine.Filter.Add(Filter);
            this.commandLine.ToString().Should().Be(string.Join(Space, EnumerateAllResults()));
        }

        private static IEnumerable<string> EnumerateAllResults()
        {
            yield return TargetResult;
            yield return TargetWorkDirResult.Replace(RegisterUserResult, string.Empty);
            yield return TargetArgumentsResult.Replace(RegisterUserResult, string.Empty);
            yield return OutputResult.Replace(RegisterUserResult, string.Empty);
            yield return FilterResult.Replace(RegisterUserResult, string.Empty);
            yield return "-hideskipped:all";
            yield return "-excludebyfile:*.Generated.cs";
            yield return SkipAutoPropsResult.Replace(RegisterUserResult, string.Empty);
        }
    }
}