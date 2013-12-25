/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    /// Represents Google tests abstract importer
    /// </summary>
    public abstract class GoogleTestsImporter
    {
        private readonly ILogger logger;
        private readonly bool continueOnFailures;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleTestsImporter"/> class
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="continueOnFailures">Whether to continue build in case of failing tests</param>
        protected GoogleTestsImporter(ILogger logger, bool continueOnFailures)
        {
            this.logger = logger;
            this.continueOnFailures = continueOnFailures;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable detailed logging into the build log. False by default
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        /// Runs import
        /// </summary>
        /// <returns>
        /// The new instance of <see cref="ExecutionResult"/> structure.
        /// </returns>
        public ExecutionResult Import()
        {
            var result = new ExecutionResult(false);
            GoogleTestXmlReader reader = null;
            try
            {
                var xmlPath = CreateXmlImport();

                reader = new GoogleTestXmlReader(xmlPath);
                reader.Read();
                result.Messages.Add(new ImportDataTeamCityMessage(ImportType.Gtest, xmlPath, Verbose));
            }
            catch (Exception e)
            {
                logger.LogErrorFromException(e, true);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
            if (continueOnFailures)
            {
                result.Status = !logger.HasLoggedErrors;
            }
            else if (reader == null)
            {
                result.Status = false;
            }
            else
            {
                result.Status = reader.FailuresCount == 0 && !logger.HasLoggedErrors;
            }
            return result;
        }

        /// <summary>
        /// Creates XML import file
        /// </summary>
        /// <returns>Path to xml file to import</returns>
        protected abstract string CreateXmlImport();
    }
}