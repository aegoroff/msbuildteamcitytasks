/*
 * Created by: egr
 * Created at: 01.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Text;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Represents TeamCity message attribute
	/// </summary>
	public class MessageAttribute
	{
		///<summary>
		/// Creates empty attribute instance
		///</summary>
		public MessageAttribute()
		{
		}
		
		///<summary>
		/// Creates new nameless attribute instance using value specified
		///</summary>
		/// <param name="value">Attribute value</param>
		public MessageAttribute(string value) : this(null, value)
		{
		}
		
		///<summary>
		/// Creates new attribute instance using name and value specified.
		///</summary>
		/// <param name="name">Attribute name</param>
		/// <param name="value">Attribute value</param>
		public MessageAttribute(string name, string value)
		{
			Name = name;
			Value = value;
		}

		/// <summary>
		/// Gets or sets attribute name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets attribute value
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Escapes input string by replacing special symbols that cannot be unescaped in attribute value
		/// </summary>
		/// <returns>Properly escaped string</returns>
		/// <remarks>
		/// For more info about escaping see http://www.jetbrains.net/confluence/display/TCD4/Build+Script+Interaction+with+TeamCity
		/// </remarks>
		private string EscapeValue()
		{
			return Value.Replace("|", "||").Replace("'", "|'").Replace("]", "|]").Replace("\n", "|n").Replace("\r", "|r");
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
			if ( string.IsNullOrEmpty(Value) )
			{
				return null;
			}
			const char valueSeparator = '\'';
			StringBuilder sb = new StringBuilder();
			if (!string.IsNullOrEmpty(Name))
			{
				sb.Append(Name);
				sb.Append("=");
			}
			sb.Append(valueSeparator);
			sb.Append(EscapeValue());
			sb.Append(valueSeparator);
			return sb.ToString();
		}
	}
}