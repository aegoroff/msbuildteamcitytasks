/*
 * Created by: egr
 * Created at: 30.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System;
using System.Globalization;
using System.Xml;

namespace Tests.Utils
{
	public class CaseWriter : IDisposable
	{
		private readonly XmlWriter _xw;

		public CaseWriter(XmlWriter xw, string name, double time, string className)
		{
			_xw = xw;
			xw.WriteStartElement("testcase");
			xw.WriteAttributeString("name", name);
			xw.WriteAttributeString("status", "run");
			xw.WriteAttributeString("time", time.ToString(CultureInfo.InvariantCulture));
			xw.WriteAttributeString("classname", className);
		}

		public void Dispose()
		{
			_xw.WriteEndElement();
			_xw.Flush();
		}
	}
}