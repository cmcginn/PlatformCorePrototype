﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace PlatformCorePrototype.Services.Helpers
{
    public static class BsonExtensions
    {
        public static BsonDocument Serialize<T>(this FilterDefinition<T> source)
        {
            var serializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return source.Render(serializer, BsonSerializer.SerializerRegistry);
        }
    }
}