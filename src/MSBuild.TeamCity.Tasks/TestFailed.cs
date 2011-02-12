/*
 * Created by: egr
 * Created at: 02.02.2011
 * © 2007-2011 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Indicates that the test with the name "test.name" has failed. Only one <see cref="TestFailed"/> messages should 
    /// appear for a given test name. actual and expected attributes can be used for 
    /// reporting comparison failure. The values will be used when opening the test in the IDE.
    ///</summary>
    /// <example>Finishes a test
    /// <code><![CDATA[
    /// <TestFailed 
    ///     Name="test.name"
    ///     Message="failure message"
    ///     Details="message and stack trace"
    /// />
    /// ]]></code>
    /// Failed test full example (with all optional attributes). Actual and Expected attributes can be used for 
    /// reporting comparison failure. The values will be used when opening the test in the IDE.
    /// <code><![CDATA[
    /// <TestFailed
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Name="test.name"
    ///     Message="failure message"
    ///     Details="message and stack trace"
    ///     Actual="actual value"
    ///     Expected="expected value"
    /// />
    /// ]]></code>
    /// </example>
    public class TestFailed : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="TestFailed"/> class
        ///</summary>
        public TestFailed()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="TestFailed"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public TestFailed( ILogger logger )
            : base(logger)
        {
        }

        /// <summary>
        /// Gets or sets test name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets test failure message
        /// </summary>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets test failure details
        /// </summary>
        [Required]
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets test actual value
        /// </summary>
        public string Actual { get; set; }

        /// <summary>
        /// Gets or sets test expected value
        /// </summary>
        public string Expected { get; set; }

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new TestFailedTeamCityMessage(Name, Message, Details)
                             {
                                 Expected = Expected,
                                 Actual = Actual
                             };
        }
    }
}