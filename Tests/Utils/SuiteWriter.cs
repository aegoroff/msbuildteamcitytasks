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
	public class SuiteWriter : IDisposable
	{
		private readonly XmlWriter _xw;

		public SuiteWriter(XmlWriter xw, int testCount, int failCount, double time, string name)
		{
			_xw = xw;
			xw.WriteStartElement("testsuite");
			xw.WriteAttributeString("tests", testCount.ToString());
			xw.WriteAttributeString("failures", failCount.ToString());
			xw.WriteAttributeString("disabled", "0");
			xw.WriteAttributeString("errors", "0");
			xw.WriteAttributeString("time", time.ToString(CultureInfo.InvariantCulture));
			xw.WriteAttributeString("name", name);
		}

		public void Dispose()
		{
			_xw.WriteEndElement();
			_xw.Flush();
		}
	}
}