using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;
using PlatformCorePrototype.Core.Services;
using PlatformCorePrototype.Services.DataStructures;

namespace PlatformCorePrototype.Services
{


    public class MongoDataService 
    {
        public async Task<IDataCollectionMetadata> GetDataCollectionMetadata(string collectionName)
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var items = db.GetCollection<BsonDocument>("collectionMetadata");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var md = items.Find(Builders<BsonDocument>.Filter.Eq(x => x["_id"], collectionName));
            var result = md.SingleAsync().ContinueWith<IDataCollectionMetadata>((t) =>
            {
                return Mapper.Map<BsonDocument, IDataCollectionMetadata>(t.Result);
            });
            return await result;
        }

        //public async Task<ViewDefinitionMetadata> GetViewDefinitionMetadataAsync(string viewId)
        //{

        //    var client = new MongoClient(Globals.MongoConnectionString);
        //    var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
        //    var items = db.GetCollection<BsonDocument>("viewDefinitionMetadata");
        //    var viewDefinitionMetadataTask = items.FindAsync(x => x["_id"] == viewId);

        //    var t1 = viewDefinitionMetadataTask.ContinueWith<Task<DataCollectionMetadata>>((t) =>
        //    {
        //        var collectionMetadataId = t.Result.ToListAsync().Result.Single()["MetadataCollectionId"].ToString();
        //        var asyncResult = GetDataCollectionMetadata(collectionMetadataId);
        //        return asyncResult;
        //    });
        //    var t2 = t1.ContinueWith<Task<ViewDefinitionMetadata>>((t) =>
        //    {
        //        var dataCollectionMetadata = t.Result.Result;
        //        //if (dataCollectionMetadata.LinkedListSettings != null)
        //        //    return GetViewDefinitionMetadataAsync<LinkedListViewDefinitionMetadata>(viewId);
        //       // else
        //            return GetViewDefinitionMetadataAsync<ViewDefinitionMetadata>(viewId);


        //    });

        //    var result = await t2.Result;
        //    return result;
        //}
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
            var viewDefinitionMetadataTask = items.Find(x => x.ViewId == viewId);
            var result = await viewDefinitionMetadataTask.SingleAsync();
            return result;
        }

        
        //public async Task<List<dynamic>> GetDataAsync(IQueryBuilder queryBuilder)
        //{
        //    Task<List<dynamic>> result = null;
        //    var client = new MongoClient(Globals.MongoConnectionString);
        //    var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
        //    DataCollectionMetadata dataCollectionMetadata = null;
        //    //first need view definition
        //    if (queryBuilder.GetType() == typeof (LinkedListQueryBuilder))
        //    {

        //        var linkedListQueryBuilder = queryBuilder as LinkedListQueryBuilder;
        //        var viewDefinitionMetadataTask = GetViewDefinitionMetadataAsync<LinkedListViewDefinitionMetadata>(queryBuilder.ViewId);
        //        LinkedListViewDefinitionMetadata viewDefinitionMetadata = null;

        //        var t1 = viewDefinitionMetadataTask.ContinueWith<Task<List<dynamic>>>((t) =>
        //        {
        //            Task<List<dynamic>> asyncResult = null;
        //            viewDefinitionMetadata = t.Result as LinkedListViewDefinitionMetadata;
        //            dataCollectionMetadata =
        //                GetDataCollectionMetadata(viewDefinitionMetadata.MetadataCollectionId).Result;
        //            //if (dataCollectionMetadata.LinkedListSettings.KeyColumn.DataType == Globals.IntegerDataTypeName)
        //            //{
        //            //    var strategy = new MongoLinkedListQueryStrategy<dynamic, int>();
        //            //    strategy.Filters = queryBuilder.SelectedFilters;
        //            //    strategy.CollectionName = dataCollectionMetadata.Id;
        //            //    strategy.DataSourceLocation = dataCollectionMetadata.DataSourceLocation;
        //            //    strategy.DataSourceName = dataCollectionMetadata.DataSourceName;
        //            //    strategy.LinkedListSettings = dataCollectionMetadata.LinkedListSettings;
        //            //    //strategy.Path = linkedListQueryBuilder.SelectedPaths.Select(x=>x.Navigation).ToList();
        //            //    strategy.LinkedListMap = new LinkedListMap<int>();
        //            //    if (!String.IsNullOrEmpty(linkedListQueryBuilder.SelectedKey))
        //            //        strategy.LinkedListMap.Key = int.Parse(linkedListQueryBuilder.SelectedKey);
        //            //    strategy.LinkedListMap.Navigation =
        //            //        linkedListQueryBuilder.SelectedPaths.Select(x => x.Navigation).ToList();
        //            //    asyncResult = strategy.RunQuery();
        //            //}
        //            return asyncResult;
        //        });
        //        result = await t1;


        //    }
        //    return await result;

        //}

        
