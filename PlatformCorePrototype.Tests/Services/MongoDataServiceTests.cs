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

        [TestMethod]
        public void DoStuff()
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase("prototype");
            var items = db.GetCollection<dynamic>("segments");
            var pipeline = new List<BsonDocument>();
            pipeline.Add(BsonDocument.Parse("{ '$match': { 'Code' : { '$in' : ['Account Main', 'Statistical'] }}}"));
            //pipeline.Add(BsonDocument.Parse("{ '$group':{'_id':'0', '_ids':{'$addToSet':'$_id'} } }"));

            var aggregation = items.AggregateAsync<dynamic>(pipeline).Result.ToListAsync<dynamic>().Result;
            var ids = aggregation.Select(x => x._id).ToList();
           
            var builder = new FilterDefinitionBuilder<dynamic>();
            var filters = new List<FilterDefinition<dynamic>>();
            var parents = builder.In("_id", ids);
            var children = builder.In("Parents", ids);
            filters.Add(parents);
            filters.Add(children);
            var fd = builder.Or(filters);
            //var fd = builder.Or()
           // var filters = new List<FilterDefinition<dynamic>>();
            //FilterDefinition<dynamic> parents;
           // var parents = builder.In<ObjectId>("_id", ids);
            //actual.ToList().ForEach(x =>
            //{
            //    parents = builder &= builder.In<ObjectId>("_id", x);
            //});
            //var parents = builder.In<List<ObjectId>>("_id",actual.ToList());
            //var children = builder.In<dynamic>("Parents", actual);
            //filters.Add(parents);
            //filters.Add(children);

            //var fd = builder.Or(filters);

            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<dynamic>();
            var result = fd.Render(documentSerializer, BsonSerializer.SerializerRegistry);
            //var pl = new List<BsonDocument> {result};
            //var pipeLineDefinition = new List<BsonDocument>();
            //pipeLineDefinition.Add(new BsonDocument {{"$match", result}});
            //var agg2 = items.AggregateAsync<dynamic>(pipeLineDefinition).Result.ToListAsync().Result;
            

            var nothing = "Y";
            //var result = aggregation.Result.ToListAsync();

        }

    }
}
