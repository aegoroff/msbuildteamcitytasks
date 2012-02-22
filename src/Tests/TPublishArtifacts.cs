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
        private ITaskItem _item1;
        private ITaskItem _item2;
        private const string ItemSpec = "ItemSpec";
        private PublishArtifacts _task;


        [SetUp]
        public void ThisSetup()
        {
            Setup();
            _item1 = Mockery.NewMock<ITaskItem>();
            _item2 = Mockery.NewMock<ITaskItem>();
            _task = new PublishArtifacts(Logger);
        }

        [Test]
        public void OneArtifact()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            Expect.Once.On(_item1).GetProperty(ItemSpec).Will(Return.Value("a"));

            _task.Artifacts = new[] { _item1 };
            Assert.That(_task.Execute());
        }

        [Test]
        public void SeveralArtifacts()
        {
            Expect.Exactly(2).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            Expect.Once.On(_item1).GetProperty(ItemSpec).Will(Return.Value("a"));
            Expect.Once.On(_item2).GetProperty(ItemSpec).Will(Return.Value("b"));

            _task.Artifacts = new[] { _item1, _item2 };
            Assert.That(_task.Execute());
        }

        [Test]
        public void ArtifactsProperty()
        {
            _task.Artifacts = new[] { _item1, _item2 };
            Assert.That(_task.Artifacts, Is.EquivalentTo(new[] { _item1, _item2 }));
        }
    }
}