/*
 * Created by: egr
 * Created at: 01.05.2009
 * © 2007-2015 Alexander Egorov
 */

using FluentAssertions;
using MSBuild.TeamCity.Tasks.Messages;
using Xunit;

namespace Tests
{
    public class TMessageAttribute
    {
        private const string Name = "n";
        private const string Value = "v";

        [Fact]
        public void NoNameAndValueDefined()
        {
            var attributeItem = new MessageAttributeItem();
            attributeItem.ToString().Should().BeEmpty();
        }

        [Fact]
        public void NameProperty()
        {
            var attributeItem = new MessageAttributeItem { Name = Name };
            attributeItem.Name.Should().Be(Name);
        }

        [Fact]
        public void ValueProperty()
        {
            var attributeItem = new MessageAttributeItem { Value = Value };
            attributeItem.Value.Should().Be(Value);
        }

        [Fact]
        public void NameAndValueDefined()
        {
            var attributeItem = new MessageAttributeItem(Value, Name);
            attributeItem.ToString().Should().Be("n='v'");
        }

        [Theory]
        [InlineData(Value, "'v'")]
        [InlineData(Value + "'", "'v|''")]
        [InlineData(Value + "]", "'v|]'")]
        [InlineData(Value + "|", "'v||'")]
        [InlineData(Value + "\n", "'v|n'")]
        [InlineData(Value + "\r", "'v|r'")]
        [InlineData(Value + "\r\n|']", "'v|r|n|||'|]'")]
        [InlineData(null, "")]
        public void OnlyValueTest(string value, string expected)
        {
            var attributeItem = new MessageAttributeItem { Value = value };
            attributeItem.ToString().Should().Be(expected);
        }

        [Fact]
        public void EqOperator()
        {
            var a1 = new MessageAttributeItem(Value + 1, Name);
            var a2 = new MessageAttributeItem(Value + 2, Name);
            (a1 == a2).Should().BeTrue();
        }

        [Fact]
        public void EqOperatorEmptyObjects()
        {
            var a1 = new MessageAttributeItem();
            var a2 = new MessageAttributeItem();
            (a1 == a2).Should().BeTrue();
        }

        [Fact]
        public void EqOperatorSameObject()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            MessageAttributeItem a2 = a1;
            (a1 == a2).Should().BeTrue();
        }

        [Fact]
        public void EqOperatorNullFirst()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            (null == a1).Should().BeFalse();
        }

        [Fact]
        public void EqOperatorNullSecond()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            (a1 == null).Should().BeFalse();
        }

        [Fact]
        public void NeOperator()
        {
            var a1 = new MessageAttributeItem(Value, Name + 1);
            var a2 = new MessageAttributeItem(Value, Name + 2);
            (a1 != a2).Should().BeTrue();
        }

        [Fact]
        public void NeOperatorFalse()
        {
            var a1 = new MessageAttributeItem(Value, Name + 1);
            MessageAttributeItem a2 = a1;
            (a1 != a2).Should().BeFalse();
        }

        [Fact]
        public void EqualsMethod()
        {
            var a1 = new MessageAttributeItem(Value + 1, Name);
            var a2 = new MessageAttributeItem(Value + 2, Name);
            a1.Equals(a2).Should().BeTrue();
        }

        [Fact]
        public void EqualsMethodFalse()
        {
            var a1 = new MessageAttributeItem(Value, Name + 1);
            var a2 = new MessageAttributeItem(Value, Name + 2);
            a1.Equals(a2).Should().BeFalse();
        }

        [Fact]
        public void EqualsMethodSameObject()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            MessageAttributeItem a2 = a1;
            a1.Equals(a2).Should().BeTrue();
        }

        [Fact]
        public void EqualsMethodEmptyObjects()
        {
            var a1 = new MessageAttributeItem();
            var a2 = new MessageAttributeItem();
            a1.Equals(a2).Should().BeTrue();
        }

        [Fact]
        public void EqualsMethodNullOther()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            const MessageAttributeItem a2 = null;
            a1.Equals(a2).Should().BeFalse();
        }

        [Fact]
        public void EqualsObjMethod()
        {
            var a1 = new MessageAttributeItem(Value + 1, Name);
            object a2 = new MessageAttributeItem(Value + 2, Name);
            a1.Equals(a2).Should().BeTrue();
        }

        [Fact]
        public void EqualsObjMethodSameObject()
        {
            var a1 = new MessageAttributeItem(Value + 1, Name);
            object a2 = a1;
            a1.Equals(a2).Should().BeTrue();
        }

        [Fact]
        public void EqualsObjMethodFalse()
        {
            var a1 = new MessageAttributeItem(Value, Name + 1);
            object a2 = new MessageAttributeItem(Value, Name + 2);
            a1.Equals(a2).Should().BeFalse();
        }

        [Fact]
        public void EqualsObjMethodNullOther()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            const object a2 = null;
            a1.Equals(a2).Should().BeFalse();
        }

        [Fact]
        public void EqualsObjMethodDifferentType()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            a1.Equals(Name).Should().BeFalse();
        }

        [Fact]
        public void GetHashCodeT()
        {
            var a1 = new MessageAttributeItem(Value, Name);
            a1.GetHashCode().Should().Be(Name.GetHashCode());
        }

        [Fact]
        public void GetHashCodeEmptyObject()
        {
            var a1 = new MessageAttributeItem();
            a1.GetHashCode().Should().Be(0);
        }
    }
}