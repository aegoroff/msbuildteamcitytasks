/*
 * Created by: egr
 * Created at: 16.01.2011
 * © 2007-2011 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Test start message.
    ///</summary>
    /// <example>Starts a test
    /// <code><![CDATA[
    /// <TestStarted Name="test.name" />
    /// ]]></code>
    /// Starts a test full example (with all optional attributes)
    /// <code><![CDATA[
    /// <TestStarted
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Name="test.name"
    /// />
    /// ]]></code>
    /// </example>
    public class TestStarted : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="TestStarted"/> class
        ///</summary>
        public TestStarted()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="TestStarted"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public TestStarted(ILogger logger)
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
            yield return new TestStartTeamCityMessage(Name);
        }
    }
}