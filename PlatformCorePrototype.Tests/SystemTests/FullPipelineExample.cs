using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Tests.SystemTests
{
    [TestClass]
    public class FullPipelineExample:TestBase
    {

        IDataCollectionMetadata GetMetadataByViewId(string viewId)
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<BsonDocument>("collectionMetadata");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var fd = builder.ElemMatch<BsonDocument>("Views", new BsonDocument {{"ViewId", viewId}});
          
            var serializer = BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>();
            var f =  fd.Render(serializer, BsonSerializer.SerializerRegistry).ToString();

            var r = items.Find(fd).SingleAsync().Result;

            var colums = r.GetElement("Columns").Value.AsBsonArray;
            var result = Mapper.Map<IDataCollectionMetadata>(r);
            return result;
        }
        [TestMethod]
        public void TestMethod1()
        {
            GetMetadataByViewId("linkedlist_account_view1");
        }
    }
}
