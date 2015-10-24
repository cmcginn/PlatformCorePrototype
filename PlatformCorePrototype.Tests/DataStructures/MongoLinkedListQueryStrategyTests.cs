using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;
using PlatformCorePrototype.Services;
using PlatformCorePrototype.Services.DataStructures;

namespace PlatformCorePrototype.Tests.DataStructures
{
    public class MongoLinkedListQueryStrategyAccessor : MongoLinkedListExpandoObjectQueryStrategy
    {
        public async Task<List<BsonDocument>> GetLinkedListMapAccessor()
        {
            return await this.GetLinkedListMap();
        }
    }

    [TestClass]
    public class MongoLinkedListQueryStrategyTests:TestBase
    {
        MongoLinkedListQueryStrategyAccessor GetTarget()
        {
            var result = new MongoLinkedListQueryStrategyAccessor();
            IDataService svc = new MongoDataService();
            var viewId = "linkedlist_account_view1";

            var collectionMetadata =
                svc.GetCollectionMetadataByViewId(viewId).Result as LinkedListDataCollectionMetadata;
            var view = collectionMetadata.Views.Single(x => x.ViewId == viewId);
            var qb= Mapper.Map<IQueryBuilder>(view);
            Assert.IsInstanceOfType(qb, typeof(LinkedListQueryBuilder));
            result.QueryBuilder = qb;
            return result;
        }

        [TestMethod]
        public void GetLinkedListMapTest()
        {
            var target = GetTarget();
           // target.
        }
        
    }

   
    
}