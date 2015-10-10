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
using JsonReader = MongoDB.Bson.IO.JsonReader;
using JsonWriter = MongoDB.Bson.IO.JsonWriter;
using PlatformCorePrototype.Web.Infrastructure;

namespace PlatformCorePrototype.Tests.Services
{
    /// <summary>
    /// Summary description for DataServiceTests
    /// </summary>
    [TestClass]
    public class DataServiceTests : ServiceTestBase
    {
        public DataServiceTests()
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

        MongoDataService GetTarget()
        {
            return new MongoDataService();
        }

        [TestMethod]
        public void GetDataCollectionMetadataTest()
        {
            var target = GetTarget();


        }

        [TestMethod]
        public void GetStuff()
        {
          
            var client = new MongoClient(Globals.MongoConnectionString);
    
            var db = client.GetDatabase("prototype");
            var items = db.GetCollection<dynamic>("segments");
       
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>();
            //BsonSerializer.RegisterDiscriminatorConvention(typeof (BsonDocument), new ObjectIdDiscriminator());
            var dd = documentSerializer.ValueType;
            var ss = new List<string>();
       
            //items.
            //BsonSerializer.RegisterSerializer<BsonValue>(new MyBsonValueSerializer());
            var stuff = items.FindAsync(new BsonDocument()).Result.ToListAsync().Result;
            //stuff.ForEach(item =>
            //{
            //    var settings = new Newtonsoft.Json.JsonSerializerSettings();
            //    settings.Converters.Add(new JsonNetBsonDocumentConverter());
            //    //settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            //    ss.Add(Newtonsoft.Json.JsonConvert.SerializeObject(item, settings));
            //});
            //var sString = ss.First();
            //var ddl = BsonDocument.Parse(stuff.First().ToString());
       
            //items.UpdateOneAsync()
            //var doc = ss.ToBsonDocument();//Newtonsoft.Json.JsonConvert.DeserializeObject<BsonDocument>(ss.First());
            //var result = stuff.ToJson(new JsonWriterSettings {OutputMode = JsonOutputMode.Strict});
            var cc = "Y";
        }
    }

    public class ObjectIdDiscriminator : IDiscriminatorConvention
    {

        public string ElementName
        {
            get { return null; }
        }

        public Type GetActualType(IBsonReader bsonReader, Type nominalType)
        {
            //throw new NotImplementedException();
            return nominalType;
        }

        public BsonValue GetDiscriminator(Type nominalType, Type actualType)
        {
            return null;
        }
    }
}
