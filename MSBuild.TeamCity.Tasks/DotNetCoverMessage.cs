/*
 * Created by: egr
 * Created at: 07.09.2010
 * © 2007-2010 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Represents dotNetCoverage TeamCity message
	/// </summary>
	public class DotNetCoverMessage : TeamCityMessage
	{
		///<summary>
		/// Initializes a new instance of the <see cref="DotNetCoverMessage"/> class
		///</summary>
		///<param name="value">key parameter value</param>
		public DotNetCoverMessage( string value )
		{
			Value = value;
			Attributes.Add(new MessageAttributeItem("key", Value));
		}

		/// <summary>
		/// Gets parameter value
		/// </summary>
		public string Value { get; private set; }

		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string Message
		{
			get { return "dotNetCoverage"; }
		}
	}
}