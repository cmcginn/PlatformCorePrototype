using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Services
{
    public class MongoDataService:IDataService
    {
        //public async Task<IDataCollectionMetadata> GetDataCollectionMetadata(string collectionName)
        //{
        //    var client = new MongoClient(Globals.MongoConnectionString);
        //    var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
        //    var items = db.GetCollection<BsonDocument>("collectionMetadata");

        //    var md = items.Find(Builders<BsonDocument>.Filter.Eq(x => x["_id"], collectionName));
        //    var dd = await md.SingleAsync();
        //    var r = Mapper.Map<BsonDocument, IDataCollectionMetadata>(dd);
        //    //return Mapper.Map<BsonDocument, dd>();
        //    //var result = md.SingleAsync().ContinueWith<IDataCollectionMetadata>((t) =>
        //    //{
        //    //    return Mapper.Map<BsonDocument, IDataCollectionMetadata>(t.Result);
        //    //});
        //    return r;
        //}



       

    






   
      

        public async Task<IDataCollectionMetadata> GetCollectionMetadataByViewId(string viewId)
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<BsonDocument>("collectionMetadata");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var fd = builder.ElemMatch<BsonDocument>("Views", new BsonDocument {{"ViewId", viewId}});
            var asyncResult = await items.Find(fd).SingleAsync();
            var result = Mapper.Map<IDataCollectionMetadata>(asyncResult);
            return result;

        }
    }
}