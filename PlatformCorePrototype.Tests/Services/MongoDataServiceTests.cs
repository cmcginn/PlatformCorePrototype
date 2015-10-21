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
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;
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
    public class MongoDataServiceTests : TestBase
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
        public void GetDataCollectionMetadataTest_WhenLinkedList()
        {
            var target = GetTarget();
            var actual = target.GetDataCollectionMetadata("linkedlistdata").Result;
            Assert.IsNotNull(actual);
        }
        [TestMethod]
        public void GetViewDefinitionAsyncTest()
        {
            var target = GetTarget();
            var actual = target.GetViewDefinitionMetadataAsync("segments_view1").Result;
            Assert.IsInstanceOfType(actual, typeof(ViewDefinitionMetadata));
        }
        //[TestMethod]
        //public void GetViewDefinitionAsyncTest_WhenLinkedList()
        //{
        //    var target = GetTarget();
        //    var actual = target.GetViewDefinitionMetadataAsync("linkedlist_account_view1").Result;
        //    Assert.IsInstanceOfType(actual, typeof(LinkedListViewDefinitionMetadata));
        //}

        //[TestMethod]
        //public void GetDataAsync()
        //{
        //    var target = GetTarget();
        //    var strategy = new MongoLinkedListQueryStrategy<dynamic, int>();

        //    //strategy.ViewId = "linkedlist_account_view1";
        //   // strategy.LinkedListMap = new LinkedListMap<int> {Navigation = new List<string> {"Account", "SalesPerson"}};
        //    //strategy.LinkedListSettings = new LinkedListSettings
        //    //{
        //    //    KeyColumn = new DataColumnMetadata {ColumnName = "Account", DataType = Globals.IntegerDataTypeName},
        //    //     MapCollectionName="linkedlistmap"
        //    //};
        //    strategy.IncludeChildren = true;


        //    //var qb = new LinkedListQueryBuilder();
        //    //qb.SelectedPath = new LinkedListPathSpecification {Navigation = "Account", DisplayOrder = 0};

        //    //qb.ViewId = "linkedlist_account_view1";
        //    var actual = target.GetDataAsync(strategy).Result;
        //    Assert.IsTrue(actual.Any());

        //}

        [TestMethod]
        public void GetDataStorageStructureTypeForViewTest_WhenLinkedList()
        {
            var target = GetTarget();
            var actual = target.GetDataStorageStructureTypeForView("linkedlist_account_view1");
            Assert.AreEqual(DataStorageStructureTypes.LinkedList, actual);
        }

        [TestMethod]
        public void GetLinkedListViewDefinitionMetadataTest()
        {
            var target = GetTarget();
            var actual = target.GetLinkedListViewDefinitionMetadata("linkedlist_account_view1").Result;
            Assert.IsTrue(actual.Paths.Any());

        }
        [TestMethod]
        public void GetViewDefinitionMetadataAsync_WhenLinkedList()
        {
            var target = GetTarget();
            var actual = target.GetViewDefinitionMetadataAsync("linkedlist_account_view1").Result;
            Assert.IsInstanceOfType(actual, typeof (LinkedListViewDefinitionMetadata));
        }

        [TestMethod]
        public void GetViewDefinitionAsyncTest_WhenLinkedList()
        {
            //var target = GetTarget();
            //var actual = target.GetViewDefinitionAsync("linkedlist_account_view1").Result;
        //    var linkedListQueryBuilder = actual.QueryBuilder as LinkedListQueryBuilder;
         //   Assert.IsTrue(linkedListQueryBuilder.AvailablePaths.Any());
        }
        
    }
}
