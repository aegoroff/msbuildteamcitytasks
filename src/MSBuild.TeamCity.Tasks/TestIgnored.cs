/*
 * Created by: egr
 * Created at: 27.01.2011
 * © 2007-2011 Alexander Egorov
 */

using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Test ignore message
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
        public TestIgnored( ILogger logger )
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
    }
}