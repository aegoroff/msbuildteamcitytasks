/*
 * Created by: egr
 * Created at: 22.02.2012
 * © 2007-2012 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    /// Represents update some build parameters TC message
    /// </summary>
    public class SetParameterTeamCityMessage : NamedTeamCityMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetParameterTeamCityMessage"/> class
        /// </summary>
        /// <param name="name">Name attribute value</param>
        /// <param name="value">Value attribute parameter</param>
        public SetParameterTeamCityMessage(string name, string value) : base(name)
        {
            Attributes.Add("value", value);
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string Message
        {
            get { return "setParameter"; }
        }
    }
}