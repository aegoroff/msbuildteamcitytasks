/*
 * Created by: egr
 * Created at: 07.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System.IO;
using System.Text;
using System.Xml;
using MSBuild.TeamCity.Tasks;
using NUnit.Framework;
using Tests.Utils;

namespace Tests
{
	[TestFixture]
	public class TGoogleTestXmlReader
	{
		private const string AllTests = "AllTests";
		private const string Suite1 = "suite1";
		private const string Suite2 = "suite2";
		private const string Test1 = "test1";
		private const string Test2 = "test2";

		private StringBuilder _sb;
		private XmlWriter _xw;
		
		[SetUp]
		public void Setup()
		{
			_sb = new StringBuilder();
			_xw = XmlWriter.Create(_sb);
		}
		
		[TearDown]
		public void Teardown()
		{
			_xw.Close();
		}
		
		[Test]
		public void ReadEmpty()
		{
			using (new SuiteWriter(_xw, 0, 0, 0.016, AllTests))
			{
			}
			GoogleTestXmlReader reader = new GoogleTestXmlReader(new StringReader(_sb.ToString()));
			reader.Read();
			Assert.That(reader.FailuresCount, Is.EqualTo(0));
		}

		[Test]
		public void ReadOneSuiteAndTest()
		{
			using (new SuiteWriter(_xw, 1, 0, 0.016, AllTests))
			{
				using (new SuiteWriter(_xw, 1, 0, 0.016, Suite1))
				{
					using (new CaseWriter(_xw, Test1, 0.016, Suite1))
					{
					}
				}
			}
			GoogleTestXmlReader reader = new GoogleTestXmlReader(new StringReader(_sb.ToString()));
			reader.Read();
			Assert.That(reader.FailuresCount, Is.EqualTo(0));
		}

		[Test]
		public void ReadTwoSuiteAndTwoTest()
		{
			using (new SuiteWriter(_xw, 4, 0, 0.032, AllTests))
			{
				using (new SuiteWriter(_xw, 1, 0, 0.016, Suite1))
				{
					using (new CaseWriter(_xw, Test1, 0.016, Suite1))
					{
					}
					using (new CaseWriter(_xw, Test2, 0, Suite1))
					{
					}
				}
				using (new SuiteWriter(_xw, 1, 0, 0.016, Suite2))
				{
					using (new CaseWriter(_xw, Test1, 0.016, Suite2))
					{
					}
					using (new CaseWriter(_xw, Test2, 0, Suite2))
					{
					}
				}
			}
			GoogleTestXmlReader reader = new GoogleTestXmlReader(new StringReader(_sb.ToString()));
			reader.Read();
			Assert.That(reader.FailuresCount, Is.EqualTo(0));
		}

		[Test]
		public void ReadTwoSuiteAndTwoTestOneFailed()
		{
			using (new SuiteWriter(_xw, 4, 1, 0.032, AllTests))
			{
				using (new SuiteWriter(_xw, 1, 1, 0.016, Suite1))
				{
					using (new CaseWriter(_xw, Test1, 0.016, Suite1))
					{
						using (new FailWriter(_xw, "m1", "d1"))
						{
						}
					}
					using (new CaseWriter(_xw, Test2, 0, Suite1))
					{
					}
				}
				using (new SuiteWriter(_xw, 1, 0, 0.016, Suite2))
				{
					using (new CaseWriter(_xw, Test1, 0.016, Suite2))
					{
					}
					using (new CaseWriter(_xw, Test2, 0, Suite2))
					{
					}
				}
			}
			GoogleTestXmlReader reader = new GoogleTestXmlReader(new StringReader(_sb.ToString()));
			reader.Read();
			Assert.That(reader.FailuresCount, Is.EqualTo(1));
		}
		
		[Test]
		public void ReadTwoSuiteAndTwoTestOneWithTwoFailures()
		{
			using (new SuiteWriter(_xw, 4, 2, 0.032, AllTests))
			{
				using (new SuiteWriter(_xw, 1, 2, 0.016, Suite1))
				{
					using (new CaseWriter(_xw, Test1, 0.016, Suite1))
					{
						using (new FailWriter(_xw, "m1", "d1"))
						{
						}
						using (new FailWriter(_xw, "m2", "d2"))
						{
						}
					}
					using (new CaseWriter(_xw, Test2, 0, Suite1))
					{
					}
				}
				using (new SuiteWriter(_xw, 1, 0, 0.016, Suite2))
				{
					using (new CaseWriter(_xw, Test1, 0.016, Suite2))
					{
					}
					using (new CaseWriter(_xw, Test2, 0, Suite2))
					{
					}
				}
			}
			GoogleTestXmlReader reader = new GoogleTestXmlReader(new StringReader(_sb.ToString()));
			reader.Read();
			Assert.That(reader.FailuresCount, Is.EqualTo(2));
		}
		
		[Test]
		public void ReadTwoSuiteAndTwoTestOnWithOneFailureOtherWithTwoFailures()
		{
			using (new SuiteWriter(_xw, 4, 3, 0.032, AllTests))
			{
				using (new SuiteWriter(_xw, 1, 3, 0.016, Suite1))
				{
					using (new CaseWriter(_xw, Test1, 0.016, Suite1))
					{
						using (new FailWriter(_xw, "m1", "d1"))
						{
						}
						using (new FailWriter(_xw, "m2", "d2"))
						{
						}
					}
					using (new CaseWriter(_xw, Test2, 0, Suite1))
					{
						using (new FailWriter(_xw, "m3", "d3"))
						{
						}
					}
				}
				using (new SuiteWriter(_xw, 1, 0, 0.016, Suite2))
				{
					using (new CaseWriter(_xw, Test1, 0.016, Suite2))
					{
					}
					using (new CaseWriter(_xw, Test2, 0, Suite2))
					{
					}
				}
			}
			GoogleTestXmlReader reader = new GoogleTestXmlReader(new StringReader(_sb.ToString()));
			reader.Read();
			Assert.That(reader.FailuresCount, Is.EqualTo(3));
		}
	}
}