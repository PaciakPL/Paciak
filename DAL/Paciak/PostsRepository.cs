﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Configuration;
using DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL.Paciak
{
    public class PostsRepository : IPostsRepository
    {
        private readonly IDbProvider dbProvider;
        private const string DbName = "forume";

        public PostsRepository(IDbProvider dbProvider)
        {
            this.dbProvider = dbProvider;
        }

        public async Task<IEnumerable<Post>> GetTopicPosts(string topicId)
        {
            var objectsCollection = dbProvider.GetDatabase(DbName).GetCollection<BsonDocument>("objects");

            var postIdsFilter = Builders<BsonDocument>.Filter.Eq("_key", $"tid:{topicId}:posts");
            using var postIds = await objectsCollection.FindAsync<PostBare>(postIdsFilter);

            var postsByIdsFilter = Builders<BsonDocument>.Filter.In("_key", postIds.ToEnumerable().Select(x => $"post:{x.Value}").ToArray());
            using var postsCollection = await objectsCollection.FindAsync<Post>(postsByIdsFilter);

            return postsCollection.ToList();
        }
    }
}