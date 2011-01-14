/*
 * Created by: egr
 * Created at: 02.05.2009
 * © 2007-2011 Alexander Egorov
 */

using System.Collections.Generic;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Block opening message. Blocks are used to group several messages in the build log.
    ///</summary>
    /// <example>Opens block
    /// <code><![CDATA[
    /// <BlockOpen Name="b1" />
    /// ]]></code>
    /// Opens block full example (with all optional attributes)
    /// <code><![CDATA[
    /// <BlockOpen
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Name="b1"
    /// />
    /// ]]></code>
    /// </example>
    public class BlockOpen : BlockTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="BlockOpen"/> class
        ///</summary>
        public BlockOpen()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="BlockOpen"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public BlockOpen( ILogger logger )
            : base(logger)
        {
        }

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new BlockOpenTeamCityMessage(Name);
        }
    }
}