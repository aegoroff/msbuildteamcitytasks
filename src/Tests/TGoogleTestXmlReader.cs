/*
 * Created by: egr
 * Created at: 07.05.2009
 * © 2007-2012 Alexander Egorov
 */

using System.IO;
using System.Text;
using System.Xml;
using MSBuild.TeamCity.Tasks.Internal;
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

        private StringBuilder sb;
        private XmlWriter xw;

        [SetUp]
        public void Setup()
        {
            sb = new StringBuilder();
            xw = XmlWriter.Create(sb);
        }

        [TearDown]
        public void Teardown()
        {
            xw.Close();
        }

        [Test]
        public void ReadEmpty()
        {
            using (new SuiteWriter(xw, 0, 0, 0.016, AllTests))
            {
            }
            AssertFailures(0);
        }

        [Test]
        public void ReadOneSuiteAndTest()
        {
            using (new SuiteWriter(xw, 1, 0, 0.016, AllTests))
            {
                using (new SuiteWriter(xw, 1, 0, 0.016, Suite1))
                {
                    using (new CaseWriter(xw, Test1, 0.016, Suite1))
                    {
                    }
                }
            }
            AssertFailures(0);
        }

        [Test]
        public void ReadTwoSuiteAndTwoTest()
        {
            using (new SuiteWriter(xw, 4, 0, 0.032, AllTests))
            {
                WriteTwoSuccess(Suite1);
                WriteTwoSuccess(Suite2);
            }
            AssertFailures(0);
        }

        [Test]
        public void ReadTwoSuiteAndTwoTestOneFailed()
        {
            using (new SuiteWriter(xw, 4, 1, 0.032, AllTests))
            {
                using (new SuiteWriter(xw, 1, 1, 0.016, Suite1))
                {
                    using (new CaseWriter(xw, Test1, 0.016, Suite1))
                    {
                        using (new FailWriter(xw, "m1", "d1"))
                        {
                        }
                    }
                    using (new CaseWriter(xw, Test2, 0, Suite1))
                    {
                    }
                }
                WriteTwoSuccess(Suite2);
            }
            AssertFailures(1);
        }

        [Test]
        public void ReadTwoSuiteAndTwoTestOneWithTwoFailures()
        {
            using (new SuiteWriter(xw, 4, 2, 0.032, AllTests))
            {
                using (new SuiteWriter(xw, 1, 2, 0.016, Suite1))
                {
                    WriteTwoFails();
                    using (new CaseWriter(xw, Test2, 0, Suite1))
                    {
                    }
                }
                WriteTwoSuccess(Suite2);
            }
            AssertFailures(2);
        }

        [Test]
        public void ReadTwoSuiteAndTwoTestOnWithOneFailureOtherWithTwoFailures()
        {
            using (new SuiteWriter(xw, 4, 3, 0.032, AllTests))
            {
                using (new SuiteWriter(xw, 1, 3, 0.016, Suite1))
                {
                    WriteTwoFails();
                    using (new CaseWriter(xw, Test2, 0, Suite1))
                    {
                        using (new FailWriter(xw, "m3", "d3"))
                        {
                        }
                    }
                }
                WriteTwoSuccess(Suite2);
            }
            AssertFailures(3);
        }


        private void AssertFailures(int expected)
        {
            GoogleTestXmlReader reader = new GoogleTestXmlReader(new StringReader(sb.ToString()));
            reader.Read();
            Assert.That(reader.FailuresCount, Is.EqualTo(expected));
        }

        private void WriteTwoSuccess(string suite)
        {
            using (new SuiteWriter(xw, 1, 0, 0.016, suite))
            {
                using (new CaseWriter(xw, Test1, 0.016, suite))
                {
                }
                using (new CaseWriter(xw, Test2, 0, suite))
                {
                }
            }
        }

        private void WriteTwoFails()
        {
            using (new CaseWriter(xw, Test1, 0.016, Suite1))
            {
                using (new FailWriter(xw, "m1", "d1"))
                {
                }
                using (new FailWriter(xw, "m2", "d2"))
                {
                }
            }
        }
    }
}