/*
 * Created by: egr
 * Created at: 02.05.2009
 * © 2007-2009 Alexander Egorov
 */

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
		/// <summary>
		/// Creates concrete message class
		/// </summary>
		/// <returns>New message instance</returns>
		protected override NamedTeamCityMessage CreateMessage()
		{
			return new BlockCloseTeamCityMessage(Name);
		}
	}
}