/*
 * Created by: egr
 * Created at: 27.08.2010
 * © 2007-2011 Alexander Egorov
 */

using System.Collections;
using System.Collections.Generic;

namespace MSBuild.TeamCity.Tasks.Internal
{
    ///<summary>
    /// Helps to build command line arguments string to pass Google test executable
    ///</summary>
    public sealed class GoogleTestCommandLine : CommandLine
    {
        private readonly bool _catchExceptions;
        private readonly bool _runDisabledTests;
        private readonly string _filter;
        private const string OutputOpt = "gtest_output";
        private const string RunDisabledTestsOpt = "gtest_also_run_disabled_tests";
        private const string CatchExceptionsOpt = "gtest_catch_exceptions";
        private const string FilterOpt = "gtest_filter";

        ///<summary>
        /// Initializes a new instance of the <see cref="GoogleTestCommandLine"/> class.
        ///</summary>
        ///<param name="catchExceptions">suppress pop-ups caused by exceptions</param>
        ///<param name="runDisabledTests">run all disabled tests too</param>
        ///<param name="filter">
        /// Run only the tests whose name matches one of the positive patterns but 
        /// none of the negative patterns. '?' matches any single character; '*' 
        /// matches any substring; ':' separates two patterns.
        /// </param>
        public GoogleTestCommandLine(bool catchExceptions, bool runDisabledTests, string filter)
        {
            _catchExceptions = catchExceptions;
            _runDisabledTests = runDisabledTests;
            _filter = filter;
        }

        /// <summary>
        /// Gets option's value separator that separates oprion value from option itself
        /// </summary>
        protected override string OptionValueSeparator
        {
            get { return "="; }
        }

        /// <summary>
        /// Gets whether to output option in case of value isn't presented
        /// </summary>
        protected override bool IsOutputInCaseOfEmptyValue
        {
            get { return true; }
        }

        /// <summary>
        /// Enumerates all possible options
        /// </summary>
        /// <returns>All possible options' pairs</returns>
        protected override IEnumerable<DictionaryEntry> EnumerateOptions()
        {
            yield return new DictionaryEntry(OutputOpt, "xml:");
            if (_runDisabledTests)
            {
                yield return new DictionaryEntry(RunDisabledTestsOpt, string.Empty);
            }
            if (_catchExceptions)
            {
                yield return new DictionaryEntry(CatchExceptionsOpt, string.Empty);
            }
            if (!string.IsNullOrEmpty(_filter))
            {
                yield return new DictionaryEntry(FilterOpt, _filter);
            }
        }
    }
}