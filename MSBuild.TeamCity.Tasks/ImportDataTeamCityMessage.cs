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
		private const string PartCover = "partcover";
		private const string Ncover = "ncover";
		private const string Ncover3 = "ncover3";

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

		///<summary>
		/// Initializes a new instance of the <see cref="ImportDataTeamCityMessage"/> class using type and path specified
		///</summary>
		///<param name="type">Data type. FxCop for example</param>
		///<param name="path">Full path to file</param>
		///<param name="tool">Here the tool name value can be partcover, ncover, or ncover3, depending on selected coverage tool in the coverage settings.</param>
		public ImportDataTeamCityMessage( ImportType type, string path, DotNetCoverateTool tool ) : this(type, path)
		{
			if ( type != ImportType.DotNetCoverage )
			{
				throw new NotSupportedException();
			}
			Attributes.Add(new MessageAttributeItem("tool", ToString(tool)));
		}

		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string Message
		{
			get { return "importData"; }
		}

		internal static DotNetCoverateTool ToDotNetCoverateTool( string type )
		{
			switch ( type )
			{
				case PartCover:
					return DotNetCoverateTool.PartCover;
				case Ncover:
					return DotNetCoverateTool.Ncover;
				case Ncover3:
					return DotNetCoverateTool.Ncover3;
				default:
					throw new NotSupportedException();
			}
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
				case ImportType.DotNetCoverage:
					return "dotNetCoverage";
				case ImportType.Mstest:
					return "mstest";
				default:
					throw new NotSupportedException();
			}
		}

		private static string ToString( DotNetCoverateTool type )
		{
			switch ( type )
			{
				case DotNetCoverateTool.PartCover:
					return PartCover;
				case DotNetCoverateTool.Ncover:
					return Ncover;
				case DotNetCoverateTool.Ncover3:
					return Ncover3;
				default:
					throw new NotSupportedException();
			}
		}
	}
}