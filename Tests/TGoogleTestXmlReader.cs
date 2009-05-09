/*
 * Created by: egr
 * Created at: 07.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System;
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

		private const string Suite1Start = "##teamcity[testSuiteStarted name='suite1']";
		private const string Suite1Finish = "##teamcity[testSuiteFinished name='suite1']";
		private const string Test1Start = "##teamcity[testStarted name='test1']";
		private const string Test1Finish = "##teamcity[testFinished name='test1' duration='16']";
		
		[Test]
		public void Constructor()
		{
			new GoogleTestXmlReader(SimpleTestResult);
		}
		
		[Test]
		public void ReadEmpty()
		{
			GoogleTestXmlReader reader = new GoogleTestXmlReader(SimpleTestResult);
			Assert.That(reader.Read(), Is.Empty);
		}
		
		[Test]
		public void ReadOneSuiteAndTest()
		{
			GoogleTestXmlReader reader = new GoogleTestXmlReader(OneSuiteAndTestTestResult);
			Assert.That(reader.Read(), Is.EquivalentTo(new[] { Suite1Start, Test1Start, Test1Finish, Suite1Finish }));
		}
		
		[Test]
		public void ReadFromFile()
		{
			const string path = @"D:\CSharp\NCover2TeamCity\GoogleTestsFailed.xml";
			GoogleTestXmlReader reader = new GoogleTestXmlReader(File.ReadAllText(path));
			foreach (var str in reader.Read() )
			{
				Console.WriteLine(str);
			}
		}
	}
}