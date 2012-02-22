/*
 * Created by: egr
 * Created at: 14.01.2011
 * © 2007-2012 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Test suite finish message.
    ///</summary>
    /// <example>Finishes test suite
    /// <code><![CDATA[
    /// <TestSuiteFinished Name="suite.name" />
    /// ]]></code>
    /// Finishes test suite full example (with all optional attributes)
    /// <code><![CDATA[
    /// <TestSuiteFinished
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Name="suite.name"
    /// />
    /// ]]></code>
    /// </example>
    public class TestSuiteFinished : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="TestSuiteFinished"/> class
        ///</summary>
        public TestSuiteFinished()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="TestSuiteFinished"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public TestSuiteFinished(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Gets or sets test suite name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new TestSuiteFinishTeamCityMessage(Name);
        }
    }
}