using DAOLibrary.Repository;
using DTOLibrary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOTest
{
    [TestFixture]
    public class TestConfessionDAO
    {
        private IConfessionRepository confessionRepository;
        [SetUp]
        public void SetUp()
        {
            confessionRepository = new ConfessionRepository();
        }

        [Test]
        public void TestGetConfessions()
        {
            var confessions = confessionRepository.GetConfessions();
            Assert.AreEqual(0, confessions.Count);
        }
    }
}
