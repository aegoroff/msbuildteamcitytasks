/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System.Collections.Generic;
using MSBuild.TeamCity.Tasks.Internal;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TPartCoverCommandLine
    {
        private PartCoverCommandLine commandLine;

        private const string Target = "t";
        private const string TargetResult = "--target t";

        private const string TargetWorkDir = "d";
        private const string TargetWorkDirResult = "--target-work-dir d";

        private const string TargetArguments = "a";
        private const string TargetArgumentsResult = "--target-args a";

        private const string Output = "o";
        private const string OutputResult = "--output o";

        private const string Include = "i";
        private const string IncludeResult = "--include i";

        private const string Exclude = "e";
        private const string ExcludeResult = "--exclude e";
        private const string Space = " ";

        [SetUp]
        public void Setup()
        {
            commandLine = new PartCoverCommandLine();
        }


        [TestCase(Target, TargetResult)]
        [TestCase("t s", "--target \"t s\"")]
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
        public void IncludesProperty()
        {
            commandLine.Includes.Add(Include);
            Assert.That(commandLine.ToString(), Is.EqualTo(IncludeResult));
        }

        [Test]
        public void ManyIncludesProperty()
        {
            commandLine.Includes.Add(Include);
            commandLine.Includes.Add(Include);
            Assert.That(commandLine.ToString(), Is.EqualTo(IncludeResult + Space + IncludeResult));
        }

        [Test]
        public void ExcludesProperty()
        {
            commandLine.Excludes.Add(Exclude);
            Assert.That(commandLine.ToString(), Is.EqualTo(ExcludeResult));
        }

        [Test]
        public void ManyExcludesProperty()
        {
            commandLine.Excludes.Add(Exclude);
            commandLine.Excludes.Add(Exclude);
            Assert.That(commandLine.ToString(), Is.EqualTo(ExcludeResult + Space + ExcludeResult));
        }

        [Test]
        public void AllProperties()
        {
            commandLine.Target = Target;
            commandLine.TargetWorkDir = TargetWorkDir;
            commandLine.TargetArguments = TargetArguments;
            commandLine.Output = Output;
            commandLine.Includes.Add(Include);
            commandLine.Includes.Add(Include);
            commandLine.Excludes.Add(Exclude);
            commandLine.Excludes.Add(Exclude);
            Assert.That(commandLine.ToString(), Is.EqualTo(string.Join(Space, EnumerateAllResults())));
        }

        private static IEnumerable<string> EnumerateAllResults()
        {
            yield return TargetResult;
            yield return TargetWorkDirResult;
            yield return TargetArgumentsResult;
            yield return OutputResult;
            yield return IncludeResult;
            yield return IncludeResult;
            yield return ExcludeResult;
            yield return ExcludeResult;
        }
    }
}