using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Services.DataStructures;

namespace PlatformCorePrototype.Tests.DataStructures
{
    /// <summary>
    /// Summary description for MongoQueryDefinitionTests
    /// </summary>
    [TestClass]
    public class MongoQueryDefinitionTests
    {
        public MongoQueryDefinitionTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        IMongoQueryDefinition GetTarget()
        {
            var result = new MongoQueryDefinition();

            return result;
        }
        [TestMethod]
        public void TestGetMatchDocument_WhenTree()
        {
            var target = GetTarget();
            var filterSpec1 = new FilterSpecification();
            filterSpec1.Column = new DataColumnMetadata {ColumnName = "Test1", DataType = Globals.StringDatatypeName};
            filterSpec1.FilterValues.Add(new FilterValue
            {
                Active = true,
                Key = "test",
                Value = "test"
            });
            target.Filters.Add(filterSpec1);
            var match = target.GetMatchDocument();
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>();
            var actual = match.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();
            Assert.AreEqual("{ \"Test1\" : { \"$in\" : [\"test\"] } }",actual);

        }
        [TestMethod]
        public void TestGetMatchDocument_WhenTree_And_MultipleFilters()
        {
            var target = GetTarget();
         
            var filterSpec1 = new FilterSpecification();
            filterSpec1.Column = new DataColumnMetadata { ColumnName = "Test1", DataType = Globals.StringDatatypeName };
            filterSpec1.FilterValues.Add(new FilterValue
            {
                Active = true,
                Key = "test",
                Value = "test"
            });
            var filterSpec2 = new FilterSpecification();
            filterSpec2.Column = new DataColumnMetadata {ColumnName = "Test2", DataType = Globals.StringDatatypeName};
            filterSpec2.FilterValues.Add(new FilterValue
            {
                Active = true,
                Key = "TestValue",
                Value = "TestValue"
            });
            filterSpec2.FilterValues.Add(new FilterValue
            {
                Active = true,
                Key = "TestValue2",
                Value = "TestValue2"
            });
            target.Filters.Add(filterSpec1);
            target.Filters.Add(filterSpec2);

            var match = target.GetMatchDocument();
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>();
            var actual = match.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();
            Assert.AreEqual("{ \"Test1\" : { \"$in\" : [\"test\"] }, \"Test2\" : { \"$in\" : [\"TestValue\", \"TestValue2\"] } }", actual);

        }
    }
}
