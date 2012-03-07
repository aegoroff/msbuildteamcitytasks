/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2012 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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
        private readonly List<MessageAttributeItem> attributes = new List<MessageAttributeItem>();

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
            get { return attributes; }
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="MessageAttributeItem"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents the current <see cref="MessageAttributeItem"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            var timestamp = new MessageAttributeItem(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", CultureInfo.InvariantCulture), "timestamp");
            if (IsAddTimestamp && !attributes.Contains(timestamp))
            {
                attributes.Add(timestamp);
            }

            var flowId = new MessageAttributeItem(FlowId, "flowId");
            if (!string.IsNullOrEmpty(FlowId) && !attributes.Contains(flowId))
            {
                attributes.Add(flowId);
            }

            if (attributes.Count == 0)
            {
                return TeamCityMessageHead + Message + TeamCityMessageTrail;
            }

            var sequence = new SequenceBuilder<MessageAttributeItem>(attributes,
                                                                     Space,
                                                                     TeamCityMessageHead +
                                                                     Message + Space,
                                                                     TeamCityMessageTrail);

            return sequence.ToString();
        }

        /// <summary>
        /// Gets attribute's value
        /// </summary>
        /// <param name="attr">attribute name</param>
        /// <returns>The attribute's value or empty string if no attribute found</returns>
        protected string GetAttributeValue(string attr)
        {
            foreach (var item in Attributes.Where(item => item.Name == attr))
            {
                return item.Value;
            }
            return string.Empty;
        }
    }
}