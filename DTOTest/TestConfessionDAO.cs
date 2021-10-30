using DAOLibrary.Repository;
using NUnit.Framework;

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
            var confessions = confessionRepository.Get();
            Assert.AreEqual(0, confessions.Count);
        }
    }
}
