/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Base class of all TC messages.
	/// </summary>
	public abstract class TeamCityMessage
	{
		private readonly IList<MessageAttribute> _attributes = new List<MessageAttribute>();
		
		/// <summary>
		/// Gets message name
		/// </summary>
		protected abstract string Message { get; }

		/// <summary>
		/// Gets message attributes list
		/// </summary>
		protected IList<MessageAttribute> Attributes
		{
			[DebuggerStepThrough]
			get { return _attributes; }
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
			StringBuilder sb = new StringBuilder();
			foreach ( MessageAttribute attribute in _attributes )
			{
				sb.Append(attribute.ToString());
				sb.Append(' ');
			}
			if ( sb.Length > 0 )
			{
				sb.Remove(sb.Length - 1, 1);
			}
			return string.Format(CultureInfo.CurrentCulture, "##teamcity[{0} {1}]", Message, sb);
		}
	}
}