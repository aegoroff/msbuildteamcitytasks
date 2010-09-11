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

namespace MSBuild.TeamCity.Tasks.Messages
{
	/// <summary>
	/// Base class of all TC messages.
	/// </summary>
	public abstract class TeamCityMessage
	{
		private readonly IList<MessageAttributeItem> _attributes = new List<MessageAttributeItem>();

		///<summary>
		/// Gets or sets a value indicating whether to add timestamt to the message. False by default
		///</summary>
		public bool IsAddTimestamp { get; set; }

		/// <summary>
		/// Gets or sets message's flowId
		/// </summary>
		public string FlowId { get; set; }

		/// <summary>
		/// Gets message name
		/// </summary>
		protected abstract string Message { get; }

		/// <summary>
		/// Gets message attributes list
		/// </summary>
		protected IList<MessageAttributeItem> Attributes
		{
			[DebuggerStepThrough]
			get { return _attributes; }
		}

		/// <summary>
		/// Returns a <see cref="String"/> that represents the current <see cref="MessageAttributeItem"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents the current <see cref="MessageAttributeItem"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			MessageAttributeItem timestamp = new MessageAttributeItem("timestamp",
			                                                          DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz",
			                                                                                CultureInfo.InvariantCulture));
			MessageAttributeItem flowId = new MessageAttributeItem("flowId", FlowId);
			if ( IsAddTimestamp && !_attributes.Contains(timestamp) )
			{
				_attributes.Add(timestamp);
			}

			if ( !string.IsNullOrEmpty(FlowId) && !_attributes.Contains(flowId) )
			{
				_attributes.Add(flowId);
			}

			StringBuilder sb = new StringBuilder();

			foreach ( MessageAttributeItem attribute in _attributes )
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