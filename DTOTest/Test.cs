using DTOLibrary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTOUnitTest
{
    [TestFixture]
    public class TestConfessionStatus
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        [TestCase("A")]
        [TestCase("P")]
        [TestCase("R")]
        
        public void IsValidStatus(string status)
        {
            bool isValid = ConfessionStatus.CheckStatus.IsMatch(status);
            Assert.IsTrue(isValid);
        }
        [Test]
        [TestCase("Abc")]
        public void IsInvalidStatus(string status)
        {
            bool isValid = ConfessionStatus.CheckStatus.IsMatch(status);
            Assert.IsFalse(isValid);
        }

    }
}
