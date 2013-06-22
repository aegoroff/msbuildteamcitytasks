/*
 * Created by: egr
 * Created at: 09.03.2012
 * © 2007-2012 Alexander Egorov
 */

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    /// Represents OpenCover command line object that creates correct command line to pass the tool.
    /// </summary>
    public class OpenCoverCommandLine : CommandLine
    {
        #region Constants and Fields

        private const string FilterOpt = "filter";
        private const string OutputOpt = "output";
        private const string TargetArgumentsOpt = "targetargs";
        private const string TargetOpt = "target";
        private const string TargetWorkDirOpt = "targetdir";

        ///<summary>
        /// Initializes a new instance of the <see cref="OpenCoverCommandLine"/> class
        ///</summary>
        public OpenCoverCommandLine()
        {
            Filter = new List<string>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets path to executable file to count coverage (it's usuallty nunit-console.exe)
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets path to working directory to target process
        /// </summary>
        public string TargetWorkDir { get; set; }

        /// <summary>
        /// Gets or sets arguments for target process
        /// </summary>
        public string TargetArguments { get; set; }

        /// <summary>
        /// Gets or sets path to output file for writing result xml
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// Gets coverage filter
        /// </summary>
        /// <remarks>
        /// A list of filters to apply to selectively include or exclude assemblies and classes from coverage results. 
        /// Filters have their own format ±[assembly-filter]class-filter. 
        /// If no filter(s) are supplied then a default include all filter is applied +[*]*. 
        /// As can be seen you can use an * as a wildcard. Also an exclusion filter (-) takes precedence over an inclusion filter (+).
        /// </remarks>
        public IList<string> Filter { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether to escape parameter with the option itself.
        /// </summary>
        protected override bool EscapeWithTheOptionItself
        {
            [DebuggerStepThrough]
            get { return true; }
        }

        /// <summary>
        /// Gets option's prefix
        /// </summary>
        protected override string OptionPrefix
        {
            [DebuggerStepThrough]
            get { return "-"; }
        }

        /// <summary>
        /// Gets option's value separator that separates option value from option itself
        /// </summary>
        protected override string OptionValueSeparator
        {
            [DebuggerStepThrough]
            get { return ":"; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Enumerates all possible options
        /// </summary>
        /// <returns>All possible options' pairs</returns>
        protected override IEnumerable<DictionaryEntry> EnumerateOptions()
        {
            yield return new DictionaryEntry(TargetOpt, Target);
            yield return new DictionaryEntry(TargetWorkDirOpt, TargetWorkDir);
            yield return new DictionaryEntry(TargetArgumentsOpt, TargetArguments);
            yield return new DictionaryEntry(OutputOpt, Output);
            yield return new DictionaryEntry(FilterOpt, Filter.Join(" "));
        }

        #endregion
    }
}