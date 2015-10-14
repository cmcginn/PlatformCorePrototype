using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Tests.Services;

namespace PlatformCorePrototype.Tests
{
    [TestClass]
    public class TestUtils : ServiceTestBase
    {
        [TestMethod]
        public void UpsertLinkedListDataCollectionMetadataTest()
        {
            UpsertLinkedListCollectionMetadata();
        }
        [TestMethod]
        public void UpsertTreeDataCollectionMetadataTest()
        {
            UpsertTreeDataCollectionMetadata();
        }

        [TestMethod]
        public void UpsertSegmentsView1ViewDefinitionMetadataTest()
        {
            UpsertSegmentsView1ViewDefinitionMetadata();
        }
        static void UpsertCollectionMetadata(DataCollectionMetadata dataCollectionMetadata)
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<DataCollectionMetadata>("collectionMetadata");
            FilterDefinition<DataCollectionMetadata> filter = new BsonDocument("_id", dataCollectionMetadata.Id);
            var deleteTask = items.DeleteOneAsync(filter).Result;
            var upsertTask = items.InsertOneAsync(dataCollectionMetadata);
            Task.WaitAll(upsertTask);

        }
        static void UpsertLinkedListCollectionMetadata(LinkedListDataCollectionMetadata dataCollectionMetadata)
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<DataCollectionMetadata>("collectionMetadata");
            FilterDefinition<DataCollectionMetadata> filter = new BsonDocument("_id", dataCollectionMetadata.Id);
            var deleteTask = items.DeleteOneAsync(filter).Result;
            var upsertTask = items.InsertOneAsync(dataCollectionMetadata);
            Task.WaitAll(upsertTask);

        }
        static void UpsertViewDefinitionMetadata(ViewDefinitionMetadata viewDefinitionMetadata)
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<ViewDefinitionMetadata>("viewDefinitionMetadata");
            var deleteTask = items.DeleteOneAsync(x => x.Id == viewDefinitionMetadata.Id).Result;
            var upsertTask = items.InsertOneAsync(viewDefinitionMetadata);
            Task.WaitAll(upsertTask);
        }
        static void UpsertTreeDataCollectionMetadata()
        {
            var dataCollectionMetadata = new DataCollectionMetadata
            {
                    Id = "segments",
                    DataSourceLocation = Globals.MongoConnectionString,
                    DataSourceName="prototype"
            };
            var code = new DataColumnMetadata
            {
                ColumnName = "Code",
                DataType = Globals.StringDataTypeName

            };
            dataCollectionMetadata.Columns.Add(code);
            UpsertCollectionMetadata(dataCollectionMetadata);
        }

        static void UpsertSegmentsView1ViewDefinitionMetadata()
        {
            var viewDefinitionMetadata = new ViewDefinitionMetadata();
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<DataCollectionMetadata>("collectionMetadata");

            var dataCollectionMetada = items.Find(x => x.Id == "segments").SingleAsync().Result;

            var codeFilter = new FilterSpecification
            {
                Column = dataCollectionMetada.Columns.Single(x => x.ColumnName == "Code"),
                FilterType = FilterTypes.Value,
                DisplayOrder = 0

            };
            viewDefinitionMetadata.Filters.Add(codeFilter);
            viewDefinitionMetadata.Id = "segments_view1";
            viewDefinitionMetadata.MetadataCollectionId = dataCollectionMetada.Id;
            UpsertViewDefinitionMetadata(viewDefinitionMetadata);

            //var codeFilter = new 
        }

        static void UpsertLinkedListCollectionMetadata()
        {
            var linkedListCollectionMetadata = new LinkedListDataCollectionMetadata
            {
                Id = "linkedlistmap",
                DataSourceLocation = Globals.MongoConnectionString,
                DataSourceName = "prototype",
                
            };

            var sourceCollectionMetadata = new DataCollectionMetadata
            {
                Id = "linkedlistdata",
                DataSourceLocation = Globals.MongoConnectionString,
                DataSourceName = "prototype"
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
            sourceCollectionMetadata.Columns.Add(account);
            sourceCollectionMetadata.Columns.Add(salesPerson);
            sourceCollectionMetadata.Columns.Add(product);
            sourceCollectionMetadata.Columns.Add(amount);



            var mapCollectionMetadata = new LinkedListDataCollectionMetadata
            {
                Id = "linkedlistmap",
                DataSourceLocation = Globals.MongoConnectionString,
                DataSourceName = "prototype"
            };
            var navigationColumn = new DataColumnMetadata
            {
                ColumnName = "Navigation",
                DataType = Globals.CollectionDataTypeName
            };

            var navigationColumnPathMember = new DataColumnMetadata
            {
                ColumnName = "Path",
                DataType = Globals.StringDataTypeName
            };
            navigationColumn.Columns.Add(navigationColumnPathMember);
            var valueColumn = new DataColumnMetadata
            {
                ColumnName = "Account",
                DataType = Globals.IntegerDataTypeName
            };
            mapCollectionMetadata.Columns.Add(navigationColumn);
            mapCollectionMetadata.Columns.Add(valueColumn);


            linkedListCollectionMetadata.NavigationColumnName = navigationColumn.ColumnName;
            linkedListCollectionMetadata.ValueColumnName = valueColumn.ColumnName;
            linkedListCollectionMetadata.SourceCollectionMetadata = sourceCollectionMetadata;
            linkedListCollectionMetadata.MapCollectionMetadata = mapCollectionMetadata;

            UpsertLinkedListCollectionMetadata(linkedListCollectionMetadata);

        }
        
        
    }
}
