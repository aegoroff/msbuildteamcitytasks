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
		public MessageAttribute( string value ) : this(null, value)
		{
		}

		///<summary>
		/// Creates new attribute instance using name and value specified.
		///</summary>
		/// <param name="name">Attribute name</param>
		/// <param name="value">Attribute value</param>
		public MessageAttribute( string name, string value )
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
		/// Returns a <see cref="string"/> that represents the current <see cref="MessageAttribute"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents the current <see cref="MessageAttribute"/>.
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
			if ( !string.IsNullOrEmpty(Name) )
			{
				sb.Append(Name);
				sb.Append("=");
			}
			sb.Append(valueSeparator);
			sb.Append(EscapeValue());
			sb.Append(valueSeparator);
			return sb.ToString();
		}

		///<summary>
		/// Overriden. Compares two instances of <see cref="MessageAttribute"/> class.
		///</summary>
		///<param name="a1">The first attribute to compare</param>
		///<param name="a2">The second attribute to compare</param>
		///<returns>
		/// true if a1 is the same instance as a2 or if a1.Equals(a2) returns true; otherwise, false.
		/// </returns>
		public static bool operator ==( MessageAttribute a1, MessageAttribute a2 )
		{
			if ( ReferenceEquals(a1, a2) )
			{
				return true;
			}
			if ( ReferenceEquals(a1, null) )
			{
				return false;
			}
			if ( ReferenceEquals(a2, null) )
			{
				return false;
			}
			return a1.Name == a2.Name;
		}

		///<summary>
		/// Overriden. Compares two instances of <see cref="MessageAttribute"/> class.
		///</summary>
		///<param name="a1">The first attribute to compare</param>
		///<param name="a2">The second attribute to compare</param>
		///<returns>
		/// true if a1 is not the same instance as a2 or if a1.Equals(a2) returns false; otherwise, true.
		/// </returns>
		public static bool operator !=( MessageAttribute a1, MessageAttribute a2 )
		{
			return !( a1 == a2 );
		}

		///<summary>
		/// Determines whether the specified <see cref="MessageAttribute"/> is equal to the current <see cref="MessageAttribute"/>.
		///</summary>
		///<param name="other">The <see cref="MessageAttribute"/> to compare with the current <see cref="MessageAttribute"/>.</param>
		///<returns>
		/// true if the specified <see cref="MessageAttribute"/> is equal to the current <see cref="MessageAttribute"/>; otherwise, false.
		/// </returns>
		public bool Equals( MessageAttribute other )
		{
			if ( ReferenceEquals(null, other) )
			{
				return false;
			}
			if ( ReferenceEquals(this, other) )
			{
				return true;
			}
			return Equals(other.Name, Name);
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="MessageAttribute"/>.
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="System.Object"/> is equal to the current <see cref="MessageAttribute"/>; otherwise, false.
		/// </returns>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="MessageAttribute"/>. 
		///                 </param>
		/// <filterpriority>2</filterpriority>
		public override bool Equals( object obj )
		{
			if ( ReferenceEquals(null, obj) )
			{
				return false;
			}
			if ( ReferenceEquals(this, obj) )
			{
				return true;
			}
			if ( obj.GetType() != typeof (MessageAttribute) )
			{
				return false;
			}
			return Equals((MessageAttribute) obj);
		}

		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="MessageAttribute"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return ( Name != null ? Name.GetHashCode() : 0 );
		}
	}
}