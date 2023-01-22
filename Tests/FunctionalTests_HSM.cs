using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BasicXML.Tests
{
    [TestClass]
    public class FunctionalTests_HSM : FunctionalTestsBase
    {
        [TestInitialize]
        public void Setup()
        {
            var validator = new ViaHSM.XMLValidator();
            validator.logger = System.Console.WriteLine;
            validate = validator.DetermineXML;
        }

    }

}