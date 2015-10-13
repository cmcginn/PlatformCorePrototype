using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Services.DataStructures;
using PlatformCorePrototype.Tests.Services;

namespace PlatformCorePrototype.Tests.DataStructures
{
    public class MongoTreeQueryStrategyAccessor : MongoTreeQueryStrategy
    {
        public BsonDocument GetParentFilterDocument()
        {
            var doc = this.GetParentFilterDefinition();
            var result = this.ToDocument(doc);
            return result;
        }

        public List<BsonDocument> GetParentPipelineDocuments()
        {
            return this.GetParentPipeline();

        }

        public async Task<List<dynamic>> GetParents()
        {
            return await this.GetParentsAsync();
        }

        public BsonDocument GetChildrenFilterDefinitionDocument()
        {
            var filterDefinition = this.GetChildrenFilterDefinition();
            var result = this.ToDocument(filterDefinition);
            return result;
        }

        public BsonDocument GetFilterDefinitionDocument()
        {
            var doc = this.GetFilterDefinition();
            var result = this.ToDocument(doc);
            return result;
        }
    }
    [TestClass]
    public class MongoTreeQueryStrategyTests:ServiceTestBase
    {
        MongoTreeQueryStrategyAccessor GetTarget()
        {
            return new MongoTreeQueryStrategyAccessor
            {
                CollectionName = "tree_strategy_tests",
                DataSourceName = "prototype",
                DataSourceLocation = Globals.MongoConnectionString
            };
        }

       
        List<FilterSpecification> GetWellKnownParentFilters()
        {
            var result = new List<FilterSpecification>();
            var filter1 = new FilterSpecification();
            filter1.Column = new DataColumnMetadata { ColumnName = "TestColumn1", DataType = Globals.StringDatatypeName };
            filter1.DisplayOrder = 0;
            filter1.FilterType = FilterTypes.Value;
            filter1.FilterValues = new List<FilterValue>
            {
                new FilterValue {Active = true, Key = "TestValue1", Value = "TestValue1"},
                new FilterValue {Active = true, Key = "TestValue2", Value = "TestValue2"}
            };
            result.Add(filter1); 
            return result;
        }

        List<FilterSpecification> GetWellKnownSegmentsParentFilters()
        {
            var result = new List<FilterSpecification>();
            var filter1 = new FilterSpecification();
            filter1.Column = new DataColumnMetadata { ColumnName = "Code", DataType = Globals.StringDatatypeName };
            filter1.DisplayOrder = 0;
            filter1.FilterType = FilterTypes.Value;
            filter1.FilterValues = new List<FilterValue>
            {
                new FilterValue {Active = true, Key = "Account Main", Value = "Account Main"},
                new FilterValue {Active = true, Key = "Statistical", Value = "Statistical"}
            };
            result.Add(filter1);
            return result;
        }

