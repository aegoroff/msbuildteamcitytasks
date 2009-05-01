/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Base class of all TC messages.
	/// </summary>
	public class TeamCityMessage
	{
		/// <summary>
		/// Gets or sets message to output
		/// </summary>
		protected string Message { get; set; }

		/// <summary>
		/// Creates new message instance
		/// </summary>
		public TeamCityMessage()
		{
		}
		
		/// <summary>
		/// Creates new message instance using message specified.
		/// </summary>
		/// <param name="message">Raw message that isn't escaped.</param>
		/// <remarks>
		/// For more info about escaping see http://www.jetbrains.net/confluence/display/TCD4/Build+Script+Interaction+with+TeamCity
		/// </remarks>
		public TeamCityMessage(string message)
		{
			Message = message;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return string.Format("##teamcity[{0}]", Message);
		}

		/// <summary>
		/// Escapes input string by replacing special symbols that cannot be unescaped in attribute value
		/// </summary>
		/// <param name="input">Input string to escape</param>
		/// <returns>Properly escaped string</returns>
		protected string Escape(string input)
		{
			return input.Replace("|", "||").Replace("'", "|'").Replace("]", "|]").Replace("\n", "|n").Replace("\r", "|r");
		}
	}
}