/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System;
using System.Collections.Generic;
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
            Messages = new List<TeamCityMessage>();
        }

        /// <summary>
        /// Gets messages list
        /// </summary>
        public IList<TeamCityMessage> Messages { get; private set; } 

        /// <summary>
        /// Gets or sets a value indicating whether to enable detailed logging into the build log. False by default
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        /// Gets or sets action that will change output level if no reports matching the path specified were found.<p/>
        /// May take the following values: info (default), nothing, warning, error
        /// </summary>
        public string WhenNoDataPublished { get; set; }

        /// <summary>
        /// Runs import
        /// </summary>
        /// <returns>
        /// The new instance of <see cref="ExecutionResult"/> structure.
        /// </returns>
        public bool Import()
        {
            Messages.Clear();
            var result = false;
            GoogleTestXmlReader reader = null;
            try
            {
                var reportPath = CreateXmlImport();
                reader = new GoogleTestXmlReader(reportPath);
                reader.Read();
                var context = new ImportDataContext
                {
                    Type = ImportType.Gtest,
                    Path = reportPath,
                    Verbose = Verbose,
                    WhenNoDataPublished = WhenNoDataPublished
                };
                Messages.Add(new ImportDataTeamCityMessage(context));
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
                result = !logger.HasLoggedErrors;
            }
            else if (reader == null)
            {
            }
            else
            {
                result = reader.FailuresCount == 0 && !logger.HasLoggedErrors;
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