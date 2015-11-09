/*
 * Created by: egr
 * Created at: 09.03.2012
 * © 2007-2015 Alexander Egorov
 */

using System;
using System.Linq;
using FluentAssertions;
using MSBuild.TeamCity.Tasks.Internal;
using Xunit;

namespace Tests
{
    public class TOpenCoverXmlReportStatisticParser
    {
        private static readonly string reportPath = Environment.CurrentDirectory + @"\..\..\..\External\opencover.xml";

        [Fact]
        public void Parse()
        {
            var parser = new OpenCoverXmlReportStatisticParser();
            var result = parser.Parse(reportPath);
            result.Count().Should().Be(9);
        }
    }
}