using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;
using PlatformCorePrototype.Services.DataStructures;

namespace PlatformCorePrototype.Tests.DataStructures
{
    public class MongoLinkedListQueryStrategyAccessor : MongoLinkedListQueryStrategy
    {
        public FilterDefinition<dynamic> GetNavigationFilterDefinitionAccessor()
        {
            return GetNavigationFilterDefinition();
        }

        public FilterDefinition<dynamic> GetLinkedListMapsFilterDefinitionAccessor()
        {
            return GetLinkedListMapsFilterDefinition().Result;
        }

        public List<BsonDocument> GetQueryPipelineAccessor()
        {
            return GetQueryPipeline().Result;
        }

        public BsonDocument GetQueryGroupDocumentAccessor()
        {
            return GetQueryGroupDocument();
        }

        public List<BsonElement> GetQueryMeasureElementsAccessor()
        {
            return GetQueryMeasureElements();
        }

        public List<BsonElement> GetQueryGroupIdElementsAccessor()
        {
            return GetQueryGroupIdElements();
        }
    }

    [TestClass]
    public class MongoLinkedListQueryStrategyTests : TestBase
    {
        private MongoLinkedListQueryStrategyAccessor GetAccessor()
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<BsonDocument>("collectionMetadata");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var dataCollectionMetadata = TestHelper.GetDataCollectionMetadata("linkedlistdata");

            // var qb = new LinkedListQueryBuilder();
            //qb.ViewId = "linkedlist_account_view1";

            var result = new MongoLinkedListQueryStrategyAccessor();
            result.QueryBuilder =
                Mapper.Map<IViewDefinitionMetadata, IQueryBuilder>(
                    dataCollectionMetadata.Views.Single(x => x.ViewId == "linkedlist_account_view1"));
            return result;
        }

        [TestMethod]
        public void TestMethod1()
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<ExpandoObject>("linkedlistdata");
            var builder = new FilterDefinitionBuilder<ExpandoObject>();
            var r = items.Aggregate();
            dynamic grouping = new ExpandoObject();

