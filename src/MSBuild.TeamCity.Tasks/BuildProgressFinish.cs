/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    /// Writes progress finish message into TeamCity log
    /// </summary>
    /// <example>Writes progress finish message into TeamCity log
    /// <code><![CDATA[
    /// <BuildProgressFinish Message="Message text" />
    /// ]]></code>
    /// Writes progress finish message into TeamCity log full example (with all optional attributes)
    /// <code><![CDATA[
    /// <BuildProgressFinish
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Message="Message text"
    /// />
    /// ]]></code>
    /// </example>
    public class BuildProgressFinish : BuildProgressTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="BuildProgressFinish"/> class
        ///</summary>
        public BuildProgressFinish()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="BuildProgressFinish"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public BuildProgressFinish( ILogger logger )
            : base(logger)
        {
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string MessageName
        {
            get { return "progressFinish"; }
        }
    }
}