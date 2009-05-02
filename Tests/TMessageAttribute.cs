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

		[Test]
		public void EqOperator()
		{
			MessageAttribute a1 = new MessageAttribute(Name, Value + 1);
			MessageAttribute a2 = new MessageAttribute(Name, Value + 2);
			Assert.That(a1 == a2);
		}
		
		[Test]
		public void EqOperatorEmptyObjects()
		{
			MessageAttribute a1 = new MessageAttribute();
			MessageAttribute a2 = new MessageAttribute();
			Assert.That(a1 == a2);
		}

		[Test]
		public void EqOperatorSameObject()
		{
			MessageAttribute a1 = new MessageAttribute(Name, Value);
			MessageAttribute a2 = a1;
			Assert.That(a1 == a2);
		}
		
		[Test]
		public void EqOperatorNullFirst()
		{
			MessageAttribute a1 = new MessageAttribute(Name, Value);
			Assert.That(null == a1, Is.False);
		}
		
		[Test]
		public void EqOperatorNullSecond()
		{
			MessageAttribute a1 = new MessageAttribute(Name, Value);
			Assert.That(a1 == null, Is.False);
		}

		[Test]
		public void NeOperator()
		{
			MessageAttribute a1 = new MessageAttribute(Name + 1, Value);
			MessageAttribute a2 = new MessageAttribute(Name + 2, Value);
			Assert.That(a1 != a2);
		}
		
		[Test]
		public void NeOperatorFalse()
		{
			MessageAttribute a1 = new MessageAttribute(Name + 1, Value);
			MessageAttribute a2 = a1;
			Assert.That(a1 != a2, Is.False);
		}

		[Test]
		public void EqualsMethod()
		{
			MessageAttribute a1 = new MessageAttribute(Name, Value + 1);
			MessageAttribute a2 = new MessageAttribute(Name, Value + 2);
			Assert.That(a1.Equals(a2));
		}

		[Test]
		public void EqualsMethodFalse()
		{
			MessageAttribute a1 = new MessageAttribute(Name + 1, Value);
			MessageAttribute a2 = new MessageAttribute(Name + 2, Value);
			Assert.That(a1.Equals(a2), Is.False);
		}
		
		[Test]
		public void EqualsMethodSameObject()
		{
			MessageAttribute a1 = new MessageAttribute(Name, Value);
			MessageAttribute a2 = a1;
			Assert.That(a1.Equals(a2));
		}
		
		[Test]
		public void EqualsMethodEmptyObjects()
		{
			MessageAttribute a1 = new MessageAttribute();
			MessageAttribute a2 = new MessageAttribute();
			Assert.That(a1.Equals(a2));
		}

		[Test]
		public void EqualsMethodNullOther()
		{
			MessageAttribute a1 = new MessageAttribute(Name, Value);
			const MessageAttribute a2 = null;
			Assert.That(a1.Equals(a2), Is.False);
		}

		[Test]
		public void EqualsObjMethod()
		{
			MessageAttribute a1 = new MessageAttribute(Name, Value + 1);
			object a2 = new MessageAttribute(Name, Value + 2);
			Assert.That(a1.Equals(a2));
		}
		
		[Test]
		public void EqualsObjMethodSameObject()
		{
			MessageAttribute a1 = new MessageAttribute(Name, Value + 1);
			object a2 = a1;
			Assert.That(a1.Equals(a2));
		}

		[Test]
		public void EqualsObjMethodFalse()
		{
			MessageAttribute a1 = new MessageAttribute(Name + 1, Value);
			object a2 = new MessageAttribute(Name + 2, Value);
			Assert.That(a1.Equals(a2), Is.False);
		}

		[Test]
		public void EqualsObjMethodNullOther()
		{
			MessageAttribute a1 = new MessageAttribute(Name, Value);
			const object a2 = null;
			Assert.That(a1.Equals(a2), Is.False);
		}
		
		[Test]
		public void EqualsObjMethodDifferentType()
		{
			MessageAttribute a1 = new MessageAttribute(Name, Value);
			Assert.That(a1.Equals(Name), Is.False);
		}
		
		[Test]
		public void GetHashCodeT()
		{
			MessageAttribute a1 = new MessageAttribute(Name, Value);
			Assert.That(a1.GetHashCode(), Is.EqualTo(Name.GetHashCode()));
		}
		
		[Test]
		public void GetHashCodeEmptyObject()
		{
			MessageAttribute a1 = new MessageAttribute();
			Assert.That(a1.GetHashCode(), Is.EqualTo(0));
		}
	}
}