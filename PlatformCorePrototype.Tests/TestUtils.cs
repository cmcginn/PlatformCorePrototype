using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Tests
{
    [TestClass]
    public class TestUtils : TestBase
    {
        //[TestMethod]
        //public void UpsertLinkedListDataCollectionMetadataTest()
        //{
        //    UpsertLinkedListCollectionMetadata();
        //}
        //[TestMethod]
        //public void UpsertTreeDataCollectionMetadataTest()
        //{
        //    UpsertTreeDataCollectionMetadata();
        //}

        //[TestMethod]
        //public void UpsertLinkedListViewDefinitionMetadataTest()
        //{
        //    UpsertLinkedListViewDefinitionMetadata();
        //}
        //[TestMethod]
        //public void UpsertSegmentsView1ViewDefinitionMetadataTest()
        //{
        //    UpsertSegmentsView1ViewDefinitionMetadata();
        //}
        //static void UpsertCollectionMetadata(DataCollectionMetadata dataCollectionMetadata)
        //{
        //    var client = new MongoClient(Globals.MongoConnectionString);
        //    var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
        //    var items = db.GetCollection<DataCollectionMetadata>("collectionMetadata");
        //    FilterDefinition<DataCollectionMetadata> filter = new BsonDocument("_id", dataCollectionMetadata.Id);
        //    var deleteTask = items.DeleteOneAsync(filter).Result;
        //    var upsertTask = items.InsertOneAsync(dataCollectionMetadata);
        //    Task.WaitAll(upsertTask);

        //}
        //static void UpsertLinkedListCollectionMetadata(LinkedListDataCollectionMetadata dataCollectionMetadata)
        //{
        //    var client = new MongoClient(Globals.MongoConnectionString);
        //    var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
        //    var items = db.GetCollection<LinkedListDataCollectionMetadata>("collectionMetadata");
        //    FilterDefinition<LinkedListDataCollectionMetadata> filter = new BsonDocument("_id", dataCollectionMetadata.Id);
        //    var deleteTask = items.DeleteOneAsync(filter).Result;
        //    var upsertTask = items.InsertOneAsync(dataCollectionMetadata);
        //    Task.WaitAll(upsertTask);

        //}
        //static void UpsertViewDefinitionMetadata(ViewDefinitionMetadata viewDefinitionMetadata)
        //{
        //    var client = new MongoClient(Globals.MongoConnectionString);
        //    var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
        //    var items = db.GetCollection<ViewDefinitionMetadata>("viewDefinitionMetadata");
        //    var deleteTask = items.DeleteOneAsync(x => x.Id == viewDefinitionMetadata.Id).Result;
        //    var upsertTask = items.InsertOneAsync(viewDefinitionMetadata);
        //    Task.WaitAll(upsertTask);
        //}
        //static void UpsertTreeDataCollectionMetadata()
        //{
        //    var dataCollectionMetadata = new DataCollectionMetadata
        //    {
        //        Id = "segments",
        //        DataSourceLocation = Globals.MongoConnectionString,
        //        DataSourceName = "prototype"
        //    };
        //    var code = new DataColumnMetadata
        //    {
        //        ColumnName = "Code",
        //        DataType = Globals.StringDataTypeName

        //    };
        //    dataCollectionMetadata.Columns.Add(code);
        //    UpsertCollectionMetadata(dataCollectionMetadata);
        //}

        //static void UpsertSegmentsView1ViewDefinitionMetadata()
        //{
        //    var viewDefinitionMetadata = new ViewDefinitionMetadata();
        //    var client = new MongoClient(Globals.MongoConnectionString);
        //    var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
        //    var items = db.GetCollection<DataCollectionMetadata>("collectionMetadata");

        //    var dataCollectionMetada = items.Find(x => x.Id == "segments").SingleAsync().Result;

        //    var codeFilter = new FilterSpecification
        //    {
        //        Column = dataCollectionMetada.Columns.Single(x => x.ColumnName == "Code"),
        //        FilterType = FilterTypes.Value,
        //        DisplayOrder = 0

        //    };
        //    // viewDefinitionMetadata.Filters.Add(codeFilter);
        //    viewDefinitionMetadata.Id = "segments_view1";
        //    //viewDefinitionMetadata.MetadataCollectionId = dataCollectionMetada.Id;
        //    UpsertViewDefinitionMetadata(viewDefinitionMetadata);

        //    //var codeFilter = new 
        //}

        //static void UpsertLinkedListCollectionMetadata()
        //{
        //    var collectionMetadata = new LinkedListDataCollectionMetadata
        //    {
        //        Id = "linkedlistdata",
        //        DataSourceLocation = Globals.MongoConnectionString,
        //        DataSourceName = "prototype",
        //        MapCollectionName = "linkedlistmap",
        //        DataStorageType = DataStorageStructureTypes.LinkedList
        //    };
        //    var account = new DataColumnMetadata
        //    {
        //        ColumnName = "Account",
        //        DataType = Globals.IntegerDataTypeName
        //    };
        //    var salesPerson = new DataColumnMetadata
        //    {
        //        ColumnName = "SalesPerson",
        //        DataType = Globals.StringDataTypeName
        //    };
        //    var product = new DataColumnMetadata
        //    {
        //        ColumnName = "Product",
        //        DataType = Globals.StringDataTypeName
        //    };
        //    var amount = new DataColumnMetadata
        //    {
        //        ColumnName = "Amount",
        //        DataType = Globals.DoubleDataTypeName
        //    };

        //    collectionMetadata.KeyColumn = account;

        //    collectionMetadata.Columns.Add(account);
        //    collectionMetadata.Columns.Add(salesPerson);
        //    collectionMetadata.Columns.Add(product);
        //    collectionMetadata.Columns.Add(amount);
        //    //collectionMetadata.LinkedListSettings.KeyColumn = account;
        //    //collectionMetadata.KeyColumn = account;
        //    UpsertLinkedListCollectionMetadata(collectionMetadata);

        //}

        //static void UpsertLinkedListViewDefinitionMetadata()
        //{
        //    var viewDefinitionMetadata = new LinkedListViewDefinitionMetadata
        //    {
        //        Id = "linkedlist_account_view1",
        //       // MetadataCollectionId = "linkedlistdata"
        //    };
        //    viewDefinitionMetadata.Paths = new List<LinkedListPathSpecification>
        //    {
        //        new LinkedListPathSpecification {DisplayOrder = 0, Navigation = "Account.SalesPerson.Product"},
        //        new LinkedListPathSpecification {DisplayOrder = 1, Navigation = "Account.Product"}
        //    };

        //    var client = new MongoClient(Globals.MongoConnectionString);
        //    var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
        //    var items = db.GetCollection<LinkedListViewDefinitionMetadata>("viewDefinitionMetadata");

        //    Task.WaitAll(items.InsertOneAsync(viewDefinitionMetadata));

        //}

        [TestMethod]
        public void UpsertLinkedListDataCollectionMetadataTest()
        {
            UpsertLinkedListDataCollectionMetadata();
        }

        private static void UpsertLinkedListDataCollectionMetadata()
        {
            var collectionMetadata = new LinkedListDataCollectionMetadata
            {
                Id = "linkedlistdata",
                DataSourceLocation = Globals.MongoConnectionString,
                DataSourceName = "prototype",
                MapCollectionName = "linkedlistmap",
                DataStorageType = DataStorageStructureTypes.LinkedList
            };
            var account = new DataColumnMetadata
            {
                ColumnName = "Account",
                DataType = Globals.IntegerDataTypeName
            };
            var salesPerson = new DataColumnMetadata
            {
                ColumnName = "SalesPerson",
                DataType = Globals.StringDataTypeName
            };
            var product = new DataColumnMetadata
            {
                ColumnName = "Product",
                DataType = Globals.StringDataTypeName
            };
            var amount = new DataColumnMetadata
            {
                ColumnName = "Amount",
                DataType = Globals.DoubleDataTypeName
            };

            collectionMetadata.KeyColumnName = account.ColumnName;
            account.Columns = new List<DataColumnMetadata>
            {
                new DataColumnMetadata {ColumnName = "TestNested"}
            };
            collectionMetadata.Columns.Add(account);
            collectionMetadata.Columns.Add(salesPerson);
            collectionMetadata.Columns.Add(product);
            collectionMetadata.Columns.Add(amount);

            var viewDefinitionMetadata = new LinkedListViewDefinitionMetadata
            {
                ViewId = "linkedlist_account_view1",
                // MetadataCollectionId = "linkedlistdata"
            };
            viewDefinitionMetadata.Paths = new List<LinkedListPathSpecification>
            {
                new LinkedListPathSpecification {DisplayOrder = 0, Navigation = "Account"},
                new LinkedListPathSpecification {DisplayOrder = 1, Navigation = "Account.SalesPerson"},
                new LinkedListPathSpecification {DisplayOrder = 2, Navigation = "Account.SalesPerson.Product"}
            };
            viewDefinitionMetadata.Measures = new List<MeasureSpecification>
            {
                new MeasureSpecification
                {
                    AggregateOperationType = AggregateOperationTypes.Sum,
                    DisplayOrder = 0,
                    Column = amount
                }
            };
            viewDefinitionMetadata.Slicers = new List<SlicerSpecification>
            {
                new SlicerSpecification {Column = account, DisplayOrder = 0},
                new SlicerSpecification {Column = salesPerson, DisplayOrder = 1},
                new SlicerSpecification {Column = product, DisplayOrder = 2}
            };
            collectionMetadata.Views.Add(viewDefinitionMetadata);
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var collection = db.GetCollection<BsonDocument>("collectionMetadata");
            Task.WaitAll(collection.InsertOneAsync(collectionMetadata.ToBsonDocument()));
            //collectionMetadata.LinkedListSettings.KeyColumn = account;
            //collectionMetadata.KeyColumn = account;
            //UpsertLinkedListCollectionMetadata(collectionMetadata);
        }
    }
}