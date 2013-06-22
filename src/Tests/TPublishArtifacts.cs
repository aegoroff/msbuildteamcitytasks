/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2013 Alexander Egorov
 */

using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks;
using NMock;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TPublishArtifacts : TTask
    {
        private Mock<ITaskItem> item1;
        private Mock<ITaskItem> item2;
        private PublishArtifacts task;


        [SetUp]
        public void ThisSetup()
        {
            Setup();
            item1 = Mockery.CreateMock<ITaskItem>();
            item2 = Mockery.CreateMock<ITaskItem>();
            task = new PublishArtifacts(Logger.MockObject);
        }

        [Test]
        public void OneArtifact()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            item1.Expects.One.GetProperty(_=>_.ItemSpec).Will(Return.Value("a"));

            task.Artifacts = new[] { item1.MockObject };
            Assert.That(task.Execute());
        }

        [Test]
        public void SeveralArtifacts()
        {
            Logger.Expects.Exactly(2).Method(_=>_.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            item1.Expects.One.GetProperty(_ => _.ItemSpec).Will(Return.Value("a"));
            item2.Expects.One.GetProperty(_ => _.ItemSpec).Will(Return.Value("b"));

            task.Artifacts = new[] { item1.MockObject, item2.MockObject };
            Assert.That(task.Execute());
        }

        [Test]
        public void ArtifactsProperty()
        {
            task.Artifacts = new[] { item1.MockObject, item2.MockObject };
            Assert.That(task.Artifacts, Is.EquivalentTo(new[] { item1.MockObject, item2.MockObject }));
        }
    }
}