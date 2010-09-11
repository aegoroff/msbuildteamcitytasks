/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2010 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks.Internal;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class TSequenceBuilder
	{
		private const string Filter = "(1,2)";
		private const string Head = "(";
		private const string Trail = ")";
		private const string Separator = ",";
		private readonly int[] _values = new[] { 1, 2 };

		[Test]
		public void Normal()
		{
			SequenceBuilder<int> builder = new SequenceBuilder<int>(_values, Separator, Head, Trail);
			Assert.AreEqual(Filter, builder.ToString());
		}

		[Test]
		public void ManyCalls()
		{
			SequenceBuilder<int> builder = new SequenceBuilder<int>(_values, Separator, Head, Trail);
			Assert.AreEqual(Filter, builder.ToString());
			Assert.AreEqual(Filter, builder.ToString());
		}

		[Test]
		public void Empty()
		{
			SequenceBuilder<int> builder = new SequenceBuilder<int>(new int[0], Separator, Head, Trail);
			Assert.AreEqual("", builder.ToString());
		}

		[Test]
		public void EmptyManyCalls()
		{
			SequenceBuilder<int> builder = new SequenceBuilder<int>(new int[0], Separator, Head, Trail);
			Assert.AreEqual("", builder.ToString());
			Assert.AreEqual("", builder.ToString());
		}

		[Test]
		public void NullEnds()
		{
			SequenceBuilder<int> builder = new SequenceBuilder<int>(_values, Separator, null, null);
			Assert.AreEqual("1,2", builder.ToString());
		}

		[Test]
		public void SimplifiedConstructor()
		{
			SequenceBuilder<int> builder = new SequenceBuilder<int>(_values, Separator);
			Assert.AreEqual("1,2", builder.ToString());
		}
	}
}