/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    /// Represents PartCover command line object that creates correct command line to pass the tool.
    /// </summary>
    public class PartCoverCommandLine
    {
        private const string TargetOpt = "target";
        private const string TargetWorkDirOpt = "target-work-dir";
        private const string TargetArgumentsOpt = "target-args";
        private const string OutputOpt = "output";
        private const string IncludeOpt = "include";
        private const string ExcludeOpt = "exclude";
        private const string EscapeSymbol = "\"";
        private const string Space = " ";

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
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            SequenceBuilder<string> sequence = new SequenceBuilder<string>(CreateOptions(), Space);
            return sequence.ToString();
        }

        private IEnumerable<string> CreateOptions()
        {
            return from option in EnumerateOptions()
                   where !string.IsNullOrEmpty(option.Value)
                   select CreateOption(option.Key, option.Value);
        }

        private IEnumerable<KeyValuePair<string, string>> EnumerateOptions()
        {
            yield return new KeyValuePair<string, string>(TargetOpt, Target);
            yield return new KeyValuePair<string, string>(TargetWorkDirOpt, TargetWorkDir);
            yield return new KeyValuePair<string, string>(TargetArgumentsOpt, TargetArguments);
            yield return new KeyValuePair<string, string>(OutputOpt, Output);

            foreach ( string include in Includes )
            {
                yield return new KeyValuePair<string, string>(IncludeOpt, include);
            }
            foreach ( string exclude in Excludes )
            {
                yield return new KeyValuePair<string, string>(ExcludeOpt, exclude);
            }
        }

        private static string CreateOption( string option, string value )
        {
            string v = value.Contains(Space) ? EscapeSymbol + value + EscapeSymbol : value;
            return string.Format(CultureInfo.CurrentCulture, "--{0} {1}", option, v);
        }
    }
}