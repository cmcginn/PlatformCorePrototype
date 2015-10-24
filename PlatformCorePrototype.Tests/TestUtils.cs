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