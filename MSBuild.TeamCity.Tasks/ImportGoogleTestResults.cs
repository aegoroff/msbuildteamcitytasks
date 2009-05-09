/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Helps to integrate Google test (http://code.google.com/p/googletest/) results in xml format
	/// into TeamCity
	///</summary>
	/// <example>Imports google test results into TC
	/// <code><![CDATA[
	/// <ImportGoogleTestResults
	///		TestResultsPath="C:\Tests.xml"
	/// />
	/// ]]></code>
	/// </example>
	public class ImportGoogleTestResults : Task
	{
		/// <summary>
		/// Full path to Google test xml output file
		/// </summary>
		[Required]
		public string TestResultsPath { get; set; }

		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			string results = File.ReadAllText(TestResultsPath);
			GoogleTestXmlReader reader = new GoogleTestXmlReader(results);
			foreach ( string result in reader.Read() )
			{
				Log.LogMessage(MessageImportance.High, result);
			}
			return true;
		}
	}
}