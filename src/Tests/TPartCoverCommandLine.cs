/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System.Collections.Generic;
using MSBuild.TeamCity.Tasks;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class TPartCoverCommandLine
	{
		private PartCoverCommandLine _commandLine;

		private const string Target = "t";
		private const string TargetResult = "--target t";
		
		private const string TargetWithSpaces = "t s";
		private const string TargetWithSpacesResult = "--target \"t s\"";
		
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
			_commandLine = new PartCoverCommandLine();
		}

		[Test]
		public void TargetProperty()
		{
			_commandLine.Target = Target;
			Assert.That(_commandLine.ToString(), Is.EqualTo(TargetResult));
		}
		
		[Test]
		public void TargetWithSpacesProperty()
		{
			_commandLine.Target = TargetWithSpaces;
			Assert.That(_commandLine.ToString(), Is.EqualTo(TargetWithSpacesResult));
		}
		
		[Test]
		public void TargetWorkDirProperty()
		{
			_commandLine.TargetWorkDir = TargetWorkDir;
			Assert.That(_commandLine.ToString(), Is.EqualTo(TargetWorkDirResult));
		}
		
		[Test]
		public void TargetArgumentsProperty()
		{
			_commandLine.TargetArguments = TargetArguments;
			Assert.That(_commandLine.ToString(), Is.EqualTo(TargetArgumentsResult));
		}
		
		[Test]
		public void OutputProperty()
		{
			_commandLine.Output = Output;
			Assert.That(_commandLine.ToString(), Is.EqualTo(OutputResult));
		}
		
		[Test]
		public void IncludesProperty()
		{
			_commandLine.Includes.Add(Include);
			Assert.That(_commandLine.ToString(), Is.EqualTo(IncludeResult));
		}
		
		[Test]
		public void ManyIncludesProperty()
		{
			_commandLine.Includes.Add(Include);
			_commandLine.Includes.Add(Include);
			Assert.That(_commandLine.ToString(), Is.EqualTo(IncludeResult + Space + IncludeResult));
		}
		
		[Test]
		public void ExcludesProperty()
		{
			_commandLine.Excludes.Add(Exclude);
			Assert.That(_commandLine.ToString(), Is.EqualTo(ExcludeResult));
		}

		[Test]
		public void ManyExcludesProperty()
		{
			_commandLine.Excludes.Add(Exclude);
			_commandLine.Excludes.Add(Exclude);
			Assert.That(_commandLine.ToString(), Is.EqualTo(ExcludeResult + Space + ExcludeResult));
		}
		
		[Test]
		public void AllProperties()
		{
			_commandLine.Target = Target;
			_commandLine.TargetWorkDir = TargetWorkDir;
			_commandLine.TargetArguments = TargetArguments;
			_commandLine.Output = Output;
			_commandLine.Includes.Add(Include);
			_commandLine.Includes.Add(Include);
			_commandLine.Excludes.Add(Exclude);
			_commandLine.Excludes.Add(Exclude);
			SequenceBuilder<string> sequence = new SequenceBuilder<string>(EnumerateAllResults(), Space);
			Assert.That(_commandLine.ToString(), Is.EqualTo(sequence.ToString()));
		}

		static IEnumerable<string> EnumerateAllResults()
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