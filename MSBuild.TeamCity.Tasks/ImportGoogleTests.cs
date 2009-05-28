/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System;
using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Helps to integrate Google test (http://code.google.com/p/googletest/) results in xml format
	/// into TeamCity
	///</summary>
	/// <example>Runs tests and imports test results into TC
	/// <code><![CDATA[
	/// <Exec
	///		Command="TestExecutable.exe --gtest_output=xml:"
	///		Timeout="30000"
	///		IgnoreExitCode="true"
	/// />
	/// <ImportGoogleTests
	///		TestResultsPath="$(MSBuildProjectDirectory)\TestExecutable.xml"
	/// />
	/// ]]></code>
	/// Runs tests and imports test results into TC continues script excecution in case of failed tests
	/// <code><![CDATA[
	/// <Exec
	///		Command="TestExecutable.exe --gtest_output=xml:"
	///		Timeout="30000"
	///		IgnoreExitCode="true"
	/// />
	/// <ImportGoogleTests
	///		ContinueOnFailures="true"
	///		TestResultsPath="$(MSBuildProjectDirectory)\TestExecutable.xml"
	/// />
	/// ]]></code>
	/// </example>
	public class ImportGoogleTests : TeamCityTask
	{
		/// <summary>
		/// Full path to Google test xml output file
		/// </summary>
		[Required]
		public string TestResultsPath { get; set; }

		///<summary>
		/// Gets or sets whether to continue in case of failed tests. False by default
		///</summary>
		public bool ContinueOnFailures { get; set; }


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
				reader = new GoogleTestXmlReader(TestResultsPath);
				reader.Read();
				Write(new ImportDataTeamCityMessage("junut", TestResultsPath));
			}
			catch ( Exception e )
			{
				Log.LogError(e.ToString());
			}
			finally
			{
				if ( reader != null )
				{
					reader.Dispose();
				}
			}
			if ( ContinueOnFailures )
			{
				return !Log.HasLoggedErrors;
			}
			if ( reader == null )
			{
				return false;
			}
			return reader.FailuresCount == 0 && !Log.HasLoggedErrors;
		}
	}
}