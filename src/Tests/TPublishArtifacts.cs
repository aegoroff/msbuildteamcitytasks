/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2012 Alexander Egorov
 */

using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks;
using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TPublishArtifacts : TTask
    {
        private ITaskItem item1;
        private ITaskItem item2;
        private const string ItemSpec = "ItemSpec";
        private PublishArtifacts task;


        [SetUp]
        public void ThisSetup()
        {
            Setup();
            item1 = Mockery.NewMock<ITaskItem>();
            item2 = Mockery.NewMock<ITaskItem>();
            task = new PublishArtifacts(Logger);
        }

        [Test]
        public void OneArtifact()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            Expect.Once.On(item1).GetProperty(ItemSpec).Will(Return.Value("a"));

            task.Artifacts = new[] { item1 };
            Assert.That(task.Execute());
        }

        [Test]
        public void SeveralArtifacts()
        {
            Expect.Exactly(2).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            Expect.Once.On(item1).GetProperty(ItemSpec).Will(Return.Value("a"));
            Expect.Once.On(item2).GetProperty(ItemSpec).Will(Return.Value("b"));

            task.Artifacts = new[] { item1, item2 };
            Assert.That(task.Execute());
        }

        [Test]
        public void ArtifactsProperty()
        {
            task.Artifacts = new[] { item1, item2 };
            Assert.That(task.Artifacts, Is.EquivalentTo(new[] { item1, item2 }));
        }
    }
}