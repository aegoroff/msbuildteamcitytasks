/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System.IO;

namespace MSBuild.TeamCity.Tasks.Internal
{
    ///<summary>
    /// Represents Google test executable run wrapper
    ///</summary>
    public sealed class GoogleTestsRunner : GoogleTestsImporter
    {
        private readonly string _testExePath;

        ///<summary>
        /// Initializes a new instance of the <see cref="GoogleTestsRunner"/> class
        ///</summary>
        ///<param name="logger">Logger instance</param>
        ///<param name="continueOnFailures">Whether to continue execution on broken tests</param>
        ///<param name="testExePath">Path to Google test executable</param>
        public GoogleTestsRunner( ILogger logger, bool continueOnFailures, string testExePath )
            : base(logger, continueOnFailures)
        {
            _testExePath = testExePath;
        }

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
        /// Creates XML import file
        /// </summary>
        /// <returns>Path to xml file to import</returns>
        protected override string CreateXmlImport()
        {
            string file = Path.GetFileNameWithoutExtension(_testExePath);
            string dir = Path.GetDirectoryName(Path.GetFullPath(_testExePath));
            string xmlPath = dir + @"\" + file + ".xml";

            // to fix IssueID 3 (delete file from previous tests run)
            if ( File.Exists(xmlPath) )
            {
                File.Delete(xmlPath);
            }

            GoogleTestArgumentsBuilder commandLine =
                new GoogleTestArgumentsBuilder(CatchGtestExceptions, RunDisabledTests, TestFilter);

            ProcessRunner processRunner = new ProcessRunner(_testExePath)
                                              {
                                                  ExecutionTimeoutMilliseconds = ExecutionTimeoutMilliseconds
                                              };
            processRunner.Run(commandLine.CreateCommandLine());

            return xmlPath;
        }
    }
}