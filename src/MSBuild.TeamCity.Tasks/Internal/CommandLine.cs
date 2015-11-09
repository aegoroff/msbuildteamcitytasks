/*
 * Created by: egr
 * Created at: 10.10.2010
 * © 2007-2015 Alexander Egorov
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    ///     Represents base class of all comand line builders
    /// </summary>
    public abstract class CommandLine
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Join(Space, this.CreateOptions());
        }

        #endregion

        #region Constants and Fields

        /// <summary>
        ///     Space string
        /// </summary>
        protected const string Space = " ";

        private const string EscapeSymbol = "\"";

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether to escape parameter with the option itself. False by default
        /// </summary>
        protected virtual bool EscapeWithTheOptionItself => false;

        /// <summary>
        ///     Gets a value indicating whether to output option in case of value isn't presented
        /// </summary>
        protected virtual bool IsOutputInCaseOfEmptyValue => false;

        /// <summary>
        ///     Gets option's prefix
        /// </summary>
        protected virtual string OptionPrefix => "--";

        /// <summary>
        ///     Gets option's value separator that separates option value from option itself
        /// </summary>
        protected abstract string OptionValueSeparator { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Enumerates all possible options
        /// </summary>
        /// <returns>All possible options' pairs</returns>
        protected abstract IEnumerable<DictionaryEntry> EnumerateOptions();

        /// <summary>
        ///     Creates properly escaped option to pass to an aplication
        /// </summary>
        /// <param name="option">option name</param>
        /// <param name="value">option value</param>
        /// <returns>Properly escaped option</returns>
        private string CreateOption(object option, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return this.OptionPrefix + option;
            }
            if (!value.Contains(Space))
            {
                return this.CreatePlainValuedOption(option, value);
            }
            if (this.EscapeWithTheOptionItself)
            {
                return EscapeSymbol + this.CreatePlainValuedOption(option, value) + EscapeSymbol;
            }
            return this.OptionPrefix + option + this.OptionValueSeparator + EscapeSymbol + value + EscapeSymbol;
        }

        private string CreatePlainValuedOption(object option, string value)
        {
            return this.OptionPrefix + option + this.OptionValueSeparator + value;
        }

        private IEnumerable<string> CreateOptions()
        {
            return
                from option in this.EnumerateOptions()
                where
                    (!string.IsNullOrEmpty(option.Value as string) && !this.IsOutputInCaseOfEmptyValue) ||
                    this.IsOutputInCaseOfEmptyValue || option.Value is bool && (bool)option.Value
                select this.CreateOption(option.Key, option.Value as string);
        }

        #endregion
    }
}