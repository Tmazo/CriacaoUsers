using Repository.Interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class UsersDataContext : IUsersDataContext
    {
        private readonly string _connectionString;
        public UsersDataContext(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        }

        public string ConnectionString => _connectionString;
        public MongoUrl Url => new MongoUrl(_connectionString);
        public MongoClient Client => new MongoClient(Url);
        public IMongoDatabase Database => Client.GetDatabase("usuarios-teste");
        public string GetCollectionName<TEntity>()
        {
            if(Attribute.GetCustomAttribute(typeof(TEntity), typeof(BsonDiscriminatorAttribute)) != null)
            {
                var cm = BsonClassMap.LookupClassMap(typeof(TEntity));
                if (!string.IsNullOrWhiteSpace(cm.Discriminator))
                    return cm.Discriminator;
            }
            var name = typeof(TEntity).Name;
            return name;
        }
        public IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName) => Database.GetCollection<TEntity>(collectionName);
        public IMongoCollection<TEntity> GetCollection<TEntity>() => Database.GetCollection<TEntity>(GetCollectionName<TEntity>());

    }
}
