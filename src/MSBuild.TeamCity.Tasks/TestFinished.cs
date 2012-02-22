/*
 * Created by: egr
 * Created at: 16.01.2011
 * © 2007-2012 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Test finish message.
    ///</summary>
    /// <example>Finishes a test
    /// <code><![CDATA[
    /// <TestFinished Name="test.name" Duration="1.0" />
    /// ]]></code>
    /// Finishes a test full example (with all optional attributes)
    /// <code><![CDATA[
    /// <TestFinished
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Name="test.name"
    ///     Duration="1.0"
    /// />
    /// ]]></code>
    /// </example>
    public class TestFinished : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="TestFinished"/> class
        ///</summary>
        public TestFinished()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="TestFinished"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public TestFinished(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Gets or sets test name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets test's duration in seconds
        /// </summary>
        [Required]
        public double Duration { get; set; }

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new TestFinishTeamCityMessage(Name, Duration);
        }
    }
}