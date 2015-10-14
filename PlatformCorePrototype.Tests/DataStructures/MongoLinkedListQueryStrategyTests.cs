using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
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
                DataSourceLocation = Globals.MongoConnectionString
      
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
    }
}
