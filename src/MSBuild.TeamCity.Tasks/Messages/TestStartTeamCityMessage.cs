/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Messages
{
    ///<summary>
    /// Represents test start TC message
    ///</summary>
    public class TestStartTeamCityMessage : NamedTeamCityMessage
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="TestStartTeamCityMessage"/> class using name specified
        ///</summary>
        ///<param name="name">Name attribute value</param>
        public TestStartTeamCityMessage( string name ) : base(name)
        {
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string Message
        {
            get { return "testStarted"; }
        }
    }
}