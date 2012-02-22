/*
 * Created by: egr
 * Created at: 30.05.2009
 * © 2007-2012 Alexander Egorov
 */

using System.Globalization;
using System.Xml;

namespace Tests.Utils
{
    public class CaseWriter : BaseWriter
    {
        public CaseWriter(XmlWriter xw, string name, double time, string className) : base(xw)
        {
            xw.WriteStartElement("testcase");
            xw.WriteAttributeString("name", name);
            xw.WriteAttributeString("status", "run");
            xw.WriteAttributeString("time", time.ToString(CultureInfo.InvariantCulture));
            xw.WriteAttributeString("classname", className);
        }
    }
}