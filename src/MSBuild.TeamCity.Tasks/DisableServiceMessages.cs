/*
 * Created by: egr
 * Created at: 16.10.2010
 * � 2007-2015 Alexander Egorov
 */

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    ///     Disables message processing
    /// </summary>
    /// <remarks>
    ///     If you need for some reason to disable searching for service messages in output,
    ///     you can disable service messages search.
    /// </remarks>
    /// <example>
    ///     Disables message processing
    ///     <code><![CDATA[
    ///  <DisableServiceMessages/>
    ///  ]]></code>
    /// </example>
    public class DisableServiceMessages : TeamCityTask
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DisableServiceMessages" /> class
        /// </summary>
        public DisableServiceMessages()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DisableServiceMessages" /> class using
        ///     logger specified
        /// </summary>
        /// <param name="logger"><see cref="ILogger" /> implementation</param>
        public DisableServiceMessages(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        ///     Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
            MessageId = "MSBuild.TeamCity.Tasks.Messages.AttributeLessMessage.#ctor(System.String)")]
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new AttributeLessMessage("disableServiceMessages");
        }
    }
}