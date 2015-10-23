using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;
using PlatformCorePrototype.Services;
using PlatformCorePrototype.Services.DataStructures;

namespace PlatformCorePrototype.Tests.SystemTests
{
    [TestClass]
    public class FullPipelineExample : TestBase
    {
        private async Task<IDataCollectionMetadata> GetCollectionMetadataByViewId(string viewId)
        {
            IDataService svc = new MongoDataService();
            return await svc.GetCollectionMetadataByViewId(viewId);

        }

        [TestMethod]
        public async Task MapsCorrectly()
        {
            var actual = await GetCollectionMetadataByViewId("linkedlist_account_view1") as LinkedListDataCollectionMetadata;
            //maps correctly?
            Assert.IsNotNull(actual);
            var viewDefinition =
                actual.Views.Single(x => x.ViewId == "linkedlist_account_view1") as LinkedListViewDefinitionMetadata;
            Assert.IsNotNull(viewDefinition);

            Assert.IsTrue(viewDefinition.Paths.Any());
            var accountMeasure = viewDefinition.Measures.Single(x => x.Column.ColumnName == "Amount");
            Assert.IsTrue(accountMeasure.AggregateOperationType == AggregateOperationTypes.Sum);
            Assert.IsTrue(viewDefinition.Slicers.Any());
        }

        [TestMethod]
        public async Task QueryDataReturnsResults()
        {
            var viewId = "linkedlist_account_view1";

            var actual = await GetCollectionMetadataByViewId(viewId);
            var view = actual.Views.Single(x => x.ViewId == viewId);
            var qs = Mapper.Map<IQueryBuilder>(view);
            Assert.IsInstanceOfType(qs, typeof (LinkedListQueryBuilder));
            var strategy = Mapper.Map<MongoLinkedListExpandoObjectQueryStrategy>(qs);
            Assert.IsInstanceOfType(strategy, typeof (IQueryStrategy<ExpandoObject>));
            IDataService svc = new MongoDataService();
            var results =  await svc.GetDataAsync<ExpandoObject>(strategy);
     
            Assert.IsTrue(results.Any());
        }
    }
}