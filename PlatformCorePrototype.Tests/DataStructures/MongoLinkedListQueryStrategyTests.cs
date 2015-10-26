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
            ((LinkedListQueryBuilder)result.QueryBuilder).LinkedListMaps =svc.GetLinkedListMaps(viewId).Result;
            return result;
        }

        [TestMethod]
        public async Task GetLinkedListMapTest()
        {
            var target = GetTarget();
            var qb = target.QueryBuilder as ILinkedListQueryBuilder;
            var strategy = Mapper.Map<MongoLinkedListExpandoObjectQueryStrategy>(qb);
            qb.SelectedSlicers.Add(qb.AvailableSlicers.Single(x => x.Column.ColumnName == "Account"));
            
            qb.SelectedNavigationPath = qb.LinkedListMaps.First().NavigationMaps.First().Navigation;
            qb.SelectedNavigation = qb.LinkedListMaps.First();
            qb.SelectedLevel = 2;
            Assert.IsNotNull(strategy.QueryBuilder);
            var actual = await strategy.RunQuery();
            Assert.IsTrue(actual.Any());
            // target.
        }
        
    }

   
    
}