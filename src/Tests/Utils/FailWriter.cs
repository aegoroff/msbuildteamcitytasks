/*
 * Created by: egr
 * Created at: 30.05.2009
 * � 2007-2015 Alexander Egorov
 */

using System.Xml;

namespace Tests.Utils
{
    public class FailWriter : BaseWriter
    {
        public FailWriter(XmlWriter xw, string message, string content) : base(xw)
        {
            xw.WriteStartElement("failure");
            xw.WriteAttributeString("message", message);
            xw.WriteAttributeString("type", string.Empty);
            xw.WriteRaw(content);
        }
    }
}