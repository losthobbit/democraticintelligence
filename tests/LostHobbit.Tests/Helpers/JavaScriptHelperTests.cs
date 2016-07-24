using LostHobbit.Helpers;
using Microsoft.AspNetCore.Html;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostHobbit.Tests.Helpers
{
    [TestFixture]
    public class JavaScriptHelperTests
    {
        [Test]
        public void SerializeObject_returns_Json()
        {
            //Arrange
            const string TestPropertyValue1 = "Hello World!";
            const int TestPropertyValue2 = 53;
            const string TestPropertyValue3 = "I am a child!";
            const int TestPropertyValue4 = 5;
            var parentObject = new TestObject { TestProperty1 = TestPropertyValue1, TestProperty2 = TestPropertyValue2 };
            var childObject = new TestObject { TestProperty1 = TestPropertyValue3, TestProperty2 = TestPropertyValue4, Parent = parentObject };
            parentObject.Child = childObject;

            //Act
            var actual = (HtmlString)JavaScriptHelper.SerializeObject(parentObject);

            //Assert
            Assert.AreEqual("{testProperty1:\"Hello World!\",testProperty2:53,parent:null,child:{testProperty1:\"I am a child!\",testProperty2:5,child:null}}",
                actual.ToString());
        }
    }

    public class TestObject
    {
        public string TestProperty1 { get; set; }
        public int TestProperty2 { get; set; }
        public TestObject Parent { get; set; }
        public TestObject Child { get; set; }
    }
}
