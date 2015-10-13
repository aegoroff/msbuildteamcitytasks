/*
 * Created by: egr
 * Created at: 26.12.2013
 * © 2007-2013 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    ///     Plain Data model for import data message
    /// </summary>
    public class ImportDataContext
    {
        /// <summary>
        ///     Gets or sets imported data type.
        /// </summary>
        public ImportType Type { get; set; }

        /// <summary>
        ///     Gets or sets full path to data source file to import data from
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether to enable detailed logging into the build log. False by default
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        ///     Gets or sets whether process all the files matching the path. Otherwise, only those updated during the build (is
        ///     determined by
        ///     last modification timestamp) are processed. False by default
        /// </summary>
        public bool ParseOutOfDate { get; set; }

        /// <summary>
        ///     Gets or sets action that will change output level if no reports matching the path specified were found.<p />
        ///     May take the following values: info (default), nothing, warning, error
        /// </summary>
        public string WhenNoDataPublished { get; set; }
    }
}