/*
 * Created by: egr
 * Created at: 27.08.2010
 * © 2007-2010 Alexander Egorov
 */

using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Helps to run Google test (http://code.google.com/p/googletest/) executable and
	/// integrate results into TC
	///</summary>
	/// <example>Runs tests and imports test results into TC
	/// <code><![CDATA[
	/// <RunGoogleTests
	///		TestExePath="TestExecutable.exe"
	/// />
	/// ]]></code>
	/// Runs tests and imports test results into TC continues script excecution in case of failed tests
	/// <code><![CDATA[
	/// <RunGoogleTests
	///		ContinueOnFailures="true"
	///		TestExePath="TestExecutable.exe"
	/// />
	/// ]]></code>
	/// </example>
	public class RunGoogleTests : TeamCityTask
	{
		/// <summary>
		/// Gets or sets full path to Google test executable
		/// </summary>
		[Required]
		public string TestExePath { get; set; }

		///<summary>
		/// Gets or sets a value indicating whether to continue in case of failed tests. False by default
		///</summary>
		public bool ContinueOnFailures { get; set; }
		
		///<summary>
		/// Gets or sets a value indicating whether to suppress pop-ups caused by exceptions. False by default
		///</summary>
		public bool CatchGtestExceptions { get; set; }
		
		/// <summary>
		/// Gets or sets a value indicating whether to run all disabled tests too. False by default
		/// </summary>
		public bool RunDisabledTests { get; set; }

		/// <summary>
		/// Gets or sets full tests fiter. 
		/// Run only the tests whose name matches one of the positive patterns but 
		/// none of the negative patterns. '?' matches any single character; '*' 
		/// matches any substring; ':' separates two patterns.
		/// </summary>
		public string TestFilter { get; set; }

		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			GoogleTestXmlReader reader = null;
			try
			{
				string file = Path.GetFileNameWithoutExtension(TestExePath);
				string dir = Path.GetDirectoryName(Path.GetFullPath(TestExePath));
				string xmlPath = dir + @"\" + file + ".xml";

				GoogleTestArgumentsBuilder commandLine =
					new GoogleTestArgumentsBuilder(CatchGtestExceptions, RunDisabledTests, TestFilter);
				Process gtestApp = new Process
				                   	{
				                   		StartInfo =
				                   			{
				                   				FileName = TestExePath,
												Arguments = commandLine.CreateCommandLine(),
				                   				UseShellExecute = false,
				                   				RedirectStandardOutput = true,
												WorkingDirectory = dir,
				                   				CreateNoWindow = true
				                   			}
				                   	};
				
				using ( gtestApp )
				{
					gtestApp.Start();
					gtestApp.WaitForExit();
				}

				reader = new GoogleTestXmlReader(xmlPath);
				reader.Read();
				Write(new ImportDataTeamCityMessage(ImportType.Junit, xmlPath));
			}
			catch (Exception e)
			{
				Log.LogError(e.ToString());
			}
			finally
			{
				if (reader != null)
				{
					reader.Dispose();
				}
			}
			if (ContinueOnFailures)
			{
				return !Log.HasLoggedErrors;
			}
			if (reader == null)
			{
				return false;
			}
			return reader.FailuresCount == 0 && !Log.HasLoggedErrors;
		}
	}
}