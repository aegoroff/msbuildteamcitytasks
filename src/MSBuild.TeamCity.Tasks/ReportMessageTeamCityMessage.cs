/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Represents Reporting Messages For Build Log
	///</summary>
	public class ReportMessageTeamCityMessage : TeamCityMessage
	{
		///<summary>
		/// Initializes a new instance of the <see cref="ReportMessageTeamCityMessage"/> class which status is NORMAL
		///</summary>
		///<param name="text">Message text</param>
		public ReportMessageTeamCityMessage( string text )
		{
			Attributes.Add(new MessageAttributeItem("text", text));
		}

		///<summary>
		/// Initializes a new instance of the <see cref="ReportMessageTeamCityMessage"/> class
		///</summary>
		///<param name="text">Message text</param>
		///<param name="status">The status attribute may take following values: NORMAL, WARNING, FAILURE, ERROR. The default value is NORMAL.</param>
		public ReportMessageTeamCityMessage( string text, string status ) : this(text)
		{
			Attributes.Add(new MessageAttributeItem("status", status));
		}

		///<summary>
		/// Initializes a new instance of the <see cref="ReportMessageTeamCityMessage"/> class
		///</summary>
		///<param name="text">Message text</param>
		///<param name="status">The status attribute may take following values: NORMAL, WARNING, FAILURE, ERROR. The default value is NORMAL.</param>
		///<param name="errorDetails">errorDetails attribute is used only if status is ERROR, in other cases it is ignored.</param>
		public ReportMessageTeamCityMessage( string text, string status, string errorDetails ) : this(text, status)
		{
			Attributes.Add(new MessageAttributeItem("errorDetails", errorDetails));
		}

		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string Message
		{
			get { return "message"; }
		}
	}
}