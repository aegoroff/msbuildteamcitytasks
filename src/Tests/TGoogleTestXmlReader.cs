/*
 * Created by: egr
 * Created at: 07.05.2009
 * © 2007-2009 Alexander Egorov
 */

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
			"<?xml version=\"1.0\" encoding=\"UTF-8\"?><testsuite tests=\"4\" failures=\"0\" disabled=\"0\" errors=\"0\" time=\"0.016\" name=\"AllTests\"><testsuite name=\"suite1\" tests=\"2\" failures=\"0\" disabled=\"0\" errors=\"0\" time=\"0\"><testcase name=\"test1\" status=\"run\" time=\"0.016\" classname=\"suite1\" ><failure message=\"m2\" type=\"\">d2</failure></testcase><testcase name=\"test2\" status=\"run\" time=\"0.016\" classname=\"suite1\" /></testsuite><testsuite name=\"suite2\" tests=\"2\" failures=\"0\" disabled=\"0\" errors=\"0\" time=\"0\"><testcase name=\"test1\" status=\"run\" time=\"0.016\" classname=\"suite2\" /><testcase name=\"test2\" status=\"run\" time=\"0.016\" classname=\"suite2\" /></testsuite></testsuite>";

		private const string Suite1Start = "##teamcity[testSuiteStarted name='suite1']";
		private const string Suite2Start = "##teamcity[testSuiteStarted name='suite2']";
		private const string Suite1Finish = "##teamcity[testSuiteFinished name='suite1']";
		private const string Suite2Finish = "##teamcity[testSuiteFinished name='suite2']";
		private const string Test1Start = "##teamcity[testStarted name='test1']";
		private const string Test1Failed = "##teamcity[testFailed name='test1' message='m2' details='d2']";
		private const string Test2Start = "##teamcity[testStarted name='test2']";
		private const string Test1Finish = "##teamcity[testFinished name='test1' duration='16']";
		private const string Test2Finish = "##teamcity[testFinished name='test2' duration='16']";
		
		[Test]
		public void ReadEmpty()
		{
			GoogleTestXmlReader reader = new GoogleTestXmlReader(SimpleTestResult);
			Assert.That(reader.Read(), Is.Empty);
			Assert.That(reader.FailuresCount, Is.EqualTo(0));
		}
		
		[Test]
		public void ReadOneSuiteAndTest()
		{
			GoogleTestXmlReader reader = new GoogleTestXmlReader(OneSuiteAndTestTestResult);
			Assert.That(reader.Read(), Is.EquivalentTo(new[] { Suite1Start, Test1Start, Test1Finish, Suite1Finish }));
			Assert.That(reader.FailuresCount, Is.EqualTo(0));
		}
		
		[Test]
		public void ReadTwoSuiteAndTwoTest()
		{
			GoogleTestXmlReader reader = new GoogleTestXmlReader(TwoSuiteAndTwoTestTestResult);
			Assert.That(reader.Read(), Is.EquivalentTo(new[] { Suite1Start, Test1Start, Test1Finish, Test2Start, Test2Finish, Suite1Finish, Suite2Start, Test1Start, Test1Finish, Test2Start, Test2Finish, Suite2Finish }));
			Assert.That(reader.FailuresCount, Is.EqualTo(0));
		}
		
		[Test]
		public void ReadTwoSuiteAndTwoTestOneFailed()
		{
			GoogleTestXmlReader reader = new GoogleTestXmlReader(TwoSuiteAndTwoTestTestOneFailedResult);
			Assert.That(reader.Read(), Is.EquivalentTo(new[] { Suite1Start, Test1Start, Test1Failed, Test1Finish, Test2Start, Test2Finish, Suite1Finish, Suite2Start, Test1Start, Test1Finish, Test2Start, Test2Finish, Suite2Finish }));
			Assert.That(reader.FailuresCount, Is.EqualTo(1));
		}
	}
}