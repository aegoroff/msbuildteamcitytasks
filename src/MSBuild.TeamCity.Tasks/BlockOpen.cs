/*
 * Created by: egr
 * Created at: 02.05.2009
 * � 2007-2009 Alexander Egorov
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
		/// Creates concrete message class
		/// </summary>
		/// <returns></returns>
		protected override NamedTeamCityMessage CreateMessage()
		{
			return new BlockOpenTeamCityMessage(Name);
		}
	}
}