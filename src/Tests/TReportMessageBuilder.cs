/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2015 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks.Messages;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TReportMessageBuilder
    {
        private const string Text = "t";

        [Test]
        public void Full()
        {
            const string error = "e";
            const string status = "ERROR";

            var builder = new ReportMessageBuilder(Text, status, error);

            Assert.That(builder.BuildMessage().ToString(),
                        Is.EqualTo("##teamcity[message text='t' status='ERROR' errorDetails='e']"));
        }

        [Test]
        public void StatusAndText()
        {
            const string status = "WARNING";

            var builder = new ReportMessageBuilder(Text, status, null);

            Assert.That(builder.BuildMessage().ToString(), Is.EqualTo("##teamcity[message text='t' status='WARNING']"));
        }

        [Test]
        public void OnlyText()
        {
            var builder = new ReportMessageBuilder(Text, null, null);

            Assert.That(builder.BuildMessage().ToString(), Is.EqualTo("##teamcity[message text='t']"));
        }

        [Test]
        public void OnlyDetails()
        {
            const string error = "e";

            var builder = new ReportMessageBuilder(Text, null, error);

            Assert.That(builder.BuildMessage().ToString(), Is.EqualTo("##teamcity[message text='t']"));
        }
    }
}