/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2010 Alexander Egorov
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


		[SetUp]
		public void ThisSetup()
		{
			Setup();
			_item1 = Mockery.NewMock<ITaskItem>();
			_item2 = Mockery.NewMock<ITaskItem>();
		}

		[Test]
		public void OneArtifact()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
			Expect.Once.On(_item1).GetProperty(ItemSpec).Will(Return.Value("a"));

			PublishArtifacts task = new PublishArtifacts(Logger)
			                        	{
			                        		Artifacts = new[] { _item1 }
			                        	};
			Assert.That(task.Execute(), Is.False);
		}

		[Test]
		public void SeveralArtifacts()
		{
			Expect.Exactly(2).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
			Expect.Once.On(_item1).GetProperty(ItemSpec).Will(Return.Value("a"));
			Expect.Once.On(_item2).GetProperty(ItemSpec).Will(Return.Value("b"));

			PublishArtifacts task = new PublishArtifacts(Logger)
			                        	{
			                        		Artifacts = new[] { _item1, _item2 }
			                        	};
			Assert.That(task.Execute(), Is.False);
		}
	}
}