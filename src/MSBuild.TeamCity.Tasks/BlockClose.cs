/*
 * Created by: egr
 * Created at: 02.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Collections.Generic;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Block closing message. Blocks are used to group several messages in the build log.
	///</summary>
	/// <example>Closes block
	/// <code><![CDATA[
	/// <BlockClose Name="b1" />
	/// ]]></code>
	/// Closes block full example (with all optional attributes)
	/// <code><![CDATA[
	/// <BlockClose
	///		IsAddTimestamp="true"
	///		FlowId="1"
	///		Name="b1"
	/// />
	/// ]]></code>
	/// </example>
	public class BlockClose : BlockTask
	{
		///<summary>
		/// Initializes a new instance of the <see cref="BlockClose"/> class
		///</summary>
		public BlockClose()
		{
		}

		///<summary>
		/// Initializes a new instance of the <see cref="BlockClose"/> class using 
		/// logger specified
		///</summary>
		///<param name="logger"><see cref="ILogger"/> implementation</param>
		public BlockClose( ILogger logger )
			: base(logger)
		{
		}

		/// <summary>
		/// Reads TeamCity messages
		/// </summary>
		/// <returns>TeamCity messages list</returns>
		protected override IEnumerable<TeamCityMessage> ReadMessages()
		{
			yield return new BlockCloseTeamCityMessage(Name);
		}
	}
}