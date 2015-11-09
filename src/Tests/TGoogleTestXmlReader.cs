/*
 * Created by: egr
 * Created at: 07.05.2009
 * © 2007-2015 Alexander Egorov
 */

using System;
using System.IO;
using System.Text;
using System.Xml;
using FluentAssertions;
using MSBuild.TeamCity.Tasks.Internal;
using Tests.Utils;
using Xunit;

namespace Tests
{
    public class TGoogleTestXmlReader : IDisposable
    {
        private const string AllTests = "AllTests";
        private const string Suite1 = "suite1";
        private const string Suite2 = "suite2";
        private const string Test1 = "test1";
        private const string Test2 = "test2";

        private readonly StringBuilder sb;
        private readonly XmlWriter xw;

        public TGoogleTestXmlReader()
        {
            this.sb = new StringBuilder();
            this.xw = XmlWriter.Create(this.sb);
        }

        public void Dispose()
        {
            this.xw.Close();
        }

        [Fact]
        public void ReadEmpty()
        {
            using (new SuiteWriter(this.xw, 0, 0, 0.016, AllTests))
            {
            }
            this.AssertFailures(0);
        }

        [Fact]
        public void ReadOneSuiteAndTest()
        {
            using (new SuiteWriter(this.xw, 1, 0, 0.016, AllTests))
            {
                using (new SuiteWriter(this.xw, 1, 0, 0.016, Suite1))
                {
                    using (new CaseWriter(this.xw, Test1, 0.016, Suite1))
                    {
                    }
                }
            }
            this.AssertFailures(0);
        }

        [Fact]
        public void ReadTwoSuiteAndTwoTest()
        {
            using (new SuiteWriter(this.xw, 4, 0, 0.032, AllTests))
            {
                this.WriteTwoSuccess(Suite1);
                this.WriteTwoSuccess(Suite2);
            }
            this.AssertFailures(0);
        }

        [Fact]
        public void ReadTwoSuiteAndTwoTestOneFailed()
        {
            using (new SuiteWriter(this.xw, 4, 1, 0.032, AllTests))
            {
                using (new SuiteWriter(this.xw, 1, 1, 0.016, Suite1))
                {
                    using (new CaseWriter(this.xw, Test1, 0.016, Suite1))
                    {
                        using (new FailWriter(this.xw, "m1", "d1"))
                        {
                        }
                    }
                    using (new CaseWriter(this.xw, Test2, 0, Suite1))
                    {
                    }
                }
                this.WriteTwoSuccess(Suite2);
            }
            this.AssertFailures(1);
        }

        [Fact]
        public void ReadTwoSuiteAndTwoTestOneWithTwoFailures()
        {
            using (new SuiteWriter(this.xw, 4, 2, 0.032, AllTests))
            {
                using (new SuiteWriter(this.xw, 1, 2, 0.016, Suite1))
                {
                    this.WriteTwoFails();
                    using (new CaseWriter(this.xw, Test2, 0, Suite1))
                    {
                    }
                }
                this.WriteTwoSuccess(Suite2);
            }
            this.AssertFailures(2);
        }

        [Fact]
        public void ReadTwoSuiteAndTwoTestOnWithOneFailureOtherWithTwoFailures()
        {
            using (new SuiteWriter(this.xw, 4, 3, 0.032, AllTests))
            {
                using (new SuiteWriter(this.xw, 1, 3, 0.016, Suite1))
                {
                    this.WriteTwoFails();
                    using (new CaseWriter(this.xw, Test2, 0, Suite1))
                    {
                        using (new FailWriter(this.xw, "m3", "d3"))
                        {
                        }
                    }
                }
                this.WriteTwoSuccess(Suite2);
            }
            this.AssertFailures(3);
        }


        private void AssertFailures(int expected)
        {
            var reader = new GoogleTestXmlReader(new StringReader(this.sb.ToString()));
            reader.Read();
            reader.FailuresCount.Should().Be(expected);
        }

        private void WriteTwoSuccess(string suite)
        {
            using (new SuiteWriter(this.xw, 1, 0, 0.016, suite))
            {
                using (new CaseWriter(this.xw, Test1, 0.016, suite))
                {
                }
                using (new CaseWriter(this.xw, Test2, 0, suite))
                {
                }
            }
        }

        private void WriteTwoFails()
        {
            using (new CaseWriter(this.xw, Test1, 0.016, Suite1))
            {
                using (new FailWriter(this.xw, "m1", "d1"))
                {
                }
                using (new FailWriter(this.xw, "m2", "d2"))
                {
                }
            }
        }
    }
}