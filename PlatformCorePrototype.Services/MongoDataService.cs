using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Services;
using PlatformCorePrototype.Services.DataStructures;

namespace PlatformCorePrototype.Services
{


    public class MongoDataService : IDataService
    {
        public async Task<DataCollectionMetadata> GetDataCollectionMetadata(string collectionName)
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<DataCollectionMetadata>("collectionMetadata");
            var builder = new FilterDefinitionBuilder<DataCollectionMetadata>();
            var md = items.Find(Builders<DataCollectionMetadata>.Filter.Eq(x => x.Id, collectionName));
           
            var result = await md.SingleAsync();
            
            return result;
        }

        public async Task<ViewDefinitionMetadata> GetViewDefinitionMetadataAsync(string viewId)
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<ViewDefinitionMetadata>("viewDefinitionMetadata");
            var viewDefinitionMetadataTask = items.Find(x => x.Id == viewId);
            var result = await viewDefinitionMetadataTask.SingleAsync();
            return result;
        }

        public async Task<List<dynamic>> GetDataTreeStructureAsync(Task<List<dynamic>> source)
        {
            var result = source.ContinueWith<List<dynamic>>(task =>
            {
                var taskResult = new List<dynamic>();
                var sourceResult = task.Result;
                task.Result.ForEach(item =>
                {
                    if (item.Parents.Count > 0)
                    {
                        var parentId = ((List<object>)item.Parents).Last().ToString();
                        var parent = sourceResult.SingleOrDefault(x => x._id.ToString() == parentId);
                        if (parent != null)
                        {
                            if (!((IDictionary<String, object>)parent).ContainsKey("Children"))
                               parent.Children= new List<dynamic>();
                            parent.Children.Add(item);
                        }
                        else
                        {
                            taskResult.Add(item);
                        }
                    }
                    else
                    {
                        taskResult.Add(item);
                    }

                });
                return taskResult;
            });
            return await result;
        }
        public async Task<List<dynamic>> GetDataAsync(IMongoQueryDefinition queryDefinition)
        {

            var viewDefinition = await GetViewDefinitionMetadataAsync(queryDefinition.ViewId);
            var collectionMetadata = await GetDataCollectionMetadata(viewDefinition.MetadataCollectionId);
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(collectionMetadata.DataSourceName);
            var items = db.GetCollection<dynamic>(collectionMetadata.Id);
            var aggregation = items.AggregateAsync<dynamic>(queryDefinition.GetPipeline());
            var result = aggregation.Result.ToListAsync();
            if (collectionMetadata.DataStorageStructure == DataStorageStructureTypes.Tree)
                return await GetDataTreeStructureAsync(result);
            else
                return await result;

        }
    }
}
