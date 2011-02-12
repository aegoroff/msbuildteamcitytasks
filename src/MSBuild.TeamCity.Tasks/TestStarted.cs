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
    /// Indicates that test has been started. 
    /// Optional CaptureStandardOutput boolean attribute can be present in the <see cref="TestStarted"/> service message to 
    /// identify whether to include output received between <see cref="TestStarted"/> and <see cref="TestFinished"/> messages 
    /// as test output or not. The default value is false. 
    /// If true standard output is reported as test output and standard error as test error output.
    ///</summary>
    /// <example>Starts a test
    /// <code><![CDATA[
    /// <TestStarted Name="test.name" />
    /// ]]></code>
    /// Starts a test and captures standard output
    /// <code><![CDATA[
    /// <TestStarted
    ///     Name="test.name"
    ///     CaptureStandardOutput="true"
    /// />
    /// ]]></code>
    /// Starts a test full example (with all optional attributes)
    /// <code><![CDATA[
    /// <TestStarted
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Name="test.name"
    ///     CaptureStandardOutput="true"
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
        /// Gets or sets test name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to identify whether to include output received between 
        /// <see cref="TestStarted"/> and <see cref="TestFinished"/> 
        /// messages as test output or not
        /// </summary>
        public bool CaptureStandardOutput { get; set; }

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new TestStartTeamCityMessage(Name, CaptureStandardOutput);
        }
    }
}