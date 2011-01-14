/*
 * Created by: egr
 * Created at: 14.01.2011
 * © 2007-2011 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Test suite start message.
    ///</summary>
    /// <example>Starts test suite
    /// <code><![CDATA[
    /// <TestSuiteStarted Name="suite.name" />
    /// ]]></code>
    /// Starts test suite full example (with all optional attributes)
    /// <code><![CDATA[
    /// <TestSuiteStarted
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Name="suite.name"
    /// />
    /// ]]></code>
    /// </example>
    public class TestSuiteStarted : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="TestSuiteStarted"/> class
        ///</summary>
        public TestSuiteStarted()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="TestSuiteStarted"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public TestSuiteStarted(ILogger logger)
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
            yield return new TestSuiteStartTeamCityMessage(Name);
        }
    }
}