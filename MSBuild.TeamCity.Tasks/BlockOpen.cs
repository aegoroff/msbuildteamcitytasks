/*
 * Created by: egr
 * Created at: 02.05.2009
 * © 2007-2009 Alexander Egorov
 */

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
	///		IsAddTimestamp="true"
	///		FlowId="1"
	///		Name="b1"
	/// />
	/// ]]></code>
	/// </example>
	public class BlockOpen : BlockTask
	{
		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			Write(new BlockOpenTeamCityMessage(Name));
			return true;
		}
	}
}