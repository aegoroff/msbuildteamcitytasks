/*
 * Created by: egr
 * Created at: 09.03.2012
 * © 2007-2012 Alexander Egorov
 */

using System.Linq;
using MSBuild.TeamCity.Tasks.Internal;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TOpenCoverStatisticParser
    {
        const string TestData = @"Visited Classes 97 of 97 (100)
    Visited Methods 217 of 221 (98.1900452488688)
    Visited Points 565 of 579 (97.5820379965458)
    Visited Branches 264 of 360 (73.3333333333333)"; 

        [Test]
        public void Parse()
        {
            var parser = new OpenCoverStatisticParser();
            var result = parser.Parse(TestData.Split('\n'));
            Assert.That(result.Count(), Is.EqualTo(6));
        }
    }
}