/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2013 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Internal;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Helps to integrate Google test (http://code.google.com/p/googletest/) results in xml format
    /// into TeamCity
    ///</summary>
    /// <example>Runs tests and imports test results into TC
    /// <code><![CDATA[
    /// <Exec
    ///     Command="TestExecutable.exe --gtest_output=xml:"
    ///     Timeout="30000"
    ///     IgnoreExitCode="true"
    /// />
    /// <ImportGoogleTests
    ///     TestResultsPath="$(MSBuildProjectDirectory)\TestExecutable.xml"
    /// />
    /// ]]></code>
    /// Runs tests and imports test results into TC continues script excecution in case of failed tests
    /// <code><![CDATA[
    /// <Exec
    ///     Command="TestExecutable.exe --gtest_output=xml:"
    ///     Timeout="30000"
    ///     IgnoreExitCode="true"
    /// />
    /// <ImportGoogleTests
    ///     ContinueOnFailures="true"
    ///     TestResultsPath="$(MSBuildProjectDirectory)\TestExecutable.xml"
    /// />
    /// ]]></code>
    /// Runs tests and imports test results into TC using verbose output to log and user defined action if no data present
    /// <code><![CDATA[
    /// <Exec
    ///     Command="TestExecutable.exe --gtest_output=xml:"
    ///     Timeout="30000"
    ///     IgnoreExitCode="true"
    /// />
    /// <ImportGoogleTests
    ///     TestResultsPath="$(MSBuildProjectDirectory)\TestExecutable.xml"
    ///     WhenNoDataPublished="error"
    ///     Verbose="true"
    /// />
    /// ]]></code>
    /// </example>
    public class ImportGoogleTests : TeamCityTask
    {
        private bool status;
        
        ///<summary>
        /// Initializes a new instance of the <see cref="ImportGoogleTests"/> class
        ///</summary>
        public ImportGoogleTests()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="ImportGoogleTests"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public ImportGoogleTests(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Gets or sets full path to Google test xml output file
        /// </summary>
        [Required]
        public string TestResultsPath { get; set; }

        ///<summary>
        /// Gets or sets a value indicating whether to continue in case of failed tests. False by default
        ///</summary>
        public bool ContinueOnFailures { get; set; }

        ///<summary>
        /// Gets or sets a value indicating whether to enable detailed logging into the build log. False by default
        ///</summary>
        public bool Verbose { get; set; }

        /// <summary>
        /// Gets or sets action that will change output level if no reports matching the path specified were found.<p/>
        /// May take the following values: info (default), nothing, warning, error
        /// </summary>
        public string WhenNoDataPublished { get; set; }

        /// <summary>
        /// Gets task execution result
        /// </summary>
        protected override bool ExecutionStatus
        {
            get { return status; }
        }

        /// <summary>
        /// Reads TeamCity messages.
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            var importer = new GoogleTestsPlainImporter(Logger, ContinueOnFailures, TestResultsPath)
            {
                Verbose = Verbose,
                WhenNoDataPublished = WhenNoDataPublished
            };
            status = importer.Import();
            return importer.Messages;
        }
    }
}