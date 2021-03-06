﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Entities
{
    [BsonIgnoreExtraElements]
    public class PostBare
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("value")]
        public string Value { get; set; }

        [BsonElement("score")]
        public double Score { get; set; }
    }
}