using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Services;
using PlatformCorePrototype.Services.DataStructures;

namespace PlatformCorePrototype.Web.Api
{
    public class ScratchController : ApiController
    {
        public async Task<List<ExpandoObject>> Get(string id)
        {
            

            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<BsonDocument>("collectionMetadata");

            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var md = items.Find(Builders<BsonDocument>.Filter.Eq(x => x["_id"], "linkedlistdata"));

            
            //var builder = new FilterDefinitionBuilder<BsonDocument>();
            var svc = new MongoDataService();
        
            var metadata =
                Mapper.Map<LinkedListDataCollectionMetadata>(md.SingleAsync().Result);
            var v =
                metadata.Views.Single(x => x.ViewId == "linkedlist_account_view1") as LinkedListViewDefinitionMetadata;

            var qb = Mapper.Map<ILinkedListQueryBuilder>(v);
            var selectedSlicers = id.Split('_');
            selectedSlicers.ToList().ForEach(z =>
            {
                qb.SelectedSlicers.Add(qb.AvailableSlicers.Single(x => x.Column.ColumnName == z));
            });

            qb.SelectedPath = qb.AvailablePaths.Single(x => x.Navigation == id.Replace("_","."));
            //
            //qb.SelectedSlicers.Add(qb.AvailableSlicers.Single(x => x.Column.ColumnName == "Product"));
            qb.SelectedMeasures.Add(qb.AvailableMeasures.First());
            var strategy = new MongoLinkedListQueryStrategy();
            strategy.QueryBuilder = qb;

            return await strategy.RunQuery();

        }
    }
}
