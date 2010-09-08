/*
 * Created by: egr
 * Created at: 01.05.2009
 * © 2007-2009 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks;
using NUnit.Framework;

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
			MessageAttributeItem attributeItem = new MessageAttributeItem();
			Assert.That(attributeItem.ToString(), Is.Empty);
		}

		[Test]
		public void NameProperty()
		{
			MessageAttributeItem attributeItem = new MessageAttributeItem { Name = Name };
			Assert.That(attributeItem.Name, Is.EqualTo(Name));
		}
		
		[Test]
		public void ValueProperty()
		{
			MessageAttributeItem attributeItem = new MessageAttributeItem { Value = Value };
			Assert.That(attributeItem.Value, Is.EqualTo(Value));
		}

		[Test]
		public void NoNameDefined()
		{
			MessageAttributeItem attributeItem = new MessageAttributeItem(Value);
			Assert.That(attributeItem.ToString(), Is.EqualTo("'v'"));
		}
		
		[Test]
		public void NameAndValueDefined()
		{
			MessageAttributeItem attributeItem = new MessageAttributeItem(Name, Value);
			Assert.That(attributeItem.ToString(), Is.EqualTo("n='v'"));
		}

		[Test]
		public void EscapeApos()
		{
			MessageAttributeItem attributeItem = new MessageAttributeItem(Value + "'");
			Assert.That(attributeItem.ToString(), Is.EqualTo("'v|''"));
		}
		
		[Test]
		public void EscapeClosingBracket()
		{
			MessageAttributeItem attributeItem = new MessageAttributeItem(Value + "]");
			Assert.That(attributeItem.ToString(), Is.EqualTo("'v|]'"));
		}
		
		[Test]
		public void EscapeVerticalLine()
		{
			MessageAttributeItem attributeItem = new MessageAttributeItem(Value + "|");
			Assert.That(attributeItem.ToString(), Is.EqualTo("'v||'"));
		}
		
		[Test]
		public void EscapeN()
		{
			MessageAttributeItem attributeItem = new MessageAttributeItem(Value + "\n");
			Assert.That(attributeItem.ToString(), Is.EqualTo("'v|n'"));
		}
		
		[Test]
		public void EscapeR()
		{
			MessageAttributeItem attributeItem = new MessageAttributeItem(Value + "\r");
			Assert.That(attributeItem.ToString(), Is.EqualTo("'v|r'"));
		}
		
		[Test]
		public void EscapeAlltogether()
		{
			MessageAttributeItem attributeItem = new MessageAttributeItem(Value + "\r\n|']");
			Assert.That(attributeItem.ToString(), Is.EqualTo("'v|r|n|||'|]'"));
		}

		[Test]
		public void EqOperator()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name, Value + 1);
			MessageAttributeItem a2 = new MessageAttributeItem(Name, Value + 2);
			Assert.That(a1 == a2);
		}
		
		[Test]
		public void EqOperatorEmptyObjects()
		{
			MessageAttributeItem a1 = new MessageAttributeItem();
			MessageAttributeItem a2 = new MessageAttributeItem();
			Assert.That(a1 == a2);
		}

		[Test]
		public void EqOperatorSameObject()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name, Value);
			MessageAttributeItem a2 = a1;
			Assert.That(a1 == a2);
		}
		
		[Test]
		public void EqOperatorNullFirst()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name, Value);
			Assert.That(null == a1, Is.False);
		}
		
		[Test]
		public void EqOperatorNullSecond()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name, Value);
			Assert.That(a1 == null, Is.False);
		}

		[Test]
		public void NeOperator()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name + 1, Value);
			MessageAttributeItem a2 = new MessageAttributeItem(Name + 2, Value);
			Assert.That(a1 != a2);
		}
		
		[Test]
		public void NeOperatorFalse()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name + 1, Value);
			MessageAttributeItem a2 = a1;
			Assert.That(a1 != a2, Is.False);
		}

		[Test]
		public void EqualsMethod()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name, Value + 1);
			MessageAttributeItem a2 = new MessageAttributeItem(Name, Value + 2);
			Assert.That(a1.Equals(a2));
		}

		[Test]
		public void EqualsMethodFalse()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name + 1, Value);
			MessageAttributeItem a2 = new MessageAttributeItem(Name + 2, Value);
			Assert.That(a1.Equals(a2), Is.False);
		}
		
		[Test]
		public void EqualsMethodSameObject()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name, Value);
			MessageAttributeItem a2 = a1;
			Assert.That(a1.Equals(a2));
		}
		
		[Test]
		public void EqualsMethodEmptyObjects()
		{
			MessageAttributeItem a1 = new MessageAttributeItem();
			MessageAttributeItem a2 = new MessageAttributeItem();
			Assert.That(a1.Equals(a2));
		}

		[Test]
		public void EqualsMethodNullOther()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name, Value);
			const MessageAttributeItem a2 = null;
			Assert.That(a1.Equals(a2), Is.False);
		}

		[Test]
		public void EqualsObjMethod()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name, Value + 1);
			object a2 = new MessageAttributeItem(Name, Value + 2);
			Assert.That(a1.Equals(a2));
		}
		
		[Test]
		public void EqualsObjMethodSameObject()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name, Value + 1);
			object a2 = a1;
			Assert.That(a1.Equals(a2));
		}

		[Test]
		public void EqualsObjMethodFalse()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name + 1, Value);
			object a2 = new MessageAttributeItem(Name + 2, Value);
			Assert.That(a1.Equals(a2), Is.False);
		}

		[Test]
		public void EqualsObjMethodNullOther()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name, Value);
			const object a2 = null;
			Assert.That(a1.Equals(a2), Is.False);
		}
		
		[Test]
		public void EqualsObjMethodDifferentType()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name, Value);
			Assert.That(a1.Equals(Name), Is.False);
		}
		
		[Test]
		public void GetHashCodeT()
		{
			MessageAttributeItem a1 = new MessageAttributeItem(Name, Value);
			Assert.That(a1.GetHashCode(), Is.EqualTo(Name.GetHashCode()));
		}
		
		[Test]
		public void GetHashCodeEmptyObject()
		{
			MessageAttributeItem a1 = new MessageAttributeItem();
			Assert.That(a1.GetHashCode(), Is.EqualTo(0));
		}
	}
}