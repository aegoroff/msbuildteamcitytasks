/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Represents importData TeamCity message that imports data from third party sources.
	///</summary>
	public class ImportDataTeamCityMessage : TeamCityMessage
	{
		///<summary>
		/// Initializes a new instance of the <see cref="ImportDataTeamCityMessage"/> class using <see cref="ImportType"/> value and path specified
		///</summary>
		///<param name="type">Data type. FxCop for example</param>
		///<param name="path">Full path to file</param>
		public ImportDataTeamCityMessage( ImportType type, string path ) : this(ToString(type), path)
		{
		}

		///<summary>
		/// Initializes a new instance of the <see cref="ImportDataTeamCityMessage"/> class using type and path specified
		///</summary>
		///<param name="type">Data type. FxCop for example</param>
		///<param name="path">Full path to file</param>
		public ImportDataTeamCityMessage( string type, string path )
		{
			Attributes.Add(new MessageAttributeItem("type", type));
			Attributes.Add(new MessageAttributeItem("path", path));
		}

		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string Message
		{
			get { return "importData"; }
		}

		private static string ToString( ImportType type )
		{
			switch ( type )
			{
				case ImportType.Junit:
					return "junit";
				case ImportType.Surefire:
					return "surefire";
				case ImportType.Nunit:
					return "nunit";
				case ImportType.FindBugs:
					return "findBugs";
				case ImportType.Pmd:
					return "pmd";
				case ImportType.FxCop:
					return "FxCop";
				default:
					throw new NotSupportedException();
			}
		}
	}
}