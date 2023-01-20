using System;
using BasicXML;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicXML.Tests
{
    [TestClass]
    public class XMLValidatorTest
    {

        XMLValidator validator;

        [TestMethod] public void Test_DetermineXML_false()
        => Assert.IsTrue(validator.DetermineXML(""));

        [TestMethod] public void Test_DetermineXML_true()
        => Assert.IsFalse(validator.DetermineXML(">"));

        [TestInitialize]
        public void TestDetermineXML()
            => validator = new XMLValidator();

    }
}
