/*
 * Created by: egr
 * Created at: 09.09.2010
 * � 2007-2010 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks;
using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
	[TestFixture]
	public class TRunGoogleTests : TTask
	{
		internal const string LogError = "LogError";
		
		[TearDown]
		public void ThisTeardown()
		{
			Teardown();
			TGoogleTestsRunner.DeleteResult();
		}

		[Test]
		public void OnlyRequired()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
			Expect.Once.On(Logger).GetProperty(TGoogleTestsRunner.HasLoggedErrors).Will(Return.Value(false));

			RunGoogleTests task = new RunGoogleTests(Logger)
			                      	{
			                      		TestExePath = TGoogleTestsRunner.CorrectExePath,
			                      	};
			Assert.That(task.Execute());
		}
		
		[Test]
		public void BadPath()
		{
			Expect.Once.On(Logger).Method(LogError).WithAnyArguments();
			Expect.Once.On(Logger).GetProperty(TGoogleTestsRunner.HasLoggedErrors).Will(Return.Value(true));

			RunGoogleTests task = new RunGoogleTests(Logger)
			                      	{
			                      		TestExePath = "bad",
			                      	};
			Assert.That(task.Execute(), Is.False);
		}
		
		[Test]
		public void ContinueOnFailures()
		{
			Expect.Once.On(Logger).Method(LogError).WithAnyArguments();
			Expect.Once.On(Logger).GetProperty(TGoogleTestsRunner.HasLoggedErrors).Will(Return.Value(false));

			RunGoogleTests task = new RunGoogleTests(Logger)
			                      	{
			                      		TestExePath = "bad",
			                      		ContinueOnFailures = true
			                      	};
			Assert.That(task.Execute());
		}

		[Test]
		public void AllPropertiesSet()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
			Expect.Once.On(Logger).GetProperty(TGoogleTestsRunner.HasLoggedErrors).Will(Return.Value(false));

			RunGoogleTests task = new RunGoogleTests(Logger)
			{
				TestExePath = TGoogleTestsRunner.CorrectExePath,
				ContinueOnFailures = false,
				RunDisabledTests = true,
				CatchGtestExceptions = true,
				TestFilter = "*",
				ExecutionTimeoutMilliseconds = 200,
			};
			Assert.That(task.Execute());
		}
	}
}