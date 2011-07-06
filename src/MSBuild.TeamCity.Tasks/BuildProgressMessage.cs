/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2011 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    /// Writes progress message into TeamCity log
    /// </summary>
    /// <example>Writes progress message into TeamCity log
    /// <code><![CDATA[
    /// <BuildProgressMessage Message="Message text" />
    /// ]]></code>
    /// Writes progress message into TeamCity log full example (with all optional attributes)
    /// <code><![CDATA[
    /// <BuildProgressMessage
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Message="Message text"
    /// />
    /// ]]></code>
    /// </example>
    public class BuildProgressMessage : BuildProgressTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="BuildProgressMessage"/> class
        ///</summary>
        public BuildProgressMessage()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="BuildProgressMessage"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public BuildProgressMessage(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string MessageName
        {
            get { return "progressMessage"; }
        }
    }
}