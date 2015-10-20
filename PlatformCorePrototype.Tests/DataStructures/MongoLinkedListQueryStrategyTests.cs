using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Services.DataStructures;
using PlatformCorePrototype.Tests.Services;

namespace PlatformCorePrototype.Tests.DataStructures
{

    public class MongoLinkedListQueryStrategyAccessor : MongoLinkedListQueryStrategy<dynamic, int>
    {
        public FilterDefinition<dynamic> GetNavigationFilterDefinitionAccessor()
        {
            return this.GetNavigationFilterDefinition();
        }

        public FilterDefinition<dynamic> GetLinkedListMapsFilterDefinitionAccessor()
        {
            return this.GetLinkedListMapsFilterDefinition().Result;
        }

        public List<BsonDocument> GetQueryPipelineAccessor()
        {
            return this.GetQueryPipeline().Result;
        }
    }
    [TestClass]
    public class MongoLinkedListQueryStrategyTests : TestBase
    {
        MongoLinkedListQueryStrategyAccessor GetAccessor()
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<BsonDocument>("collectionMetadata");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            // var fd = builder.ElemMatch<BsonDocument>("Views", new BsonDocument { { "ViewId", "linkedlist_account_view1" } });
            // var collectionMetadataDocument = items.Find(fd).SingleAsync().Result;
            // var collectionMetadata = Mapper.Map<LinkedListDataCollectionMetadata>(collectionMetadataDocument);
            var dataCollectionMetadata = TestHelper.GetDataCollectionMetadata("linkedlistdata");
            var result = new MongoLinkedListQueryStrategyAccessor
            {
                CollectionMetadata = dataCollectionMetadata
                //CollectionName = "linkedlistdata",
                //DataSourceName = "prototype",
                //DataSourceLocation = Globals.MongoConnectionString,
                // LinkedListSettings = new LinkedListSettings {  KeyColumn=new DataColumnMetadata{ ColumnName="Account", DataType=Globals.IntegerDataTypeName}, MapCollectionName="linkedlistmap"}

            };
            return result;
        }
        [TestMethod]
        public void TestMethod1()
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<BsonDocument>("collectionMetadata");
            //var builder = new FilterDefinitionBuilder<BsonDocument>();
            var doc = items.FindAsync(new BsonDocument()).Result.ToListAsync().Result.First();

        }


        [TestMethod]
        public void GetNavigationFilterDefinitionTest()
        {
            var target = GetAccessor();
            target.Path = new List<string> { "Account", "SalesPerson" };
            var definition = target.GetNavigationFilterDefinitionAccessor();
            var actual = TestHelper.ToDocument<dynamic>(definition).ToString();
            var expected = "{ \"Navigation.0\" : \"Account\", \"Navigation.1\" : \"SalesPerson\" }";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetNavigationFilterDefinitionTest_WhenNoNavigation()
        {
            var target = GetAccessor();
            var actual = target.GetNavigationFilterDefinitionAccessor();
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetLinkedListMapsFilterDefinitionTest()
        {
            var target = GetAccessor();
            var result = target.GetLinkedListMapsFilterDefinitionAccessor();
            var actual = TestHelper.ToDocument<dynamic>(result).ToString();
            var expected =
                "{ \"Account\" : { \"$in\" : [1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010] } }";
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void GetQueryPipelineTest()
        {
            var target = GetAccessor();
            target.Path = new List<string> {"Account", "SalesPerson"};
            var result = target.GetQueryPipelineAccessor();
           
            var actualMatchDocument = result.First().ToString();
            var expectedMatchDocument =
                "{ \"$match\" : { \"Account\" : { \"$in\" : [1001, 1002, 1003, 1004, 1005] } } }";
            Assert.AreEqual(expectedMatchDocument, actualMatchDocument);
        }

        [TestMethod]
        public void GetQueryPipelineTest_WhenNoCriteria_AssetPipelineNull()
        {
            var target = GetAccessor();
           
            var result = target.GetQueryPipelineAccessor();
            Assert.IsFalse(result.Any());

        }
        //[TestMethod]
        //public void GetLinkedListQueryPipelineTest()
        //{
        //    var target = GetAccessor();
        //    target.Path = new List<string> { "Account", "SalesPerson" };
        //    var result = target.GetLinkedListQueryPipelineAccessor().First();
        //    var actual = TestHelper.ToDocument<BsonDocument>(result).ToString();
        //    var expected = "{ \"$match\" : { \"Navigation.0\" : \"Account\", \"Navigation.1\" : \"SalesPerson\" } }";
        //    Assert.AreEqual(expected, actual);


        //}

        //[TestMethod]
        //public void GetLinkedListQueryPipelineTest_WhenNoNavigation()
        //{
        //    var target = GetAccessor();

        //    var result = target.GetLinkedListQueryPipelineAccessor();
            
        //    Assert.IsNull(result);


        //}


    }
}
