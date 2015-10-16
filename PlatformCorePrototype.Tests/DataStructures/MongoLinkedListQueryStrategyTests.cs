using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Services.DataStructures;
using PlatformCorePrototype.Tests.Services;

namespace PlatformCorePrototype.Tests.DataStructures
{

    public class MongoLinkedListQueryStrategyAccessor : MongoLinkedListQueryStrategy<dynamic, int>
    {
        public BsonDocument GetMapFilterDefinitionDocument()
        {
            var doc = this.GetMapFilterDefinition();
            return this.ToDocument(doc);
        }

        public List<BsonDocument> GetMapFilterPipelineDocuments()
        {
            var docs = this.GetMapFilterPipeline();
            return docs;
        }

        public async Task<List<LinkedListMap<int>>> GetLinkListMapsAccessor()
        {
            return await this.GetLinkListMaps();
        }

        public BsonDocument GetListFilterDefinitionDocument()
        {
            return this.GetListMatchDocument();
     
        }
    }
    [TestClass]
    public class MongoLinkedListQueryStrategyTests:ServiceTestBase
    {
        MongoLinkedListQueryStrategyAccessor GetAccessor()
        {
            var result = new MongoLinkedListQueryStrategyAccessor
            {
                CollectionName = "linkedlistdata",
                DataSourceName = "prototype",
                DataSourceLocation = Globals.MongoConnectionString,
                LinkedListSettings = new LinkedListSettings {  KeyColumn=new DataColumnMetadata{ ColumnName="Account", DataType=Globals.IntegerDataTypeName}, MapCollectionName="linkedlistmap"}
      
            };
            return result;
        }
        [TestMethod]
        public void TestMethod1()
        {
            var target = GetAccessor();
            target.LinkedListMap.Key = 1000;

        }

        [TestMethod]
        public void GetMapFilterDefinitionDocument_WhenPathOnly()
        {
            var target = GetAccessor();
            target.LinkedListMap = new LinkedListMap<int>
            {
        
                Navigation = new List<string> {"Account", "Product", "SalesPerson"}
            };
            var actual = target.GetMapFilterDefinitionDocument().ToString();
            var expected = "{ \"Navigation\" : [\"Account\", \"Product\", \"SalesPerson\"] }";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMapFilterDefinitionDocument_WhenKeyOnly()
        {
            var target = GetAccessor();
            target.LinkedListMap = new LinkedListMap<int>
            {
                Key = 1000
            };
            var actual = target.GetMapFilterDefinitionDocument().ToString();
            var expected = "{ \"Key\" : 1000 }";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMapFilterDefinitionDocument()
        {
            var target = GetAccessor();
            target.LinkedListMap = new LinkedListMap<int>
            {
                Key = 1000,
                Navigation = new List<string> { "Account", "Product", "SalesPerson" }
            };
            var actual = target.GetMapFilterDefinitionDocument().ToString();
            var expected = "{ \"Navigation\" : [\"Account\", \"Product\", \"SalesPerson\"], \"Key\" : 1000 }";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetMapFilterPipelineTest()
        {
             var target = GetAccessor();
            target.LinkedListMap = new LinkedListMap<int>
            {
                Key = 1000,
                Navigation = new List<string> { "Account", "Product", "SalesPerson" }
            };
            var actual = target.GetMapFilterPipelineDocuments();
            var match = actual.First().ToString();
            var project = actual.ElementAt(1).ToString();
            var expected =
                "{ \"$match\" : { \"Navigation\" : [\"Account\", \"Product\", \"SalesPerson\"], \"Key\" : 1000 } }";
            Assert.AreEqual(expected, match, "Match documents not equal");
            expected = "{ \"$project\" : { \"$project\" : { \"_id\" : 0, \"Key\" : \"$Key\" } } }";
            Assert.AreEqual(expected, project, "Projection documents not equal");

        }
        [TestMethod]
        public void GetLinkListMapsTest()
        {
            var target = GetAccessor();
            target.LinkedListMap = new LinkedListMap<int>
            {

                Navigation = new List<string> { "Account", "SalesPerson" }
            };
            var actual = target.GetLinkListMapsAccessor().Result;
            Assert.IsTrue(actual.Any());

      
        }

        [TestMethod]
        public void GetListFilterDefinitionDocument()
        {
            var target = GetAccessor();
            target.LinkedListMap = new LinkedListMap<int>
            {
                Navigation = new List<string> { "Account", "SalesPerson" }
            };
            var actual = target.GetListFilterDefinitionDocument().ToString();
            var expected = "{ \"$match\" : { \"Account\" : { \"$in\" : [1001, 1002, 1003, 1004, 1005] } } }";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RunQueryTest()
        {
            var target = GetAccessor();
            target.LinkedListMap = new LinkedListMap<int>
            {
                Navigation = new List<string> { "Account", "SalesPerson" }
            };
            var actual = target.RunQuery().Result;
            Assert.IsTrue(actual.Any());
        }
    }
}
