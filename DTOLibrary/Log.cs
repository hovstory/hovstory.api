using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DTOLibrary
{
    public class Log
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }
        public string Operator { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
