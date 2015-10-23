using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Services;

namespace PlatformCorePrototype.Tests
{
    public class TestHelper : TestBase
    {
        public static IDataCollectionMetadata GetDataCollectionMetadata(string collectionName)
        {
            var svc = new MongoDataService();
            return svc.GetDataCollectionMetadata(collectionName).Result;
        }

        public static BsonDocument ToDocument<T>(FilterDefinition<T> source)
        {
            var serializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return source.Render(serializer, BsonSerializer.SerializerRegistry);
        }
    }
}