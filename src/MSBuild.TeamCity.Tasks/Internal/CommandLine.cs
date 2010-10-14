/*
 * Created by: egr
 * Created at: 10.10.2010
 * © 2007-2010 Alexander Egorov
 */

using System.Collections.Generic;
using System.Linq;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    /// Represents base class of all comand line builders
    /// </summary>
    public abstract class CommandLine
    {
        /// <summary>
        /// Space string
        /// </summary>
        protected const string Space = " ";

        private const string OptionPrefix = "--";
        private const string EscapeSymbol = "\"";

        /// <summary>
        /// Gets option's value separator that separates oprion value from option itself
        /// </summary>
        protected abstract string OptionValueSeparator { get; }

        /// <summary>
        /// Gets a value indicating whether to output option in case of value isn't presented
        /// </summary>
        protected virtual bool IsOutputInCaseOfEmptyValue
        {
            get { return false; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Join(Space, CreateOptions().ToArray());
        }

        /// <summary>
        /// Enumerates all possible options
        /// </summary>
        /// <returns>All possible options' pairs</returns>
        protected abstract IEnumerable<KeyValuePair<string, string>> EnumerateOptions();

        /// <summary>
        /// Creates properly escaped option to pass to an aplication
        /// </summary>
        /// <param name="option">option name</param>
        /// <param name="value">option value</param>
        /// <returns>Properly escaped option</returns>
        protected string CreateOption( string option, string value )
        {
            if ( string.IsNullOrEmpty(value) )
            {
                return OptionPrefix + option;
            }
            string v = value.Contains(Space) ? EscapeSymbol + value + EscapeSymbol : value;
            return OptionPrefix + option + OptionValueSeparator + v;
        }

        private IEnumerable<string> CreateOptions()
        {
            return
                from option in EnumerateOptions()
                where
                    ( !string.IsNullOrEmpty(option.Value) && !IsOutputInCaseOfEmptyValue ) || IsOutputInCaseOfEmptyValue
                select CreateOption(option.Key, option.Value);
        }
    }
}