/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2011 Alexander Egorov
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
        private readonly ILogger _logger;
        private readonly bool _continueOnFailures;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleTestsImporter"/> class
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="continueOnFailures">Whether to continue build in case of failing tests</param>
        protected GoogleTestsImporter( ILogger logger, bool continueOnFailures )
        {
            _logger = logger;
            _continueOnFailures = continueOnFailures;
        }

        /// <summary>
        /// Runs import
        /// </summary>
        /// <returns>
        /// The new instance of <see cref="ExecutionResult"/> structure.
        /// </returns>
        public ExecutionResult Import()
        {
            ExecutionResult result = new ExecutionResult();
            GoogleTestXmlReader reader = null;
            try
            {
                string xmlPath = CreateXmlImport();

                reader = new GoogleTestXmlReader(xmlPath);
                reader.Read();
                result.Messages = new List<TeamCityMessage>
                                      {
                                          new ImportDataTeamCityMessage(ImportType.Junit, xmlPath)
                                      };
            }
            catch ( Exception e )
            {
                _logger.LogError(e.ToString());
            }
            finally
            {
                if ( reader != null )
                {
                    reader.Dispose();
                }
            }
            if ( _continueOnFailures )
            {
                result.Status = !_logger.HasLoggedErrors;
            }
            else if ( reader == null )
            {
                result.Status = false;
            }
            else
            {
                result.Status = reader.FailuresCount == 0 && !_logger.HasLoggedErrors;
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