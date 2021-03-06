﻿/*
 * Created by: egr
 * Created at: 27.01.2011
 * © 2007-2015 Alexander Egorov
 */

using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    ///     Represents test ignore TC message
    /// </summary>
    public class TestIgnoredTeamCityMessage : NamedTeamCityMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TestIgnoredTeamCityMessage" /> class
        /// </summary>
        /// <param name="name">Test's name</param>
        /// <param name="message">Ignore comment</param>
        public TestIgnoredTeamCityMessage(string name, string message) : base(name)
        {
            this.Attributes.Add("message", message);
        }

        /// <summary>
        ///     Gets message name
        /// </summary>
        protected override string Message
        {
            [DebuggerStepThrough] get { return "testIgnored"; }
        }
    }
}