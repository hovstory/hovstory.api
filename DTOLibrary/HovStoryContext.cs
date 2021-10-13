using HOVStoryConfiguration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLibrary
{
    /// <summary>
    /// Database Context class for accessing collections in MongoDB
    /// </summary>
    public class HovStoryContext
    {
        private readonly IMongoDatabase _database;

        public HovStoryContext()
        {
            var client = new MongoClient(Configuration.ConnectionString);
            _database = client.GetDatabase(Configuration.DatabaseName);
        }

        /// <summary>
        /// The Collection for Confessions
        /// </summary>
        public IMongoCollection<Confession> Confessions 
            => _database.GetCollection<Confession>(Configuration.ConfessionsTableName);

        /// <summary>
        /// The Collection for Users
        /// </summary>
        public IMongoCollection<User> Users
            => _database.GetCollection<User>(Configuration.UsersTableName);
    }
}
