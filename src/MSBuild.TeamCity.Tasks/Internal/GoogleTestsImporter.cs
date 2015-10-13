/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    ///     Represents Google tests abstract importer
    /// </summary>
    public abstract class GoogleTestsImporter
    {
        private readonly bool continueOnFailures;
        private readonly ILogger logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GoogleTestsImporter" /> class
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="continueOnFailures">Whether to continue build in case of failing tests</param>
        protected GoogleTestsImporter(ILogger logger, bool continueOnFailures)
        {
            this.logger = logger;
            this.continueOnFailures = continueOnFailures;
            this.Messages = new List<TeamCityMessage>();
        }

        /// <summary>
        ///     Gets messages list
        /// </summary>
        public IList<TeamCityMessage> Messages { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether to enable detailed logging into the build log. False by default
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        ///     Gets or sets action that will change output level if no reports matching the path specified were found.<p />
        ///     May take the following values: info (default), nothing, warning, error
        /// </summary>
        public string WhenNoDataPublished { get; set; }

        /// <summary>
        ///     Gets or sets whether process all the files matching the path. Otherwise, only those updated during the build (is
        ///     determined by
        ///     last modification timestamp) are processed. False by default
        /// </summary>
        public bool ParseOutOfDate { get; set; }

        /// <summary>
        ///     Runs import
        /// </summary>
        /// <returns>
        ///     The new instance of <see cref="ExecutionResult" /> structure.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public bool Import()
        {
            this.Messages.Clear();
            var result = false;
            GoogleTestXmlReader reader = null;
            try
            {
                var reportPath = this.CreateXmlImport();
                reader = new GoogleTestXmlReader(reportPath);
                reader.Read();
                var context = new ImportDataContext
                {
                    Type = ImportType.Gtest,
                    Path = reportPath,
                    Verbose = this.Verbose,
                    WhenNoDataPublished = this.WhenNoDataPublished,
                    ParseOutOfDate = this.ParseOutOfDate
                };
                this.Messages.Add(new ImportDataTeamCityMessage(context));
            }
            catch (Exception e)
            {
                this.logger.LogErrorFromException(e, true);
            }
            finally
            {
                reader?.Dispose();
            }
            if (this.continueOnFailures)
            {
                result = !this.logger.HasLoggedErrors;
            }
            else if (reader == null)
            {
            }
            else
            {
                result = reader.FailuresCount == 0 && !this.logger.HasLoggedErrors;
            }
            return result;
        }

        /// <summary>
        ///     Creates XML import file
        /// </summary>
        /// <returns>Path to xml file to import</returns>
        protected abstract string CreateXmlImport();
    }
}