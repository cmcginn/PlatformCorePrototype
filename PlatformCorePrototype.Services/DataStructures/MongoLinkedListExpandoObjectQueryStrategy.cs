using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;
using PlatformCorePrototype.Services.Helpers;

namespace PlatformCorePrototype.Services.DataStructures
{
    public class MongoLinkedListExpandoObjectQueryStrategy : IQueryStrategy<ExpandoObject>
    {
        #region public interface

        public IQueryBuilder QueryBuilder { get; set; }

        public async Task<List<ExpandoObject>> RunQuery()
        {
            
            if (LinkedListQueryBuilder.SelectedNavigation != null)
            {
                
                var navPath = String.Join(".",
                    LinkedListQueryBuilder.SelectedNavigationPath.Split('.').Take(LinkedListQueryBuilder.SelectedLevel +1));
                var keys = new List<object>();
                var keyValues = new BsonArray();
                LinkedListQueryBuilder.LinkedListMaps.ForEach(x =>
                {
                    x.NavigationMaps.ForEach(n =>
                    {
                        BsonValue val = null;
                        var p = String.Join(".",n.Navigation.Split('.').Take(LinkedListQueryBuilder.SelectedLevel+1));

                        if (navPath == p)
                        {
                            if (n.Key is Int64)
                                val = (BsonValue) new BsonInt32(int.Parse(n.Key.ToString()));
                            else
                                val = (BsonValue) n.Key;
                           
                                keyValues.Add(val);
                        }

                                
                        
                     
                    });
                });
                var builder = new FilterDefinitionBuilder<ExpandoObject>();
                FilterDefinition<ExpandoObject> filter =
                    builder.In(LinkedListQueryBuilder.SelectedNavigation.SlicerColumnName, keyValues);
                var db = GetDatabase();
                var collection = db.GetCollection<ExpandoObject>(CollectionMetadata.Id);
                var groupDocument = new BsonDocument {{"id", 0}, {"$sum", "$Amount"}};
                var aggregated = collection.Aggregate().Match(filter).Group<ExpandoObject>(new BsonDocument { { "_id", 0 }, { "f0", new BsonDocument { { "$sum", "$Amount" } } } });
                return await aggregated.ToListAsync();

            }
         
            //{
                //var q = from llm in LinkedListQueryBuilder.LinkedListMaps
                //        join np in LinkedListQueryBuilder.LinkedListMaps.Select(m=>m.NavigationMaps)
                //        on llm
                //LinkedListQueryBuilder.LinkedListMaps.Where(x=>x.)
                //var paths = LinkedListQueryBuilder.SelectedPath.Split('.').ToList();
                //var pathText = 
            //}
            //throw new System.NotImplementedException();
            //var pipeline = await GetQueryPipeline();
            //var db = GetDatabase();
            //var collection = db.GetCollection<ExpandoObject>(CollectionMetadata.Id);
            //var ag = collection.Aggregate();
            //pipeline.ForEach(pl => { ag = ag.AppendStage<ExpandoObject>(pl); });
            //return await ag.ToListAsync();
            throw new System.NotImplementedException();
        }

        #endregion

        #region utilities

        private LinkedListDataCollectionMetadata _CollectionMetadata;
        private LinkedListDataCollectionMetadata _LinkedListDataCollectionMetadata;
        private ILinkedListQueryBuilder _LinkedListQueryBuilder;

        protected ILinkedListQueryBuilder LinkedListQueryBuilder
        {
            get
            {
                if (_LinkedListQueryBuilder == null)
                {
                    _LinkedListQueryBuilder = QueryBuilder as ILinkedListQueryBuilder;
                    if (_LinkedListQueryBuilder == null)
                        throw new Exception(
                            "MongoLinkedListQueryStrategy requires member IQueryBuilder to be of type ILinkedListQueryBuilder");
                }
                return _LinkedListQueryBuilder;
            }
            set { _LinkedListQueryBuilder = value; }
        }

        protected LinkedListDataCollectionMetadata CollectionMetadata
        {
            get { return _CollectionMetadata ?? (_CollectionMetadata = GetCollectionMetadata()); }
            set { _CollectionMetadata = value; }
        }

        //protected async Task<Dictionary<string, List<BsonValue>>> GetLinkedListMap()
        //{
        //    var doc = await GetLinkedListMapDocument();
        //    var navigations = doc.GetElement("NavigationMaps").Value.AsBsonArray;
        //    var result = navigations.Select(
        //        x =>
        //            new
        //            {
        //                Key = x["Key"],
        //                Path = x["Navigation"].ToString().Split('.').Take(LinkedListQueryBuilder.SelectedLevel)
        //            })
        //        .GroupBy(x => x.Key)
        //        .Select(x => new {Navigation = String.Join(".",x.First().Path), Values = x.Select(m => m.Key).ToList()})
        //        .ToDictionary(x => x.Navigation, x => x.Values);
        //    return result;

        //}
        protected async Task<List<BsonDocument>> GetLinkedListMap()
        {
            throw new System.NotImplementedException();
            //var doc = await GetLinkedListMapDocument();
            //var navigations = doc.GetElement("NavigationMaps").Value.AsBsonArray;
            //var result = navigations.Select(
            //    x =>
            //        new
            //        {
            //            Key = x["Key"],
            //            Path = x["Navigation"].ToString().Split('.').Take(LinkedListQueryBuilder.SelectedLevel)
            //        })
            //    .GroupBy(x => x.Key)
            //    .Select(x=>new BsonDocument{{"Navigation"}})
            //    //.Select(x => new { Navigation = String.Join(".", x.First().Path), Values = x.Select(m => m.Key).ToList() })

            //return result;

        }
        protected async Task<BsonDocument> GetLinkedListMapDocument()
        {
            throw new System.NotImplementedException();
            //var db = GetDatabase();
            //var collection = db.GetCollection<BsonDocument>(CollectionMetadata.MapCollectionName);
            //var result =
            //    collection.Find(new BsonDocument
            //    {
            //        {"SlicerColumnName", LinkedListQueryBuilder.SelectedLinkedListSlicer.Column.ColumnName}
            //    })
            //        .SingleAsync();
            //return await result;
        }

     

   

       

        protected LinkedListDataCollectionMetadata GetCollectionMetadata()
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);

            var collection = db.GetCollection<BsonDocument>("collectionMetadata");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var fd = builder.ElemMatch<BsonDocument>("Views", new BsonDocument { { "ViewId", QueryBuilder.ViewId } });
            var queryResult = collection.Find(fd).SingleOrDefaultAsync();
            if (queryResult == null)
                throw new Exception(
                    String.Format("DataCollectionMetadata containing viewId {0} could not be found", QueryBuilder.ViewId));

            var result = Mapper.Map<BsonDocument, LinkedListDataCollectionMetadata>(queryResult.Result);
            return result;
        }

        protected IMongoDatabase GetDatabase()
        {
            //todo throw query strategy exception if collection metadata is null
            var client = new MongoClient(CollectionMetadata.DataSourceLocation);
            var db = client.GetDatabase(CollectionMetadata.DataSourceName);
            return db;
        }

        #endregion
    }
}