/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using System;
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

		///<summary>
		/// Whether to add timestamt to the message. False by default
		///</summary>
		public bool IsAddTimeStamp { get; set; }

		/// <summary>
		/// Gets or sets message's flowId
		/// </summary>
		public string FlowId { get; set; }

		/// <summary>
		/// Returns a <see cref="String"/> that represents the current <see cref="MessageAttribute"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents the current <see cref="MessageAttribute"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			MessageAttribute timestamp = new MessageAttribute("timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"));
			MessageAttribute flowId = new MessageAttribute("flowId", FlowId);
			if ( IsAddTimeStamp && !_attributes.Contains(timestamp) )
			{
				_attributes.Add(timestamp);
			}
			if ( !string.IsNullOrEmpty(FlowId) && !_attributes.Contains(flowId) )
			{
				_attributes.Add(flowId);
			}
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