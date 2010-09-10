/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2010 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks;
using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
	[TestFixture]
	public class TSimpleTasks : TTask
	{
		[Test]
		public void CommonPropertiesTest()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			BlockOpen task = new BlockOpen(Logger)
			                 	{
			                 		Name = "n"
			                 	};
			task.FlowId = "1";
			task.IsAddTimestamp = true;
			Assert.That(task.Execute());
		}
		
		[Test]
		public void BlockTaskNameProperty()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			BlockOpen task = new BlockOpen(Logger)
			                 	{
			                 		Name = "n"
			                 	};
			Assert.That(task.Name, Is.EqualTo("n"));
		}
		
		[Test]
		public void BlockOpen()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			BlockOpen task = new BlockOpen(Logger)
			                 	{
			                 		Name = "n"
			                 	};
			Assert.That(task.Execute());
		}

		[Test]
		public void BlockClose()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			BlockClose task = new BlockClose(Logger)
			                  	{
			                  		Name = "n"
			                  	};
			Assert.That(task.Execute());
		}

		[Test]
		public void BuildNumber()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			BuildNumber task = new BuildNumber(Logger)
			                   	{
			                   		Number = "1"
			                   	};
			Assert.That(task.Execute());
		}

		[Test]
		public void BuildProgressStart()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			BuildProgressStart task = new BuildProgressStart(Logger)
			                          	{
			                          		Message = "m"
			                          	};
			Assert.That(task.Execute());
		}

		[Test]
		public void BuildProgressMessage()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			BuildProgressMessage task = new BuildProgressMessage(Logger)
			                            	{
			                            		Message = "m"
			                            	};
			Assert.That(task.Execute());
		}

		[Test]
		public void BuildProgressFinish()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			BuildProgressFinish task = new BuildProgressFinish(Logger)
			                           	{
			                           		Message = "m"
			                           	};
			Assert.That(task.Execute());
		}

		[Test]
		public void BuildStatus()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			BuildStatus task = new BuildStatus(Logger)
			                   	{
			                   		Status = "SUCCESS",
			                   		Text = "t"
			                   	};
			Assert.That(task.Execute());
		}

		[Test]
		public void ReportMessage()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			ReportMessage task = new ReportMessage(Logger)
			                     	{
			                     		Status = "WARNING",
			                     		Text = "t",
			                     		ErrorDetails = "ed"
			                     	};
			Assert.That(task.Execute());
		}
		
		[Test]
		public void ReportBuildStatistic()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			ReportBuildStatistic task = new ReportBuildStatistic(Logger)
			                     	{
			                     		Key = "k",
										Value = 1
			                     	};
			Assert.That(task.Execute());
		}
		
		[Test]
		public void ImportData()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
			
			ImportData task = new ImportData(Logger)
			                     	{
			                     		Path = "p",
			                     		Type = "dotNetCoverage",
			                     		Tool = "ncover",
			                     	};
			Assert.That(task.Execute());
		}
		
		[Test]
		public void ImportGoogleTests()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
			Expect.Once.On(Logger).GetProperty(TGoogleTestsRunner.HasLoggedErrors).Will(Return.Value(false));

			ImportGoogleTests task = new ImportGoogleTests(Logger)
			                     	{
			                     		ContinueOnFailures = true,
										TestResultsPath = TGoogleTestsPlainImporter.SuccessTestsPath,
			                     	};
			Assert.That(task.Execute());
		}
	}
}