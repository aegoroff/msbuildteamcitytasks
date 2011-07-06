/*
 * Created by: egr
 * Created at: 27.08.2010
 * © 2007-2011 Alexander Egorov
 */

using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Internal;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Helps to run Google test (http://code.google.com/p/googletest/) executable and
    /// integrate results into TC
    ///</summary>
    /// <example>Runs tests and imports test results into TC
    /// <code><![CDATA[
    /// <RunGoogleTests
    ///     TestExePath="TestExecutable.exe"
    /// />
    /// ]]></code>
    /// Runs tests and waits 30 seconds the tests to complete
    /// <code><![CDATA[
    /// <RunGoogleTests
    ///     ExecutionTimeoutMilliseconds="30000"
    ///     TestExePath="TestExecutable.exe"
    /// />
    /// ]]></code>
    /// Runs tests and continues script excecution in case of failed tests
    /// <code><![CDATA[
    /// <RunGoogleTests
    ///     ContinueOnFailures="true"
    ///     TestExePath="TestExecutable.exe"
    /// />
    /// ]]></code>
    /// Runs tests and suppress pop-ups caused by exceptions
    /// <code><![CDATA[
    /// <RunGoogleTests
    ///     CatchGtestExceptions="true"
    ///     TestExePath="TestExecutable.exe"
    /// />
    /// ]]></code>
    /// Runs tests including all disabled tests too
    /// <code><![CDATA[
    /// <RunGoogleTests
    ///     RunDisabledTests="true"
    ///     TestExePath="TestExecutable.exe"
    /// />
    /// ]]></code>
    /// Runs only tests which named are matched to the filter specified.
    /// <code><![CDATA[
    /// <RunGoogleTests
    ///     TestFilter="SpecialTest*"
    ///     TestExePath="TestExecutable.exe"
    /// />
    /// ]]></code>
    /// </example>
    public class RunGoogleTests : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="RunGoogleTests"/> class
        ///</summary>
        public RunGoogleTests()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="RunGoogleTests"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public RunGoogleTests(ILogger logger) : base(logger)
        {
        }

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
        /// Gets or sets the time to wait the specified number of milliseconds for the test process to finish.
        /// By default waiting indefinitely.
        /// </summary>
        public int ExecutionTimeoutMilliseconds { get; set; }

        /// <summary>
        /// Gets task execution result
        /// </summary>
        protected override ExecutionResult ExecutionResult
        {
            get
            {
                GoogleTestsRunner runner = new GoogleTestsRunner(Logger, ContinueOnFailures, TestExePath)
                                               {
                                                   CatchGtestExceptions = CatchGtestExceptions,
                                                   ExecutionTimeoutMilliseconds = ExecutionTimeoutMilliseconds,
                                                   RunDisabledTests = RunDisabledTests,
                                                   TestFilter = TestFilter
                                               };

                return runner.Import();
            }
        }
    }
}