using System;
using BasicXML;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicXML.Tests
{
    [TestClass] public class ValidationModelTest
    {

        ValidationModel model;

        [TestMethod] public void Test_isComplete()
            => Assert.IsTrue(model.isComplete);

        [TestMethod] public void Test_isComplete_unclosedElement() {
            model.BeginTag();
            model.QualifyTag(isClosing: false);
            model.EndTag();
            Assert.IsFalse(model.isComplete);
        }

         [TestMethod] public void Test_isComplete_closedElement() {
            model.BeginTag();
            model.QualifyTag(isClosing: false);
            model.EndTag();
            model.BeginTag();
            model.QualifyTag(isClosing: true);
            model.EndTag();
            Assert.IsTrue(model.isComplete);
        }

        [TestMethod] public void Test_isComplete_unclosedTag() {
            model.BeginTag();
            Assert.IsFalse(model.isComplete);
        }

        [TestMethod] public void Test_BeginTag()
        {
            model.BeginTag();
            Assert.IsNotNull(model._currentTag);
        }

        [TestMethod] public void Test_EndTag_unqualifiedTag() {
            Assert.ThrowsException<InvalidOperationException>( 
                () => model.EndTag() 
            );
        }

        [TestMethod] public void Test_EndTag_startTag(){
            model.BeginTag();
            model.QualifyTag(isClosing: false);
            model.EndTag();
            Assert.IsTrue(model._currentTag == null);
        }

        [TestMethod] public void Test_EndTag_endTagWhenStackEmpty() {
            model.BeginTag();
            model.QualifyTag(isClosing: true);
            Assert.ThrowsException<InvalidOperationException>(
                () => model.EndTag()
            );
        }

        [TestMethod]
        public void Test_EndTag_endTag()
        {
            model.BeginTag();
            model.QualifyTag(isClosing: false);
            model.EndTag();
            model.BeginTag();
            model.QualifyTag(isClosing: true);
            model.EndTag();
        }

        [TestMethod]
        public void Test_ExtendTag_noCurrentTag() {
            Assert.ThrowsException<NullReferenceException>(
                () => model.ExtendTag('a')
           );
        }

        [TestMethod]
        public void Test_ExtendTag()
        {
            model.BeginTag();
            model.ExtendTag('a');
        }

        [TestMethod]
        public void Test_QualifyTag_closing()
        {
            //model.BeginTag();
            model.QualifyTag(isClosing: true);
            Assert.IsTrue(model._isClosingTag.Value);
        }

        [TestMethod]
        public void Test_QualifyTag_notClosing()
        {
            //model.BeginTag();
            model.QualifyTag(isClosing: false);
            Assert.IsFalse(model._isClosingTag.Value);
        }

        [TestInitialize]
        public void Setup()
            => model = new ValidationModel();

    }
}
