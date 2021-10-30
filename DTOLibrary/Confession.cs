using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DTOLibrary
{
    public class Confession
    {
        /// <summary>
        /// The Object ID matched with the one in MongoDB
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Content of the Confession
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Image URL of the Confession, it should be uploaded to Firebase or somewhere else and get the URL
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// The time that the confession was sent
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Status of the Confession
        /// <seealso cref="ConfessionStatus"/>
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Admin name that approved or rejected the confession
        /// </summary>
        public string Admin { get; set; }

        /// <summary>
        /// Note of the admin when rejecting the confession
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The time that the confession was modified (approved or rejected) by the admin. It is the same as the CreatedAt when it is pending for approval
        /// </summary>
        public DateTime ModifiedOn { get; set; }
    }
}
