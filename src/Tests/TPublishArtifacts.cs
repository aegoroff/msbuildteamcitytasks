/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2015 Alexander Egorov
 */

using FluentAssertions;
using Microsoft.Build.Framework;
using Moq;
using MSBuild.TeamCity.Tasks;
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
            this.item1 = new Mock<ITaskItem>();
            this.item2 = new Mock<ITaskItem>();
            this.task = new PublishArtifacts(this.Logger.Object);
        }

        [Fact]
        public void OneArtifact()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));
            this.item1.SetupGet(_ => _.ItemSpec).Returns("a"); // 1

            this.task.Artifacts = new[] { this.item1.Object };
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void SeveralArtifacts()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>())); // 2
            this.item1.SetupGet(_ => _.ItemSpec).Returns("a");
            this.item2.SetupGet(_ => _.ItemSpec).Returns("b");

            this.task.Artifacts = new[] { this.item1.Object, this.item2.Object };
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void ArtifactsProperty()
        {
            this.task.Artifacts = new[] { this.item1.Object, this.item2.Object };
            this.task.Artifacts.ShouldBeEquivalentTo(new[] { this.item1.Object, this.item2.Object });
        }
    }
}