/*
 * Created by: egr
 * Created at: 09.03.2012
 * © 2007-2015 Alexander Egorov
 */

using System;
using System.Linq;
using MSBuild.TeamCity.Tasks.Internal;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TOpenCoverStatisticParser
    {
        private static readonly string ReportPath = Environment.CurrentDirectory + @"\..\..\..\External\opencover.xml";

        [Test]
        public void Parse()
        {
            var parser = new OpenCoverStatisticParser();
            var result = parser.Parse(ReportPath);
            Assert.That(result.Count(), Is.EqualTo(9));
        }
    }
}