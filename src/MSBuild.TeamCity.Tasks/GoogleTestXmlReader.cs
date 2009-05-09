/*
 * Created by: egr
 * Created at: 07.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Represents Google test (http://code.google.com/p/googletest/) xml test result parser
	///</summary>
	public class GoogleTestXmlReader : IDisposable
	{
		private const string TestSuite = "testsuite";
		private const string TestCase = "testcase";
		private const string Failure = "failure";
		private const string Name = "name";
		private const string Time = "time";
		private const string Message = "message";
		private readonly XmlReader _reader;

		///<summary>
		/// Creates new reader instance using whole XML text read into string specified.
		///</summary>
		///<param name="xmlText"></param>
		public GoogleTestXmlReader( string xmlText )
		{
			_reader = XmlReader.Create(new StringReader(xmlText));
		}

		///<summary>
		/// Reads Google test xml report and returns suites and tests as TC messages.
		///</summary>
		///<returns></returns>
		public ICollection<string> Read()
		{
			List<string> result = new List<string>();
			_reader.MoveToContent();
			while ( _reader.ReadToFollowing(TestSuite) )
			{
				string suite = _reader.GetAttribute(Name);
				result.Add(new TestSuiteStartTeamCityMessage(suite).ToString());

				ReadTests(result);

				result.Add(new TestSuiteFinishTeamCityMessage(suite).ToString());
			}
			return result;
		}

		private void ReadTests( ICollection<string> result )
		{
			XmlReader rdr = _reader.ReadSubtree();
			using ( rdr )
			{
				while ( rdr.ReadToFollowing(TestCase) )
				{
					string test = rdr.GetAttribute(Name);
					string time = rdr.GetAttribute(Time);
					double duration = double.Parse(time, CultureInfo.InvariantCulture);

					result.Add(new TestStartTeamCityMessage(test).ToString());

					ReadFailures(result, test);

					result.Add(new TestFinishTeamCityMessage(test, duration).ToString());
				}
			}
		}

		private void ReadFailures( ICollection<string> result, string test )
		{
			XmlReader rdr = _reader.ReadSubtree();
			using ( rdr )
			{
				while ( rdr.ReadToFollowing(Failure) )
				{
					string message = rdr.GetAttribute(Message);
					string details = rdr.ReadElementContentAsString();
					result.Add(new TestFailedTeamCityMessage(test, message, details).ToString());
				}
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
		/// <param name="disposing"></param>
		protected void Dispose( bool disposing )
		{
			if ( disposing )
			{
				_reader.Close();
			}
		}

		/// <summary>
		/// Finalizes object
		/// </summary>
		~GoogleTestXmlReader()
		{
			Dispose(false);
		}
	}
}