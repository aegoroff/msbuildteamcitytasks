/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2012 Alexander Egorov
 */

using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Internal;

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
    /// </example>
    public class ImportGoogleTests : TeamCityTask
    {
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

        /// <summary>
        /// Gets task execution result
        /// </summary>
        protected override ExecutionResult ExecutionResult
        {
            get { return new GoogleTestsPlainImporter(Logger, ContinueOnFailures, TestResultsPath).Import(); }
        }
    }
}