            var g =
                r.Group<ExpandoObject>(
                    BsonDocument.Parse(
                        "{ '_id' : { 's0' : '$SalesPerson', 's1' : '$Product' }, 'f0' : { '$sum' : '$Amount' } }"));
            r = r.AppendStage<ExpandoObject>(
                BsonDocument.Parse(
                    "{'$group':{ '_id' : { 's0' : '$SalesPerson', 's1' : '$Product' }, 'f0' : { '$sum' : '$Amount' } }}"));
            var result = r.ToListAsync().Result;
        }


        [TestMethod]
        public void GetNavigationFilterDefinitionTest()
        {
            var target = GetAccessor();
            var qb = target.QueryBuilder as LinkedListQueryBuilder;
            qb.SelectedPath = qb.AvailablePaths.First();

            //  target.Path = new List<string> { "Account", "SalesPerson" };
            var definition = target.GetNavigationFilterDefinitionAccessor();
            var actual = TestHelper.ToDocument(definition).ToString();

            var expected = "{ \"Navigation\" : /^Account.SalesPerson/ }";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetNavigationFilterDefinitionTest_WhenExcludeChildren()
        {
            var target = GetAccessor();
            var qb = target.QueryBuilder as LinkedListQueryBuilder;
            qb.SelectedPath = qb.AvailablePaths.First();
            qb.ExcludeChildren = true;
            var definition = target.GetNavigationFilterDefinitionAccessor();
            var actual = TestHelper.ToDocument(definition).ToString();
            var expected = "{ \"Navigation\" : \"Account.SalesPerson\" }";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetNavigationFilterDefinitionTest_WhenNoNavigation()
        {
            var target = GetAccessor();
            //var qb = target.QueryBuilder as LinkedListQueryBuilder;
            //qb.SelectedPath = qb.AvailablePaths.First();
            var actual = target.GetNavigationFilterDefinitionAccessor();
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetLinkedListMapsFilterDefinitionTest()
        {
            var target = GetAccessor();
            var qb = target.QueryBuilder as LinkedListQueryBuilder;
            qb.SelectedPath = qb.AvailablePaths.First();
            var result = target.GetLinkedListMapsFilterDefinitionAccessor();
            var actual = TestHelper.ToDocument(result).ToString();
            var expected =
                "{ \"Account\" : { \"$in\" : [1001, 1002, 1003, 1004, 1005, 1011, 1012, 1013, 1014, 1015] } }";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetQueryPipelineTest()
        {
            var target = GetAccessor();
            var qb = target.QueryBuilder as LinkedListQueryBuilder;
            qb.SelectedPath = qb.AvailablePaths.First();
            qb.SelectedSlicers.Add(qb.AvailableSlicers.Single(x => x.Column.ColumnName == "SalesPerson"));
            qb.SelectedSlicers.Add(qb.AvailableSlicers.Single(x => x.Column.ColumnName == "Product"));
            qb.SelectedMeasures.Add(qb.AvailableMeasures.First());
            var result = target.GetQueryPipelineAccessor();

            var actualMatchDocument = result.First().ToString();
            var expectedMatchDocument =
                "{ \"$match\" : { \"Account\" : { \"$in\" : [1001, 1002, 1003, 1004, 1005, 1011, 1012, 1013, 1014, 1015] } } }";
            var actualGroupDocument = result.ElementAt(1).ToString();
            var expectedGroupDocument =
                "{ \"$group\" : { \"_id\" : { \"slicer_0\" : \"$SalesPerson\", \"slicer_1\" : \"$Product\" }, \"measure_0\" : { \"$sum\" : \"$Amount\" } } }";
            Assert.AreEqual(expectedMatchDocument, actualMatchDocument);
            Assert.AreEqual(expectedGroupDocument, actualGroupDocument);
        }

        [TestMethod]
        public void GetQueryPipelineTest_WhenNoCriteria_AssetPipelineNull()
        {
            var target = GetAccessor();
            var result = target.GetQueryPipelineAccessor();
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetQueryGroupIdElementsTest()
        {
            var target = GetAccessor();
            var qb = target.QueryBuilder as LinkedListQueryBuilder;
            qb.SelectedSlicers.Add(qb.AvailableSlicers.Single(x => x.Column.ColumnName == "SalesPerson"));
            qb.SelectedSlicers.Add(qb.AvailableSlicers.Single(x => x.Column.ColumnName == "Product"));
            var actual = target.GetQueryGroupIdElementsAccessor();
        }

        [TestMethod]
        public void GetQueryMeasureDocumentTest()
        {
            var target = GetAccessor();
            var qb = target.QueryBuilder as LinkedListQueryBuilder;
            qb.SelectedMeasures.Add(qb.AvailableMeasures.First());
            //  var actual = target.GetQueryMeasureDocumentAccessor().ToString();
        }

        [TestMethod]
        public void GetQueryGroupDocumentTest()
        {
            var target = GetAccessor();
            var qb = target.QueryBuilder as LinkedListQueryBuilder;
            qb.SelectedSlicers.Add(qb.AvailableSlicers.Single(x => x.Column.ColumnName == "SalesPerson"));
            qb.SelectedSlicers.Add(qb.AvailableSlicers.Single(x => x.Column.ColumnName == "Product"));
            qb.SelectedMeasures.Add(qb.AvailableMeasures.First());
            var actual = target.GetQueryGroupDocumentAccessor().ToString();
            var expected =
                "{ \"$group\" : { \"_id\" : { \"slicer_0\" : \"$SalesPerson\", \"slicer_1\" : \"$Product\" }, \"measure_0\" : { \"$sum\" : \"$Amount\" } } }";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RunQueryTest()
        {
            var target = GetAccessor();
            var qb = target.QueryBuilder as LinkedListQueryBuilder;
            qb.SelectedPath = qb.AvailablePaths.First();
            qb.SelectedSlicers.Add(qb.AvailableSlicers.Single(x => x.Column.ColumnName == "SalesPerson"));
            qb.SelectedSlicers.Add(qb.AvailableSlicers.Single(x => x.Column.ColumnName == "Product"));
            qb.SelectedMeasures.Add(qb.AvailableMeasures.First());
            var actual = target.RunQuery().Result;

            //  var dd = Newtonsoft.Json.JsonConvert.SerializeObject(actual.First());
            Assert.IsTrue(actual.Any());

            // Assert.IsTrue(actual.Any());
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