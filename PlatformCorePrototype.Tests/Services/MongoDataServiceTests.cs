using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Services;
using PlatformCorePrototype.Services.Configuration;
using PlatformCorePrototype.Services.DataStructures;
using JsonReader = MongoDB.Bson.IO.JsonReader;
using JsonWriter = MongoDB.Bson.IO.JsonWriter;
using PlatformCorePrototype.Web.Infrastructure;

namespace PlatformCorePrototype.Tests.Services
{
    /// <summary>
    /// Summary description for DataServiceTests
    /// </summary>
    [TestClass]
    public class MongoDataServiceTests : ServiceTestBase
    {
        public MongoDataServiceTests()
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
            get { return testContextInstance; }
            set { testContextInstance = value; }
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

        private MongoDataService GetTarget()
        {
            return new MongoDataService();
        }

        [TestMethod]
        public void GetDataCollectionMetadataTest()
        {
            var target = GetTarget();


        }

        [TestMethod]
        public void GetDataAsyncTest_WhenTree()
        {
            var target = GetTarget();
            var qd = new MongoQueryDefinition();
            var md = TestHelper.GetDataCollectionMetadata("segments");
            Assert.IsNotNull(md,"CollectionMetadata for segments not found");
            Assert.IsTrue(md.DataStorageStructure == Core.DataStructures.DataStorageStructureTypes.Tree,"Expected Tree Data Structure");
            qd.ViewId = "segments_view1";
            qd.DataSource = new Core.DataStructures.DataSourceSettings
            {
                CollectionName = md.DataSourceName,
                DataSourceLocation = md.DataSourceLocation
            };
            var actual = target.GetDataAsync(qd).Result;
            Assert.IsTrue(actual.Any(), "No results");
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(actual);
            Assert.IsTrue(actual.Count == 1, "Expected 1 root with children");
        }


    }
}
