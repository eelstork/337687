using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BasicXML.Tests
{

    [TestClass]
    public class FunctionalTests
    {

        Func<string, bool> validate;

        [TestMethod] public void Test1() 
            => IsTrue(validate("<Design><Code>hello world</Code></Design>"));

        [TestMethod] public void Test2()
            => IsFalse(validate("<Design><Code>hello world</Code></Design><People>"));

        [TestMethod] public void Test3()
            => IsFalse(validate("<People><Design><Code>hello world</People></Code></Design>"));

        [TestMethod] public void Test4()
            => IsFalse(validate("<People age=\"1\">hello world</People>"));

        // Assumptions

        [TestMethod] public void Assumption1() 
            => IsFalse(validate("<></>"));

        [TestMethod] public void Assumption2() 
            => IsTrue(validate("/"));

        [TestMethod] public void Assumption3() 
            => IsTrue(validate("<Calculus>5/2</Calculus>"));

        [TestMethod] public void Assumption4() 
            => IsTrue(validate("a"));

        [TestMethod] public void Assumption5() 
            => IsTrue(validate("<story>Tout est bien qui \"finit\" bien </story>"));

        [TestMethod] public void Assumption6() 
            => IsTrue(validate("<story author=\"<Angler Thomas>\"></story author=\"<Angler Thomas>\">"));

        [TestMethod] public void Assumption7() 
            => IsTrue(validate("<p>The <em>quick</em> brown fox.</p>"));

        [TestMethod] public void Assumption8() 
            => IsTrue(validate("The <em>quick</em> brown fox"));

        // -------------------------------------------------------------------

        [TestInitialize]
        public void Setup() 
            => validate = new XMLValidator().DetermineXML;
    }
}
