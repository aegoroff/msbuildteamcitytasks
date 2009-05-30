/*
 * Created by: egr
 * Created at: 30.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System;
using System.Xml;

namespace Tests.Utils
{
	public class FailWriter : IDisposable
	{
		private readonly XmlWriter _xw;

		public FailWriter(XmlWriter xw, string message, string content)
		{
			_xw = xw;
			xw.WriteStartElement("failure");
			xw.WriteAttributeString("message", message);
			xw.WriteAttributeString("type", string.Empty);
			xw.WriteRaw(content);
		}

		public void Dispose()
		{
			_xw.WriteEndElement();
			_xw.Flush();
		}
	}
}