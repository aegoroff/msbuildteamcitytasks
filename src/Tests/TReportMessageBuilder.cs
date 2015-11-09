/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2015 Alexander Egorov
 */

using FluentAssertions;
using MSBuild.TeamCity.Tasks.Messages;
using Xunit;

namespace Tests
{
    public class TReportMessageBuilder
    {
        private const string Text = "t";

        [Theory]
        [InlineData(Text, "ERROR", "e", "##teamcity[message text='t' status='ERROR' errorDetails='e']")]
        [InlineData(Text, "WARNING", null, "##teamcity[message text='t' status='WARNING']")]
        [InlineData(Text, null, null, "##teamcity[message text='t']")]
        [InlineData(Text, null, "e", "##teamcity[message text='t']")]
        public void Tests(string text, string status, string details, string expectation)
        {
            var builder = new ReportMessageBuilder(text, status, details);

            builder.BuildMessage()
                .ToString()
                .Should()
                .Be(expectation);
        }
    }
}