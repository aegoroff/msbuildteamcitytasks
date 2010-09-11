/*
 * Created by: egr
 * Created at: 28.08.2010
 * © 2007-2010 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks.Internal;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class TGoogleTestArgumentsBuilder
	{
		private const string OutputXml = "--gtest_output=xml:";
		private const string DisableTestsCommand = "--gtest_also_run_disabled_tests";
		private const string CatchExceptionsCommand = "--gtest_catch_exceptions";
		private const string Space = " ";
		private const string FilterCommand = "--gtest_filter=\"1\"";
		private const string Filter = "1";

		[Test]
		public void OnlyXml()
		{
			GoogleTestArgumentsBuilder builder = new GoogleTestArgumentsBuilder(false, false, string.Empty);
			Assert.That(builder.CreateCommandLine(), Is.EqualTo(OutputXml));
		}

		[Test]
		public void OnlyCathcingExceptions()
		{
			GoogleTestArgumentsBuilder builder = new GoogleTestArgumentsBuilder(true, false, string.Empty);
			Assert.That(builder.CreateCommandLine(), Is.EqualTo(OutputXml + Space + CatchExceptionsCommand));
		}

		[Test]
		public void OnlyRunDisabledTests()
		{
			GoogleTestArgumentsBuilder builder = new GoogleTestArgumentsBuilder(false, true, string.Empty);
			Assert.That(builder.CreateCommandLine(), Is.EqualTo(OutputXml + Space + DisableTestsCommand));
		}

		[Test]
		public void OnlyFilterTests()
		{
			GoogleTestArgumentsBuilder builder = new GoogleTestArgumentsBuilder(false, false, Filter);
			Assert.That(builder.CreateCommandLine(), Is.EqualTo(OutputXml + Space + FilterCommand));
		}

		[Test]
		public void RunDisabledTestsAndCathcingExceptions()
		{
			GoogleTestArgumentsBuilder builder = new GoogleTestArgumentsBuilder(true, true, string.Empty);
			Assert.That(builder.CreateCommandLine(),
			            Is.EqualTo(OutputXml + Space + DisableTestsCommand + Space + CatchExceptionsCommand));
		}

		[Test]
		public void RunDisabledTestsAndFilter()
		{
			GoogleTestArgumentsBuilder builder = new GoogleTestArgumentsBuilder(false, true, Filter);
			Assert.That(builder.CreateCommandLine(), Is.EqualTo(OutputXml + Space + DisableTestsCommand + Space + FilterCommand));
		}

		[Test]
		public void CathcingExceptionsAndFilter()
		{
			GoogleTestArgumentsBuilder builder = new GoogleTestArgumentsBuilder(true, false, Filter);
			Assert.That(builder.CreateCommandLine(),
			            Is.EqualTo(OutputXml + Space + CatchExceptionsCommand + Space + FilterCommand));
		}

		[Test]
		public void All()
		{
			GoogleTestArgumentsBuilder builder = new GoogleTestArgumentsBuilder(true, true, Filter);
			Assert.That(builder.CreateCommandLine(),
			            Is.EqualTo(OutputXml + Space + DisableTestsCommand + Space + CatchExceptionsCommand + Space +
			                       FilterCommand));
		}
	}
}