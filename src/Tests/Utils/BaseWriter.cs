/*
 * Created by: egr
 * Created at: 30.05.2009
 * © 2007-2012 Alexander Egorov
 */

using System;
using System.Xml;

namespace Tests.Utils
{
    public class BaseWriter : IDisposable
    {
        private readonly XmlWriter _xw;

        protected BaseWriter(XmlWriter xw)
        {
            _xw = xw;
        }

        public void Dispose()
        {
            _xw.WriteEndElement();
            _xw.Flush();
        }
    }
}