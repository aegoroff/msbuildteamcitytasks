/*
 * Created by: egr
 * Created at: 02.05.2009
 * © 2007-2015 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    ///     Represent base class of all Block* tasks. Cannot be used directly (because it's abstract) in MSBuild script.
    /// </summary>
    public abstract class BlockTask : TeamCityTask
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BlockTask" /> class
        /// </summary>
        protected BlockTask()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BlockTask" /> class using
        ///     logger specified
        /// </summary>
        /// <param name="logger"><see cref="ILogger" /> implementation</param>
        protected BlockTask(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        ///     Gets or sets block name
        /// </summary>
        [Required]
        public string Name { get; set; }
    }

    /// <summary>
    ///     Block opening message. Blocks are used to group several messages in the build log.
    /// </summary>
    /// <example>
    ///     Opens block
    ///     <code><![CDATA[
    ///  <BlockOpen Name="b1" />
    ///  ]]></code>
    ///     Opens block full example (with all optional attributes)
    ///     <code><![CDATA[
    ///  <BlockOpen
    ///      IsAddTimestamp="true"
    ///      FlowId="1"
    ///      Name="b1"
    ///  />
    ///  ]]></code>
    /// </example>
    public class BlockOpen : BlockTask
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BlockOpen" /> class
        /// </summary>
        public BlockOpen()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BlockOpen" /> class using
        ///     logger specified
        /// </summary>
        /// <param name="logger"><see cref="ILogger" /> implementation</param>
        public BlockOpen(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        ///     Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new BlockOpenTeamCityMessage(this.Name);
        }
    }

    /// <summary>
    ///     Block closing message. Blocks are used to group several messages in the build log.
    /// </summary>
    /// <example>
    ///     Closes block
    ///     <code><![CDATA[
    ///  <BlockClose Name="b1" />
    ///  ]]></code>
    ///     Closes block full example (with all optional attributes)
    ///     <code><![CDATA[
    ///  <BlockClose
    ///      IsAddTimestamp="true"
    ///      FlowId="1"
    ///      Name="b1"
    ///  />
    ///  ]]></code>
    /// </example>
    public class BlockClose : BlockTask
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MSBuild.TeamCity.Tasks.BlockClose" /> class
        /// </summary>
        public BlockClose()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MSBuild.TeamCity.Tasks.BlockClose" /> class using
        ///     logger specified
        /// </summary>
        /// <param name="logger"><see cref="ILogger" /> implementation</param>
        public BlockClose(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        ///     Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new BlockCloseTeamCityMessage(this.Name);
        }
    }
}