/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2015 Alexander Egorov
 */

using FluentAssertions;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks;
using NMock;
using Xunit;

namespace Tests
{
    public class TPublishArtifacts : TTask
    {
        private readonly Mock<ITaskItem> item1;
        private readonly Mock<ITaskItem> item2;
        private readonly PublishArtifacts task;

        public TPublishArtifacts()
        {
            this.item1 = this.Mockery.CreateMock<ITaskItem>();
            this.item2 = this.Mockery.CreateMock<ITaskItem>();
            this.task = new PublishArtifacts(this.Logger.MockObject);
        }

        [Fact]
        public void OneArtifact()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.item1.Expects.One.GetProperty(_ => _.ItemSpec).Will(Return.Value("a"));

            this.task.Artifacts = new[] { this.item1.MockObject };
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void SeveralArtifacts()
        {
            this.Logger.Expects.Exactly(2).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.item1.Expects.One.GetProperty(_ => _.ItemSpec).Will(Return.Value("a"));
            this.item2.Expects.One.GetProperty(_ => _.ItemSpec).Will(Return.Value("b"));

            this.task.Artifacts = new[] { this.item1.MockObject, this.item2.MockObject };
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void ArtifactsProperty()
        {
            this.task.Artifacts = new[] { this.item1.MockObject, this.item2.MockObject };
            this.task.Artifacts.ShouldBeEquivalentTo(new[] { this.item1.MockObject, this.item2.MockObject });
        }
    }
}