/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2011 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using MSBuild.TeamCity.Tasks.Internal;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    /// Base class of all TC messages.
    /// </summary>
    public abstract class TeamCityMessage
    {
        private const string Space = " ";
        private const string TeamCityMessageHead = "##teamcity[";
        private const string TeamCityMessageTrail = "]";
        private readonly List<MessageAttributeItem> _attributes = new List<MessageAttributeItem>();

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
                                                                      DateTime.Now.ToString(
                                                                          "yyyy-MM-ddTHH:mm:ss.fffzzz",
                                                                          CultureInfo.InvariantCulture));
            if ( IsAddTimestamp && !_attributes.Contains(timestamp) )
            {
                _attributes.Add(timestamp);
            }

            MessageAttributeItem flowId = new MessageAttributeItem("flowId", FlowId);
            if ( !string.IsNullOrEmpty(FlowId) && !_attributes.Contains(flowId) )
            {
                _attributes.Add(flowId);
            }

            if ( _attributes.Count == 0 )
            {
                return TeamCityMessageHead + Message + TeamCityMessageTrail;
            }

            SequenceBuilder<MessageAttributeItem> sequence = new SequenceBuilder<MessageAttributeItem>(_attributes,
                                                                                                       Space,
                                                                                                       TeamCityMessageHead + Message + Space,
                                                                                                       TeamCityMessageTrail);

            return sequence.ToString();
        }
    }
}