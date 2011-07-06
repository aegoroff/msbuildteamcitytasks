/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2011 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Internal
{
    ///<summary>
    /// Represents plain wrapper that uses existing tests result xml import
    ///</summary>
    public class GoogleTestsPlainImporter : GoogleTestsImporter
    {
        private readonly string _testResultsPath;

        ///<summary>
        /// Initializes a new instance of the <see cref="GoogleTestsPlainImporter"/> class
        ///</summary>
        ///<param name="logger">Logger instance</param>
        ///<param name="continueOnFailures">Whether to continue execution on broken tests</param>
        ///<param name="testResultsPath">Path to tests' xml import</param>
        public GoogleTestsPlainImporter(ILogger logger, bool continueOnFailures, string testResultsPath)
            : base(logger, continueOnFailures)
        {
            _testResultsPath = testResultsPath;
        }

        /// <summary>
        /// Creates XML import file
        /// </summary>
        /// <returns>Path to xml file to import</returns>
        protected override string CreateXmlImport()
        {
            return _testResultsPath;
        }
    }
}