using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DTOLibrary
{
    public class User
    {
        /// <summary>
        /// The Object ID matched with the one in MongoDB
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Email of the admin
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password of the admin
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Name of the admin
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// When the admin is added
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