#region refactored
        //public async Task<LinkedListDataCollectionMetadata> GetLinkedListDataCollectionMetadata(string collectionName)
        //{
        //    var client = new MongoClient(Globals.MongoConnectionString);
        //    var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
        //    var items = db.GetCollection<LinkedListDataCollectionMetadata>("collectionMetadata");
        //    var builder = new FilterDefinitionBuilder<LinkedListDataCollectionMetadata>();
        //    var md = items.Find(Builders<LinkedListDataCollectionMetadata>.Filter.Eq(x => x.Id, collectionName));

        //    var result = await md.SingleAsync();

        //    return result;
        //}
        public async Task<List<dynamic>> GetDataAsync(IQueryStrategy<dynamic> strategy)
        {
            throw new System.NotImplementedException();
            //var storageType = await GetDataStorageStructureTypeForView(strategy.ViewId);
            //if (storageType == DataStorageStructureTypes.LinkedList)
            // {
            //    var t1 = GetLinkedListViewDefinitionMetadata(strategy.ViewId);
            //    var t2 = t1.ContinueWith<Task<LinkedListDataCollectionMetadata>>((t) =>
            //    {
            //        strategy.ViewDefinitionMetadata = t.Result;
            //        return GetLinkedListDataCollectionMetadata("VIEWID");
            //    });
            //var t3 = t2.ContinueWith<Task<LinkedListDataCollectionMetadata>>((t) =>
            //{
            //    return t.Result;
            //});
            //var t4 = t3.ContinueWith<Task<List<dynamic>>>((t) =>
            //{
            //    strategy.CollectionMetadata = t.Result.Result;
            //    return strategy.RunQuery();
            //});
            //    //var t3 = t2.ContinueWith<Task<List<dynamic>>>((t) =>
            //    //{
            //    //    strategy.CollectionMetadata = t.Result.Result;

            //    //    return strategy.RunQuery();
            //    //});
            //var tasks = new List<Task> {t3, t4};
            //Task.WaitAll(tasks.ToArray());
            //return await t4.Result;
            //}

            //return null;
        }

        //public async Task<ViewDefinition> GetViewDefinitionAsync(string viewId)
        //{
        //    var t1 = GetViewDefinitionMetadataAsync(viewId);
        //    var t2 = t1.ContinueWith<ViewDefinition>((t) =>
        //    {
        //        var asyncResult =  Mapper.Map<ViewDefinition>(t.Result);
        //        asyncResult.QueryBuilder.ViewId = viewId;
        //        return asyncResult;
        //    });
        //    var result = await t2;
        //    return result;
        //}
        public async Task<ViewDefinitionMetadata> GetViewDefinitionMetadataAsync(string viewId)
        {
     
            var storageType = await GetDataStorageStructureTypeForView(viewId);
            if (storageType == DataStorageStructureTypes.LinkedList)
                return await GetLinkedListViewDefinitionMetadata(viewId);
            else
                return await GetViewDefinitionMetadata(viewId);

        }
        public async Task<LinkedListViewDefinitionMetadata> GetLinkedListViewDefinitionMetadata(string viewId)
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var collection = db.GetCollection<LinkedListViewDefinitionMetadata>("viewDefinitionMetadata");
            var result =collection.Find(x => x.ViewId == viewId).SingleOrDefaultAsync();
            Task.WaitAll(result);
            return await result;
        }
        public async Task<ViewDefinitionMetadata> GetViewDefinitionMetadata(string viewId)
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var collection = db.GetCollection<ViewDefinitionMetadata>("viewDefinitionMetadata");
            var result = await collection.Find(x => x.ViewId == viewId).SingleOrDefaultAsync();
            return result;
        }
        public async Task<DataStorageStructureTypes> GetDataStorageStructureTypeForView(string viewId)
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);
            var views = db.GetCollection<BsonDocument>("viewDefinitionMetadata");
            var dataCollections = db.GetCollection<BsonDocument>("collectionMetadata");
            var t1 = views.Find(x => x["_id"] == viewId).Project(x => x["MetadataCollectionId"]).SingleOrDefaultAsync();
            var t2 = t1.ContinueWith<Task<BsonValue>>((t) =>
            {
                return dataCollections.Find(x => x["_id"] == t.Result).Project(x => x["DataStorageType"]).SingleOrDefaultAsync();
            });
            var t3 = t2.ContinueWith<DataStorageStructureTypes>((t) =>
            {
                var s = t.Result.Result.ToString();
                var typeId = (DataStorageStructureTypes) (int.Parse(s));
                return typeId;
            });
            var result = await t3;
            return result;
        }

#endregion

       // public async 
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