        [TestMethod]
        public void ParentFilterDocumentTest()
        {
            var target = GetTarget();
            target.Filters = GetWellKnownParentFilters();
            var actual = target.GetParentFilterDocument().ToString();
            var expected = "{ \"TestColumn1\" : { \"$in\" : [\"TestValue1\", \"TestValue2\"] } }";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParentPipelineDocumentsTest()
        {
            var target = GetTarget();
            target.Filters = GetWellKnownParentFilters();
            var actual = target.GetParentPipelineDocuments();

            var expected = "{ \"$match\" : { \"TestColumn1\" : { \"$in\" : [\"TestValue1\", \"TestValue2\"] } } }";
            Assert.AreEqual(expected, actual.First().ToString());
        }

        [TestMethod]
        public void GetParentsTest()
        {
            var target = GetTarget();
            target.Filters = GetWellKnownSegmentsParentFilters();
            var actual = target.GetParents().Result;
            Assert.IsTrue(actual.Any());
        }

        [TestMethod]
        public void GetChildrenFilterDefinitionDocument()
        {
            var target = GetTarget();
            target.Filters = GetWellKnownSegmentsParentFilters();
            var actual = target.GetChildrenFilterDefinitionDocument().ToString();
            var expected = "{ \"Parents\" : { \"$in\" : [ObjectId(\"561cd0a8cd1587437c632e5b\"), ObjectId(\"561cd0a8cd1587437c632e5c\")] } }";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetFilterDefinitionDocument_WhenNoChildrenIncluded()
        {
            var target = GetTarget();
            target.Filters = GetWellKnownSegmentsParentFilters();
            var expected = "{ \"Code\" : { \"$in\" : [\"Account Main\", \"Statistical\"] } }";
            var actual = target.GetFilterDefinitionDocument().ToString();
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void GetFilterDefinitionDocument_WhenChildrenIncluded()
        {
            var target = GetTarget();
            target.Filters = GetWellKnownSegmentsParentFilters();
            target.IncludeChildren = true;
            var expected =
                "{ \"$or\" : [{ \"Code\" : { \"$in\" : [\"Account Main\", \"Statistical\"] } }, { \"Parents\" : { \"$in\" : [ObjectId(\"561cd0a8cd1587437c632e5b\"), ObjectId(\"561cd0a8cd1587437c632e5c\")] } }] }";
            var actual = target.GetFilterDefinitionDocument().ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RunQueryTest()
        {
            var target = GetTarget();
            target.Filters = GetWellKnownSegmentsParentFilters();
            target.IncludeChildren = true;
            var actual = target.RunQuery().Result;
            Assert.IsTrue(actual.Count > 2);

        }
        //[TestMethod]
        //public void GetParentFilterDefinitionTest()
        //{
           
        //    var target = GetTarget();
        //    target.Filters = GetWellKnownParentFilters();
        //    var filterDefinition = target.GetParentFilterDefinition();
        //    var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<dynamic>();
        //    var actual = filterDefinition.Render(documentSerializer, BsonSerializer.SerializerRegistry);
        //    Assert.AreEqual("{ \"TestColumn1\" : { \"$in\" : [\"TestValue1\", \"TestValue2\"] } }", actual.ToString());

        //}
        //[TestMethod]
        //public void GetParentParentMatchDocumentTest()
        //{

        //    var target = GetTarget();
        //    target.Filters = GetWellKnownParentFilters();
        //    var actual = target.GetParentMatchDocument().ToString();
        //    var expected = "{ \"$match\" : { \"TestColumn1\" : { \"$in\" : [\"TestValue1\", \"TestValue2\"] } } }";
        //    Assert.AreEqual(expected,actual);

        //}

        //[TestMethod]
        //public void GetParentPipelineTest()
        //{

        //    var target = GetTarget();
        //    target.Filters = GetWellKnownParentFilters();
        //    var actual = target.GetParentPipeline().Single().ToString();
        //    var expected = "{ \"$match\" : { \"TestColumn1\" : { \"$in\" : [\"TestValue1\", \"TestValue2\"] } } }";
        //    Assert.AreEqual(expected, actual);

        //}

        //[TestMethod]
        //public void GetParentsTest()
        //{
        //    var target = GetSegmentsTarget();
        //    target.Filters = GetWellKnownSegmentsParentFilters();
        //    var actual = target.GetParents().Result;
        //    Assert.IsTrue(actual.Any());
        //}

        //[TestMethod]
        //public void GetChildrenFilterDefinitionTest()
        //{
        //    var target = GetSegmentsTarget();
        //    target.Filters = GetWellKnownSegmentsParentFilters();
        //    var filterResult = target.GetChildrenFilterDefinition();
        //    var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<dynamic>();
        //    var actual = filterResult.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();
        //    var expected = "{ \"Parents\" : { \"$in\" : [ObjectId(\"561cd0a8cd1587437c632e5b\"), ObjectId(\"561cd0a8cd1587437c632e5c\")] } }";
        //    Assert.AreEqual(expected, actual);
        //}


    }
}
