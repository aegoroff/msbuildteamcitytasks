/*
 * Created by: egr
 * Created at: 27.08.2010
 * © 2007-2010 Alexander Egorov
 */

using System.Collections.Generic;
using System.Globalization;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Helps to build command line arguments string to pass Google test executable
	///</summary>
	public sealed class GoogleTestArgumentsBuilder
	{
		private readonly bool _catchExceptions;
		private readonly bool _runDisabledTests;
		private readonly string _filter;
		private const string OutputXml = "--gtest_output=xml:";
		private const string DisableTestsCommand = "--gtest_also_run_disabled_tests";
		private const string CatchExceptionsCommand = "--gtest_catch_exceptions";
		private const string Space = " ";
		private const string FilterCommand = "--gtest_filter=\"{0}\"";

		///<summary>
		/// Initializes a new instance of the <see cref="GoogleTestArgumentsBuilder"/> class.
		///</summary>
		///<param name="catchExceptions">suppress pop-ups caused by exceptions</param>
		///<param name="runDisabledTests">run all disabled tests too</param>
		///<param name="filter">
		/// Run only the tests whose name matches one of the positive patterns but 
		/// none of the negative patterns. '?' matches any single character; '*' 
		/// matches any substring; ':' separates two patterns.
		/// </param>
		public GoogleTestArgumentsBuilder( bool catchExceptions, bool runDisabledTests, string filter )
		{
			_catchExceptions = catchExceptions;
			_runDisabledTests = runDisabledTests;
			_filter = filter;
		}

		///<summary>
		/// Creates command line arguments string to pass Google test executable
		///</summary>
		///<returns>command line arguments string</returns>
		public string CreateCommandLine()
		{
			SequenceBuilder<string> sequence = new SequenceBuilder<string>(CreateOptions(), Space);
			return sequence.ToString();
		}

		private IEnumerable<string> CreateOptions()
		{
			yield return OutputXml;

			if ( _runDisabledTests )
			{
				yield return DisableTestsCommand;
			}
			if ( _catchExceptions )
			{
				yield return CatchExceptionsCommand;
			}
			if ( !string.IsNullOrEmpty(_filter) )
			{
				yield return string.Format(CultureInfo.CurrentCulture, FilterCommand, _filter);
			}
		}
	}
}