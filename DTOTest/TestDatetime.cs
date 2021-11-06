using HOVStoryUtils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOTest
{
    [TestFixture]
    class TestDatetime
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void ConvertDatetime()
        {
            DateTime dateTime = new DateTime(2021, 11, 06, 10, 15, 25);
            DateTime vnTime = Utils.Datetime.ConvertToVn(dateTime);
            Assert.AreEqual(vnTime, new DateTime(2021, 11, 06, 17, 15, 25));
        }
    }
}
