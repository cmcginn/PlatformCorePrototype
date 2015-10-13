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
                    DataSourceName="prototype",
                    DataStorageStructure= DataStorageStructureTypes.Tree
            };
            var code = new DataColumnMetadata
            {
                ColumnName = "Code",
                DataType = Globals.StringDatatypeName

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
        
    }
}
