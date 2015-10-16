﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;
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
            var items = db.GetCollection<BsonDocument>("viewDefinitionMetadata");
            var viewDefinitionMetadataTask = items.FindAsync(x => x["_id"] == viewId);

            var t1 = viewDefinitionMetadataTask.ContinueWith<Task<DataCollectionMetadata>>((t) =>
            {
                var collectionMetadataId = t.Result.ToListAsync().Result.Single()["MetadataCollectionId"].ToString();
                var asyncResult = GetDataCollectionMetadata(collectionMetadataId);
                return asyncResult;
            });
            var t2 = t1.ContinueWith<Task<ViewDefinitionMetadata>>((t) =>
            {
                var dataCollectionMetadata = t.Result.Result;
                if (dataCollectionMetadata.LinkedListSettings != null)
                    return GetViewDefinitionMetadataAsync<LinkedListViewDefinitionMetadata>(viewId);
                else
                    return GetViewDefinitionMetadataAsync<ViewDefinitionMetadata>(viewId);


            });

            var result = await t2.Result;
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
                                parent.Children = new List<dynamic>();
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

        async Task<ViewDefinitionMetadata> GetViewDefinitionMetadataAsync<T>(string viewId) where T : ViewDefinitionMetadata
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<T>("viewDefinitionMetadata");
            var viewDefinitionMetadataTask = items.Find(x => x.Id == viewId);
            var result = await viewDefinitionMetadataTask.SingleAsync();
            return result;
        }

        public async Task<List<dynamic>> GetDataAsync(IQueryBuilder queryBuilder)
        {
            Task<List<dynamic>> result = null;
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            DataCollectionMetadata dataCollectionMetadata = null;
            //first need view definition
            if (queryBuilder.GetType() == typeof (LinkedListQueryBuilder))
            {

                var linkedListQueryBuilder = queryBuilder as LinkedListQueryBuilder;
                var viewDefinitionMetadataTask = GetViewDefinitionMetadataAsync<LinkedListViewDefinitionMetadata>(queryBuilder.ViewId);
                LinkedListViewDefinitionMetadata viewDefinitionMetadata = null;

                var t1 = viewDefinitionMetadataTask.ContinueWith<Task<List<dynamic>>>((t) =>
                {
                    Task<List<dynamic>> asyncResult = null;
                    viewDefinitionMetadata = t.Result as LinkedListViewDefinitionMetadata;
                    dataCollectionMetadata =
                        GetDataCollectionMetadata(viewDefinitionMetadata.MetadataCollectionId).Result;
                    if (dataCollectionMetadata.LinkedListSettings.KeyColumn.DataType == Globals.IntegerDataTypeName)
                    {
                        var strategy = new MongoLinkedListQueryStrategy<dynamic, int>();
                        strategy.Filters = queryBuilder.SelectedFilters;
                        strategy.CollectionName = dataCollectionMetadata.Id;
                        strategy.DataSourceLocation = dataCollectionMetadata.DataSourceLocation;
                        strategy.DataSourceName = dataCollectionMetadata.DataSourceName;
                        strategy.LinkedListSettings = dataCollectionMetadata.LinkedListSettings;
                        //strategy.Path = linkedListQueryBuilder.SelectedPaths.Select(x=>x.Name).ToList();
                        strategy.LinkedListMap = new LinkedListMap<int>();
                        if (!String.IsNullOrEmpty(linkedListQueryBuilder.SelectedKey))
                            strategy.LinkedListMap.Key = int.Parse(linkedListQueryBuilder.SelectedKey);
                        strategy.LinkedListMap.Navigation =
                            linkedListQueryBuilder.SelectedPaths.Select(x => x.Name).ToList();
                        asyncResult = strategy.RunQuery();
                    }
                    return asyncResult;
                });
                result = await t1;


            }
            return await result;

        }
        //public async Task<List<dynamic>> GetDataAsync(IMongoQueryDefinition queryDefinition)
        //{

        //    var viewDefinition = await GetViewDefinitionMetadataAsync(queryDefinition.ViewId);
        //    var collectionMetadata = await GetDataCollectionMetadata(viewDefinition.MetadataCollectionId);
        //    var client = new MongoClient(Globals.MongoConnectionString);
        //    var db = client.GetDatabase(collectionMetadata.DataSourceName);
        //    var items = db.GetCollection<dynamic>(collectionMetadata.Id);
        //    var aggregation = items.AggregateAsync<dynamic>(queryDefinition.GetPipeline());
        //    var result = aggregation.Result.ToListAsync();
        //    if (collectionMetadata.DataStorageStructure == DataStorageStructureTypes.Tree)
        //        return await GetDataTreeStructureAsync(result);

        //        return await result;

        //}
    }
}
