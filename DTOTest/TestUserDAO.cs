using DAOLibrary.Repository;
using NUnit.Framework;

namespace DTOTest
{
    [TestFixture]
    public class TestUserDAO
    {
        private IUserRepository userRepository;
        [SetUp]
        public void SetUp()
        {
            userRepository = new UserRepository();
        }

        [Test]
        public void TestGetUser()
        {
            var user = userRepository.Get("n.tranphongse@gmail.com");
            Assert.IsNull(user);
        }
    }
}
