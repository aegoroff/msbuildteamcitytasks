/*
 * Created by: egr
 * Created at: 27.01.2011
 * © 2007-2011 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Test output reporting (to be shown as the test result if the test fails). 
    /// A test should have no more then one <see cref="TestStdOut"/> message. 
    /// The messages should appear inside <see cref="TestStarted"/> and <see cref="TestFinished"/> messages with the 
    /// matching name attributes.
    ///</summary>
    /// <example>Output test data
    /// <code><![CDATA[
    /// <TestStdOut Name="test.name" Out="output" />
    /// ]]></code>
    /// Output test data full example (with all optional attributes)
    /// <code><![CDATA[
    /// <TestStdOut
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Name="test.name"
    ///     Out="output"
    /// />
    /// ]]></code>
    /// </example>
    public class TestStdOut : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="TestStdOut"/> class
        ///</summary>
        public TestStdOut()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="TestStdOut"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public TestStdOut(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Gets or sets test name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets test output reporting (to be shown as the test result if the test fails)
        /// </summary>
        [Required]
        public string Out { get; set; }

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new TestStdOutTeamCityMessage(Name, Out);
        }
    }
}