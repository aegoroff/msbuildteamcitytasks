/*
 * Created by: egr
 * Created at: 01.05.2009
 * © 2007-2011 Alexander Egorov
 */

using System.Text;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    /// Represents TeamCity message attribute
    /// </summary>
    public class MessageAttributeItem
    {
        private const char ValueSeparator = '\'';

        ///<summary>
        /// Initializes a new instance of the <see cref="MessageAttributeItem"/> class
        ///</summary>
        public MessageAttributeItem()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="MessageAttributeItem"/> class using value specified
        ///</summary>
        /// <param name="value">Attribute value</param>
        public MessageAttributeItem(string value) : this(null, value)
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="MessageAttributeItem"/> class using name and value specified.
        ///</summary>
        /// <param name="name">Attribute name</param>
        /// <param name="value">Attribute value</param>
        public MessageAttributeItem(string name, string value)
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
        /// Returns a <see cref="string"/> that represents the current <see cref="MessageAttributeItem"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents the current <see cref="MessageAttributeItem"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Value))
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
            {
                sb.Append(Name).Append("=");
            }

            sb.Append(ValueSeparator).Append(EscapeValue()).Append(ValueSeparator);
            return sb.ToString();
        }

        ///<summary>
        /// Overriden. Compares two instances of <see cref="MessageAttributeItem"/> class.
        ///</summary>
        ///<param name="item1">The first attribute to compare</param>
        ///<param name="item2">The second attribute to compare</param>
        ///<returns>
        /// true if item1 is the same instance as item2 or if item1.Equals(item2) returns true; otherwise, false.
        /// </returns>
        public static bool operator ==(MessageAttributeItem item1, MessageAttributeItem item2)
        {
            if (ReferenceEquals(item1, item2))
            {
                return true;
            }
            if (ReferenceEquals(item1, null))
            {
                return false;
            }
            if (ReferenceEquals(item2, null))
            {
                return false;
            }
            return item1.Name == item2.Name;
        }

        ///<summary>
        /// Overriden. Compares two instances of <see cref="MessageAttributeItem"/> class.
        ///</summary>
        ///<param name="item1">The first attribute to compare</param>
        ///<param name="item2">The second attribute to compare</param>
        ///<returns>
        /// true if item1 is not the same instance as item2 or if item1.Equals(item2) returns false; otherwise, true.
        /// </returns>
        public static bool operator !=(MessageAttributeItem item1, MessageAttributeItem item2)
        {
            return !(item1 == item2);
        }

        ///<summary>
        /// Determines whether the specified <see cref="MessageAttributeItem"/> is equal to the current <see cref="MessageAttributeItem"/>.
        ///</summary>
        ///<param name="other">The <see cref="MessageAttributeItem"/> to compare with the current <see cref="MessageAttributeItem"/>.</param>
        ///<returns>
        /// true if the specified <see cref="MessageAttributeItem"/> is equal to the current <see cref="MessageAttributeItem"/>; otherwise, false.
        /// </returns>
        public bool Equals(MessageAttributeItem other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.Name, Name);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="MessageAttributeItem"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="System.Object"/> is equal to the current <see cref="MessageAttributeItem"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="MessageAttributeItem"/>. 
        ///                 </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof(MessageAttributeItem))
            {
                return false;
            }
            return Equals((MessageAttributeItem) obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="MessageAttributeItem"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }

        /// <summary>
        /// Escapes input string by replacing special symbols that cannot be unescaped in attribute value
        /// </summary>
        /// <returns>Properly escaped string</returns>
        /// <remarks>
        /// For more info about escaping see http://www.jetbrains.net/confluence/display/TCD4/Build+Script+Interaction+with+TeamCity
        /// </remarks>
        private string EscapeValue()
        {
            StringBuilder sb = new StringBuilder(Value);
            sb.Replace("|", "||").Replace("'", "|'").Replace("]", "|]").Replace("\n", "|n").Replace("\r", "|r");
            return sb.ToString();
        }
    }
}