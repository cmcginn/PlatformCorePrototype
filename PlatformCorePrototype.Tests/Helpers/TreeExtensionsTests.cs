using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PlatformCorePrototype.Services.DataStructures;
using PlatformCorePrototype.Services.Helpers;
using PlatformCorePrototype.Tests.Services;

namespace PlatformCorePrototype.Tests.Helpers
{
    [TestClass]
    public class TreeExtensionsTests
    {

        BsonDocument GetTestTreeNode()
        {
            return BsonDocument.Parse(TestData.NestedTreeNodeBson);
        }
        [TestMethod]
        public void GetTreeNodeFilterDefinitionTest()
        {
            var node = GetTestTreeNode();

            var fd = node.GetTreeNodeFilterDefinition();

            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>();
            var actual = fd.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();
            Assert.AreEqual(
                "{ \"_id._id\" : { \"$in\" : [ObjectId(\"56154ccccd158746a441b27d\"), ObjectId(\"56154ccccd158746a441b27a\"), ObjectId(\"56154ccccd158746a441b27c\")] } }",
                actual);


        }

        [TestMethod]
        public void GetTreeNodeFilterPathDefinitionTest()
        {
            var source = new List<string> {"One","Two","three"};
            var fd = source.GetTreeNodeFilterPathDefinition();
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>();
            var actual = fd.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();
            Assert.AreEqual("{ \"Path\" : \"One.Two.three\" }", actual);
        }
        
    }
}
