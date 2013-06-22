/*
 * Created by: egr
 * Created at: 30.05.2009
 * © 2007-2013 Alexander Egorov
 */

using System;
using System.Xml;

namespace Tests.Utils
{
    public class BaseWriter : IDisposable
    {
        private readonly XmlWriter xw;

        protected BaseWriter(XmlWriter xw)
        {
            this.xw = xw;
        }

        public void Dispose()
        {
            xw.WriteEndElement();
            xw.Flush();
        }
    }
}