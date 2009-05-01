/*
 * Created by: egr
 * Created at: 01.05.2009
 * © 2007-2009 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tests
{
	[TestFixture]
	public class TMessageAttribute
	{
		const string Name = "n";
		const string Value = "v";
		
		[Test]
		public void NoNameAndValueDefined()
		{
			MessageAttribute attribute = new MessageAttribute();
			Assert.That(attribute.ToString(), Is.Null);
		}

		[Test]
		public void NameProperty()
		{
			MessageAttribute attribute = new MessageAttribute { Name = Name };
			Assert.That(attribute.Name, Is.EqualTo(Name));
		}
		
		[Test]
		public void ValueProperty()
		{
			MessageAttribute attribute = new MessageAttribute { Value = Value };
			Assert.That(attribute.Value, Is.EqualTo(Value));
		}

		[Test]
		public void NoNameDefined()
		{
			MessageAttribute attribute = new MessageAttribute(Value);
			Assert.That(attribute.ToString(), Is.EqualTo("'v'"));
		}
		
		[Test]
		public void NameAndValueDefined()
		{
			MessageAttribute attribute = new MessageAttribute(Name, Value);
			Assert.That(attribute.ToString(), Is.EqualTo("n='v'"));
		}

		[Test]
		public void EscapeApos()
		{
			MessageAttribute attribute = new MessageAttribute(Value + "'");
			Assert.That(attribute.ToString(), Is.EqualTo("'v|''"));
		}
		
		[Test]
		public void EscapeClosingBracket()
		{
			MessageAttribute attribute = new MessageAttribute(Value + "]");
			Assert.That(attribute.ToString(), Is.EqualTo("'v|]'"));
		}
		
		[Test]
		public void EscapeVerticalLine()
		{
			MessageAttribute attribute = new MessageAttribute(Value + "|");
			Assert.That(attribute.ToString(), Is.EqualTo("'v||'"));
		}
		
		[Test]
		public void EscapeN()
		{
			MessageAttribute attribute = new MessageAttribute(Value + "\n");
			Assert.That(attribute.ToString(), Is.EqualTo("'v|n'"));
		}
		
		[Test]
		public void EscapeR()
		{
			MessageAttribute attribute = new MessageAttribute(Value + "\r");
			Assert.That(attribute.ToString(), Is.EqualTo("'v|r'"));
		}
		
		[Test]
		public void EscapeAlltogether()
		{
			MessageAttribute attribute = new MessageAttribute(Value + "\r\n|']");
			Assert.That(attribute.ToString(), Is.EqualTo("'v|r|n|||'|]'"));
		}
	}
}