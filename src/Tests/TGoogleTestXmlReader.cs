/*
 * Created by: egr
 * Created at: 07.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System.IO;
using MSBuild.TeamCity.Tasks;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class TGoogleTestXmlReader
	{
		private const string SimpleTestResult =
			"<?xml version=\"1.0\" encoding=\"UTF-8\"?><testsuite tests=\"26\" failures=\"0\" disabled=\"0\" errors=\"0\" time=\"0.016\" name=\"AllTests\"></testsuite>";

		private const string OneSuiteAndTestTestResult =
			"<?xml version=\"1.0\" encoding=\"UTF-8\"?><testsuite tests=\"1\" failures=\"0\" disabled=\"0\" errors=\"0\" time=\"0.016\" name=\"AllTests\"><testsuite name=\"suite1\" tests=\"1\" failures=\"0\" disabled=\"0\" errors=\"0\" time=\"0\"><testcase name=\"test1\" status=\"run\" time=\"0.016\" classname=\"suite1\" /></testsuite></testsuite>";

		private const string TwoSuiteAndTwoTestTestResult =
			"<?xml version=\"1.0\" encoding=\"UTF-8\"?><testsuite tests=\"4\" failures=\"0\" disabled=\"0\" errors=\"0\" time=\"0.016\" name=\"AllTests\"><testsuite name=\"suite1\" tests=\"2\" failures=\"0\" disabled=\"0\" errors=\"0\" time=\"0\"><testcase name=\"test1\" status=\"run\" time=\"0.016\" classname=\"suite1\" /><testcase name=\"test2\" status=\"run\" time=\"0.016\" classname=\"suite1\" /></testsuite><testsuite name=\"suite2\" tests=\"2\" failures=\"0\" disabled=\"0\" errors=\"0\" time=\"0\"><testcase name=\"test1\" status=\"run\" time=\"0.016\" classname=\"suite2\" /><testcase name=\"test2\" status=\"run\" time=\"0.016\" classname=\"suite2\" /></testsuite></testsuite>";

		private const string TwoSuiteAndTwoTestTestOneFailedResult =
			"<?xml version=\"1.0\" encoding=\"UTF-8\"?><testsuite tests=\"4\" failures=\"0\" disabled=\"0\" errors=\"0\" time=\"0.016\" name=\"AllTests\"><testsuite name=\"suite1\" tests=\"2\" failures=\"1\" disabled=\"0\" errors=\"0\" time=\"0\"><testcase name=\"test1\" status=\"run\" time=\"0.016\" classname=\"suite1\" ><failure message=\"m2\" type=\"\">d2</failure></testcase><testcase name=\"test2\" status=\"run\" time=\"0.016\" classname=\"suite1\" /></testsuite><testsuite name=\"suite2\" tests=\"2\" failures=\"0\" disabled=\"0\" errors=\"0\" time=\"0\"><testcase name=\"test1\" status=\"run\" time=\"0.016\" classname=\"suite2\" /><testcase name=\"test2\" status=\"run\" time=\"0.016\" classname=\"suite2\" /></testsuite></testsuite>";
		
		private const string TwoSuiteAndTwoTestTestTwoFailedResult =
			"<?xml version=\"1.0\" encoding=\"UTF-8\"?><testsuite tests=\"4\" failures=\"0\" disabled=\"0\" errors=\"0\" time=\"0.016\" name=\"AllTests\"><testsuite name=\"suite1\" tests=\"2\" failures=\"2\" disabled=\"0\" errors=\"0\" time=\"0\"><testcase name=\"test1\" status=\"run\" time=\"0.016\" classname=\"suite1\" ><failure message=\"m2\" type=\"\">d2</failure><failure message=\"m3\" type=\"\">d3</failure></testcase><testcase name=\"test2\" status=\"run\" time=\"0.016\" classname=\"suite1\" /></testsuite><testsuite name=\"suite2\" tests=\"2\" failures=\"0\" disabled=\"0\" errors=\"0\" time=\"0\"><testcase name=\"test1\" status=\"run\" time=\"0.016\" classname=\"suite2\" /><testcase name=\"test2\" status=\"run\" time=\"0.016\" classname=\"suite2\" /></testsuite></testsuite>";

		[Test]
		public void ReadEmpty()
		{
			GoogleTestXmlReader reader = new GoogleTestXmlReader(new StringReader(SimpleTestResult));
			reader.Read();
			Assert.That(reader.FailuresCount, Is.EqualTo(0));
		}

		[Test]
		public void ReadOneSuiteAndTest()
		{
			GoogleTestXmlReader reader = new GoogleTestXmlReader(new StringReader(OneSuiteAndTestTestResult));
			reader.Read();
			Assert.That(reader.FailuresCount, Is.EqualTo(0));
		}

		[Test]
		public void ReadTwoSuiteAndTwoTest()
		{
			GoogleTestXmlReader reader = new GoogleTestXmlReader(new StringReader(TwoSuiteAndTwoTestTestResult));
			reader.Read();
			Assert.That(reader.FailuresCount, Is.EqualTo(0));
		}

		[Test]
		public void ReadTwoSuiteAndTwoTestOneFailed()
		{
			GoogleTestXmlReader reader = new GoogleTestXmlReader(new StringReader(TwoSuiteAndTwoTestTestOneFailedResult));
			reader.Read();
			Assert.That(reader.FailuresCount, Is.EqualTo(1));
		}
		
		[Test]
		public void ReadTwoSuiteAndTwoTestTwoFailures()
		{
			GoogleTestXmlReader reader = new GoogleTestXmlReader(new StringReader(TwoSuiteAndTwoTestTestTwoFailedResult));
			reader.Read();
			Assert.That(reader.FailuresCount, Is.EqualTo(2));
		}
	}
}