/*
 * Created by: egr
 * Created at: 27.01.2011
 * © 2007-2013 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Indicates that the test "test.name" is present but was not run (was ignored). 
    /// For an ignored test, report the <see cref="TestIgnored"/> message between 
    /// corresponding <see cref="TestStarted"/> and <see cref="TestFinished"/> messages. 
    /// <see cref="TestIgnored"/> message can be reported without 
    /// matching <see cref="TestStarted"/> and <see cref="TestFinished"/> messages.
    ///</summary>
    /// <example>Ignores a test
    /// <code><![CDATA[
    /// <TestIgnored Name="test.name" Message="Ignore comment" />
    /// ]]></code>
    /// Ignores a test full example (with all optional attributes)
    /// <code><![CDATA[
    /// <TestIgnored
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Name="test.name"
    ///     Message="Ignore comment"
    /// />
    /// ]]></code>
    /// </example>
    public class TestIgnored : TeamCityTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestIgnored"/> class
        /// </summary>
        public TestIgnored()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestIgnored"/> class using 
        /// logger specified
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> implementation</param>
        public TestIgnored(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Gets or sets test name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets ignore comment
        /// </summary>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new TestIgnoredTeamCityMessage(Name, Message);
        }
    }
}