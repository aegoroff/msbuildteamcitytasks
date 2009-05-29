/*
 * Created by: egr
 * Created at: 07.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System;
using System.IO;
using System.Xml;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Represents Google test (http://code.google.com/p/googletest/) xml test result parser
	///</summary>
	public class GoogleTestXmlReader : IDisposable
	{
		private const string Failure = "failure";
		private readonly XmlReader _reader;

		///<summary>
		/// Initializes a new instance of the <see cref="GoogleTestXmlReader"/> class using whole XML text read into string specified.
		///</summary>
		///<param name="xmlTextReader">The <see cref="TextReader"/> from which to read the XML data.</param>
		public GoogleTestXmlReader( TextReader xmlTextReader )
		{
			_reader = XmlReader.Create(xmlTextReader);
		}

		///<summary>
		/// Initializes a new instance of the <see cref="GoogleTestXmlReader"/> class using full path to xml report file specified.
		///</summary>
		///<param name="path">The URI for the file containing the XML data.</param>
		public GoogleTestXmlReader( string path )
		{
			_reader = XmlReader.Create(path);
		}

		///<summary>
		/// Gets failed tests count
		///</summary>
		public int FailuresCount { get; private set; }

		///<summary>
		/// Reads Google test xml report and returns suites and tests as TC messages.
		///</summary>
		public void Read()
		{
			_reader.MoveToContent();
			while ( _reader.ReadToFollowing(Failure) )
			{
				FailuresCount++;
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <param name="disposing">Whether to perform application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</param>
		protected virtual void Dispose( bool disposing )
		{
			if ( disposing )
			{
				_reader.Close();
			}
		}
	}
}