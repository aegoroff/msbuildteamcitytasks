/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2012 Alexander Egorov
 */

using System.Diagnostics;

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
        public TestFailedTeamCityMessage(string name, string message, string details) : base(name)
        {
            Attributes.Add("message", message);
            Attributes.Add("details", details);
        }

        ///<summary>
        /// Gets or sets expected attribute value
        ///</summary>
        public string Expected
        {
            get { return GetAttributeValue(ExpectedAttr); }
            set { SetAttributeValue(value, ExpectedAttr); }
        }

        ///<summary>
        /// Gets or sets actual attribute value
        ///</summary>
        public string Actual
        {
            get { return GetAttributeValue(ActualAttr); }
            set { SetAttributeValue(value, ActualAttr); }
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string Message
        {
            [DebuggerStepThrough]
            get { return "testFailed"; }
        }

        private void InsertType()
        {
            var item = new MessageAttributeItem(TypeAttrValue, TypeAttr);
            if (Attributes.Contains(item))
            {
                Attributes.Remove(item);
            }
            Attributes.Insert(0, item);
        }

        private void SetAttributeValue(string value, string attr)
        {
            if (!string.IsNullOrEmpty(value))
            {
                InsertType();
            }
            Attributes.Add(attr, value);
        }
    }
}