/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2011 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Messages
{
    ///<summary>
    /// Represents test failure TC message
    ///</summary>
    public class TestFailedTeamCityMessage : NamedTeamCityMessage
    {
        private const string ExpectedAttr = "expected";
        private const string ActualAttr = "actual";
        private const string TypeAttr = "type";
        private const string TypeAttrValue = "comparisonFailure";

        ///<summary>
        /// Initializes a new instance of the <see cref="TestFailedTeamCityMessage"/> class
        ///</summary>
        ///<param name="name">Test's name</param>
        ///<param name="message">Failure message</param>
        ///<param name="details">Failure details like stack trace</param>
        public TestFailedTeamCityMessage( string name, string message, string details ) : base(name)
        {
            Attributes.Add("message", message);
            Attributes.Add("details", details);
        }

        ///<summary>
        /// Gets or sets expected attribute value
        ///</summary>
        public string Expected
        {
            get
            {
                return GetAttributeValue(ExpectedAttr);
            }
            
            set
            {
                InsertType();
                Attributes.Add(ExpectedAttr, value);
            }
        }

        ///<summary>
        /// Gets or sets actual attribute value
        ///</summary>
        public string Actual
        {
            get
            {
                return GetAttributeValue(ActualAttr);
            }
            
            set
            {
                InsertType();
                Attributes.Add(ActualAttr, value);
            }
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string Message
        {
            get { return "testFailed"; }
        }

        private void InsertType()
        {
            MessageAttributeItem item = new MessageAttributeItem(TypeAttr, TypeAttrValue);
            if ( Attributes.Contains(item) )
            {
                Attributes.Remove(item);
            }
            Attributes.Insert(0, item);
        }
    }
}