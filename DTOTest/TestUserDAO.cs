using DAOLibrary.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
