/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2011 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Messages
{
    ///<summary>
    /// Represents test suite finish TC message
    ///</summary>
    public class TestSuiteFinishTeamCityMessage : NamedTeamCityMessage
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="TestSuiteFinishTeamCityMessage"/> class using name specified
        ///</summary>
        ///<param name="name">Name attribute value</param>
        public TestSuiteFinishTeamCityMessage(string name) : base(name)
        {
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string Message
        {
            get { return "testSuiteFinished"; }
        }
    }
}