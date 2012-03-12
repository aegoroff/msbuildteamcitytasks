/*
 * Created by: egr
 * Created at: 09.03.2012
 * © 2007-2012 Alexander Egorov
 */

using System.Collections.Generic;
using MSBuild.TeamCity.Tasks.Internal;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TOpenCoverCommandLine
    {
        private OpenCoverCommandLine commandLine;

        private const string Target = "t";
        private const string TargetResult = "-target:t";

        private const string TargetWorkDir = "d";
        private const string TargetWorkDirResult = "-targetdir:d";

        private const string TargetArguments = "a";
        private const string TargetArgumentsResult = "-targetargs:a";

        private const string Output = "o";
        private const string OutputResult = "-output:o";

        private const string Filter = "+[*]*";
        private const string FilterResult = "-filter:+[*]*";
        private const string Space = " ";

        [SetUp]
        public void Setup()
        {
            commandLine = new OpenCoverCommandLine();
        }

        [TestCase(Target, TargetResult)]
        [TestCase("t s", "\"-target:t s\"")]
        public void TargetProperty(string target, string expected)
        {
            commandLine.Target = target;
            Assert.That(commandLine.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void TargetWorkDirProperty()
        {
            commandLine.TargetWorkDir = TargetWorkDir;
            Assert.That(commandLine.ToString(), Is.EqualTo(TargetWorkDirResult));
        }

        [Test]
        public void TargetArgumentsProperty()
        {
            commandLine.TargetArguments = TargetArguments;
            Assert.That(commandLine.ToString(), Is.EqualTo(TargetArgumentsResult));
        }

        [Test]
        public void OutputProperty()
        {
            commandLine.Output = Output;
            Assert.That(commandLine.ToString(), Is.EqualTo(OutputResult));
        }
        
        [Test]
        public void FilterProperty()
        {
            commandLine.Filter.Add(Filter);
            Assert.That(commandLine.ToString(), Is.EqualTo(FilterResult));
        }
        
        [Test]
        public void ManyFiltersProperty()
        {
            commandLine.Filter.Add(Filter);
            commandLine.Filter.Add("-[System]*");
            Assert.That(commandLine.ToString(), Is.EqualTo("\"-filter:+[*]* -[System]*\""));
        }

        [Test]
        public void AllProperties()
        {
            commandLine.Target = Target;
            commandLine.TargetWorkDir = TargetWorkDir;
            commandLine.TargetArguments = TargetArguments;
            commandLine.Output = Output;
            commandLine.Filter.Add(Filter);
            Assert.That(commandLine.ToString(), Is.EqualTo(string.Join(Space, EnumerateAllResults())));
        }

        private static IEnumerable<string> EnumerateAllResults()
        {
            yield return TargetResult;
            yield return TargetWorkDirResult;
            yield return TargetArgumentsResult;
            yield return OutputResult;
            yield return FilterResult;
        }
    }
}