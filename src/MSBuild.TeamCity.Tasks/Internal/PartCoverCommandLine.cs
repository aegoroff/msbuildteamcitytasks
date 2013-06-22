/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System.Collections;
using System.Collections.Generic;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    /// Represents PartCover command line object that creates correct command line to pass the tool.
    /// </summary>
    public sealed class PartCoverCommandLine : CommandLine
    {
        private const string TargetOpt = "target";
        private const string TargetWorkDirOpt = "target-work-dir";
        private const string TargetArgumentsOpt = "target-args";
        private const string OutputOpt = "output";
        private const string IncludeOpt = "include";
        private const string ExcludeOpt = "exclude";

        ///<summary>
        /// Initializes a new instance of the <see cref="PartCoverCommandLine"/> class
        ///</summary>
        public PartCoverCommandLine()
        {
            Includes = new List<string>();
            Excludes = new List<string>();
        }

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
        /// Gets include rules
        /// </summary>
        /// <remarks>
        /// Common form for rules is [&lt;assembly_regexp&gt;]&lt;class_regexp&gt;<br/>
        /// Regexp can contains digits, letters, asterisks and pluses. 
        /// Asterisk means zero or more letters and digits in template, plus means inner state of classes.
        /// </remarks>
        public IList<string> Includes { get; private set; }

        /// <summary>
        /// Gets exclude rules
        /// </summary>
        /// <remarks>
        /// Common form for rules is [&lt;assembly_regexp&gt;]&lt;class_regexp&gt;<br/>
        /// Regexp can contains digits, letters, asterisks and pluses. 
        /// Asterisk means zero or more letters and digits in template, plus means inner state of classes.
        /// </remarks>
        public IList<string> Excludes { get; private set; }

        /// <summary>
        /// Gets option's value separator that separates oprion value from option itself
        /// </summary>
        protected override string OptionValueSeparator
        {
            get { return Space; }
        }

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

            foreach (var include in Includes)
            {
                yield return new DictionaryEntry(IncludeOpt, include);
            }
            foreach (var exclude in Excludes)
            {
                yield return new DictionaryEntry(ExcludeOpt, exclude);
            }
        }
    }
}