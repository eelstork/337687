using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BasicXML.Tests
{
    [TestClass]
    public class FunctionalTests_MultiParser : FunctionalTestsBase
    {
        [TestInitialize]
        public void Setup()
            => validate = new ViaMultiparser.XMLValidator().DetermineXML;
    }
}
