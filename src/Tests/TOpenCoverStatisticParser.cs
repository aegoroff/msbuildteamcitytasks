/*
 * Created by: egr
 * Created at: 16.10.2015
 * © 2007-2015 Alexander Egorov
 */

using System.Linq;
using MSBuild.TeamCity.Tasks.Internal;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TOpenCoverStatisticParser
    {
        private const string TestData = @"Visited Classes 97 of 97 (100)
    Visited Methods 217 of 221 (98.1900452488688)
    Visited Points 565 of 579 (97.5820379965458)
    Visited Branches 264 of 360 (73.3333333333333)
Alternative Visited Classes 97 of 97 (100)
Alternative Visited Methods 509 of 589 (86.4176570458404)";

        [Test]
        public void Parse()
        {
            var parser = new OpenCoverOutputStatisticParser();
            var result = parser.Parse(TestData.Split('\n'));
            Assert.That(result.Count(), Is.EqualTo(9));
        }
    }
}