using HOVStoryConfiguration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOTest
{
    [TestFixture]
    public class TestConfiguration
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestDatabaseName()
        {
            Assert.AreEqual("HOVStory", Configuration.DatabaseName);
        }

        [Test]
        public void TestConnectionString()
        {
            Assert.AreEqual("mongodb+srv://hovstory:HOVStory@humansofvothisaudb.uevma.mongodb.net/HOVStory?retryWrites=true&w=majority", Configuration.ConnectionString);
        }
        [Test]
        public void TestConfessionsName()
        {
            Assert.AreEqual("Confessions", Configuration.ConfessionsTableName);
        }
        [Test]
        public void TestUsersName()
        {
            Assert.AreEqual("Users", Configuration.UsersTableName);
        }
        [Test]
        public void TestApproved()
        {
            Assert.AreEqual("A", Configuration.ConfessionApproved);
        }
        [Test]
        public void TestRejected()
        {
            Assert.AreEqual("R", Configuration.ConfessionRejected);
        }
        [Test]
        public void TestPending()
        {
            Assert.AreEqual("P", Configuration.ConfessionPending);
        }

    }
}
