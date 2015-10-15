/*
 * Created by: egr
 * Created at: 01.05.2009
 * © 2007-2015 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks.Messages;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TMessageAttribute
    {
        private const string Name = "n";
        private const string Value = "v";

        [Test]
        public void NoNameAndValueDefined()
        {
            var attributeItem = new MessageAttributeItem();
            Assert.That(attributeItem.ToString(), Is.Empty);
        }

        [Test]
        public void NameProperty()
        {
            var attributeItem = new MessageAttributeItem { Name = Name };
            Assert.That(attributeItem.Name, Is.EqualTo(Name));
        }

        [Test]
        public void ValueProperty()
        {
            var attributeItem = new MessageAttributeItem { Value = Value };
            Assert.That(attributeItem.Value, Is.EqualTo(Value));
        }

        [Test]
        public void NameAndValueDefined()
        {
            var attributeItem = new MessageAttributeItem(Value, Name);
            Assert.That(attributeItem.ToString(), Is.EqualTo("n='v'"));
        }

        [TestCase(Value, "'v'")]
        [TestCase(Value + "'", "'v|''")]
        [TestCase(Value + "]", "'v|]'")]
        [TestCase(Value + "|", "'v||'")]
        [TestCase(Value + "\n", "'v|n'")]
        [TestCase(Value + "\r", "'v|r'")]
        [TestCase(Value + "\r\n|']", "'v|r|n|||'|]'")]
        [TestCase(null, "")]
        public void OnlyValueTest(string value, string expected)
        {
            var attributeItem = new MessageAttributeItem { Value = value };
            Assert.That(attributeItem.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void EqOperator()
        {
            var a1 = new MessageAttributeItem(Value + 1, Name);
            var a2 = new MessageAttributeItem(Value + 2, Name);
            Assert.That(a1 == a2);
        }

        [Test]
        public void EqOperatorEmptyObjects()
        {
            var a1 = new MessageAttributeItem();
            var a2 = new MessageAttributeItem();
            Assert.That(a1 == a2);
        }

        [Test]
        public void EqOperatorSameObject()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            MessageAttributeItem a2 = a1;
            Assert.That(a1 == a2);
        }

        [Test]
        public void EqOperatorNullFirst()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            Assert.That(null == a1, Is.False);
        }

        [Test]
        public void EqOperatorNullSecond()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            Assert.That(a1 == null, Is.False);
        }

        [Test]
        public void NeOperator()
        {
            var a1 = new MessageAttributeItem(Value, Name + 1);
            var a2 = new MessageAttributeItem(Value, Name + 2);
            Assert.That(a1 != a2);
        }

        [Test]
        public void NeOperatorFalse()
        {
            var a1 = new MessageAttributeItem(Value, Name + 1);
            MessageAttributeItem a2 = a1;
            Assert.That(a1 != a2, Is.False);
        }

        [Test]
        public void EqualsMethod()
        {
            var a1 = new MessageAttributeItem(Value + 1, Name);
            var a2 = new MessageAttributeItem(Value + 2, Name);
            Assert.That(a1.Equals(a2));
        }

        [Test]
        public void EqualsMethodFalse()
        {
            var a1 = new MessageAttributeItem(Value, Name + 1);
            var a2 = new MessageAttributeItem(Value, Name + 2);
            Assert.That(a1.Equals(a2), Is.False);
        }

        [Test]
        public void EqualsMethodSameObject()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            MessageAttributeItem a2 = a1;
            Assert.That(a1.Equals(a2));
        }

        [Test]
        public void EqualsMethodEmptyObjects()
        {
            var a1 = new MessageAttributeItem();
            var a2 = new MessageAttributeItem();
            Assert.That(a1.Equals(a2));
        }

        [Test]
        public void EqualsMethodNullOther()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            const MessageAttributeItem a2 = null;
            Assert.That(a1.Equals(a2), Is.False);
        }

        [Test]
        public void EqualsObjMethod()
        {
            var a1 = new MessageAttributeItem(Value + 1, Name);
            object a2 = new MessageAttributeItem(Value + 2, Name);
            Assert.That(a1.Equals(a2));
        }

        [Test]
        public void EqualsObjMethodSameObject()
        {
            var a1 = new MessageAttributeItem(Value + 1, Name);
            object a2 = a1;
            Assert.That(a1.Equals(a2));
        }

        [Test]
        public void EqualsObjMethodFalse()
        {
            var a1 = new MessageAttributeItem(Value, Name + 1);
            object a2 = new MessageAttributeItem(Value, Name + 2);
            Assert.That(a1.Equals(a2), Is.False);
        }

        [Test]
        public void EqualsObjMethodNullOther()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            const object a2 = null;
            Assert.That(a1.Equals(a2), Is.False);
        }

        [Test]
        public void EqualsObjMethodDifferentType()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            Assert.That(a1.Equals(Name), Is.False);
        }

        [Test]
        public void GetHashCodeT()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            Assert.That(a1.GetHashCode(), Is.EqualTo(Name.GetHashCode()));
        }

        [Test]
        public void GetHashCodeEmptyObject()
        {
            var a1 = new MessageAttributeItem();
            Assert.That(a1.GetHashCode(), Is.EqualTo(0));
        }
    }
}