using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;
using PlatformCorePrototype.Services.Helpers;

namespace PlatformCorePrototype.Services.DataStructures
{
    public class MongoLinkedListQueryStrategy<T>
    {

        #region public interface

        

        public IQueryBuilder QueryBuilder { get; set; }
        public async Task<List<T>> RunQuery()
        {
            //throw new System.NotImplementedException();
            var pipeline = await GetQueryPipeline();
            var db = GetDatabase();
            var collection = db.GetCollection<T>(CollectionMetadata.Id);
            var ag = collection.Aggregate();
            
          
            //ProjectionDefinition < T > = new ProjectionDefinition<T>();
            //ag.Group<T>((x) => { return pipeline.First(); });
            //Command<T> cmd = pipeline;
            // var pp = collection.AggregateAsync<T>()

            //pl.Stages.Add(pipeline.First())
            //pl.Group(x=>x.State,g => new { State = g.Key, TotalPopulation = g.Sum(x => x.Population) })
            //PipelineDefinition p = new BsonDocument[];
            //pl.AppendStage(pipeline.First())
            //pl.Group(pipeline.First());
            //PipelineDefinition<BsonDocument,T> pd = pipeline;
            //var result = collection.AggregateAsync<T>()
        }
        #endregion

        #region utilities

        private ILinkedListQueryBuilder _LinkedListQueryBuilder;
        protected ILinkedListQueryBuilder LinkedListQueryBuilder
        {
            get
            {
                if (_LinkedListQueryBuilder == null)
                {
                    _LinkedListQueryBuilder = QueryBuilder as ILinkedListQueryBuilder;
                    if (_LinkedListQueryBuilder == null)
                        throw new System.Exception(
                            "MongoLinkedListQueryStrategy requires member IQueryBuilder to be of type ILinkedListQueryBuilder");
                    
                }
                return _LinkedListQueryBuilder;
            }
            set { _LinkedListQueryBuilder = value; }
        }
        protected virtual FilterDefinition<T> GetNavigationFilterDefinition()
        {
      
            FilterDefinition<T> result = null;
            if (LinkedListQueryBuilder.SelectedPath != null)
            {
                if (!String.IsNullOrWhiteSpace(LinkedListQueryBuilder.SelectedPath.Navigation))
                {


                    var builder = new FilterDefinitionBuilder<T>();
                    if (LinkedListQueryBuilder.ExcludeChildren)
                    {

                        result = builder.Eq("Navigation", LinkedListQueryBuilder.SelectedPath.Navigation);
                    }
                    else
                    {
                        var expr =
                            new BsonRegularExpression(String.Format("^{0}",
                                LinkedListQueryBuilder.SelectedPath.Navigation));
                        result = builder.Regex("Navigation", expr);
                    }
                }


            }
            return result;
        }

        protected virtual async Task<FilterDefinition<T>> GetLinkedListMapsFilterDefinition()
        {
            var db = GetDatabase();
            var collection = db.GetCollection<BsonDocument>(CollectionMetadata.MapCollectionName);
            var navigationFilter = GetNavigationFilterDefinition();
            var findDocument = navigationFilter != null ? navigationFilter.Serialize() : new BsonDocument();
            var items = collection.FindAsync<BsonDocument>(findDocument).Result.ToListAsync();
            var result = items.ContinueWith<FilterDefinition<T>>((t) =>
            {
                FilterDefinition<T> asyncResult = null;
                if (t.Result.Any())
                {

                    var keyValues = new BsonArray();
                    var builder = new FilterDefinitionBuilder<T>();
                    t.Result.ForEach(doc => keyValues.Add(doc.GetElement("Key").Value));
                    asyncResult = builder.In(CollectionMetadata.KeyColumnName, keyValues);

                }
                return asyncResult;
            });
            return await result;
        }

        protected List<BsonElement> GetQueryGroupIdElements()
        {
            List<BsonElement> result = null;
            if (LinkedListQueryBuilder.SelectedSlicers.Any())
            {
                result = new List<BsonElement>();
       
                for (var index = 0; index < LinkedListQueryBuilder.SelectedSlicers.Count; index++)
                {
                    BsonElement el;

                    el = new BsonElement(String.Format("slicer_{0}", index), new BsonString(String.Format("${0}", LinkedListQueryBuilder.SelectedSlicers[index].Column.ColumnName)));
                    result.Add(el);
                    //TODO Handle Dates?

                }
            }
            return result;
        }
        protected List<BsonElement> GetQueryMeasureElements()
        {
            List<BsonElement> result = null;
            if (LinkedListQueryBuilder.SelectedMeasures.Any())
            {
                result = new List<BsonElement>();
                var elements = new List<BsonElement>();
                for (var index = 0; index < LinkedListQueryBuilder.SelectedMeasures.Count; index++)
                {
                    var measure = LinkedListQueryBuilder.SelectedMeasures[index];
                    var op = "";
                    BsonValue elementValue = null;
                    switch (measure.AggregateOperationType)
                    {
                        case AggregateOperationTypes.Average:
                            op = "$avg";
                            elementValue = new BsonString(String.Format("${0}", measure.Column.ColumnName));
                            break;

                        case AggregateOperationTypes.Sum:
                            op = "$sum";
                            elementValue = new BsonString(String.Format("${0}", measure.Column.ColumnName));
                            break;
                        case AggregateOperationTypes.Count:
                            op = "$sum";
                            elementValue = new BsonInt32(1);
                            break;
                    }
                    var el = new BsonElement(op, elementValue);
                    var factDoc = new BsonDocument();
                    factDoc.Add(el);
                    var factElementDoc = new BsonDocument();
                    result.Add(new BsonElement(String.Format("measure_{0}", index), factDoc));
                
                }
                
            }
            return result;
        }
        protected BsonDocument GetQueryGroupDocument()
        {
            BsonDocument result = null;
            var idElements = GetQueryGroupIdElements();
            if (idElements!=null)
            {
                result = new BsonDocument {{"_id", new BsonDocument(idElements)}};
            
                
                var measureElements = GetQueryMeasureElements();
                
                if (measureElements!=null)
                {
                    result.AddRange(measureElements);

                }

            }

            return result;
        }
        
        protected virtual async Task<List<BsonDocument>> GetQueryPipeline()
        {
            
            FilterDefinition<T> linkedListFilters = null;
            if (LinkedListQueryBuilder.SelectedPath != null)
            {
                if(!String.IsNullOrWhiteSpace(LinkedListQueryBuilder.SelectedPath.Navigation))
                    linkedListFilters = await GetLinkedListMapsFilterDefinition();
            }

            var result = new Task<List<BsonDocument>>(() =>
            {
                var asyncResult = new List<BsonDocument>();
                if (linkedListFilters != null)
                    asyncResult.Add(new BsonDocument {{"$match", linkedListFilters.Serialize()}});

                var groupDocument = GetQueryGroupDocument();

                if (groupDocument != null)
                    asyncResult.Add(new BsonDocument{{"$group",groupDocument}});
                return asyncResult;
            });
            result.Start();
            return await result;
        }

        private LinkedListDataCollectionMetadata _LinkedListDataCollectionMetadata;
        

        private LinkedListDataCollectionMetadata _CollectionMetadata;
        protected LinkedListDataCollectionMetadata CollectionMetadata
        {
            get { return _CollectionMetadata ?? (_CollectionMetadata = GetCollectionMetadata()); }
            set { _CollectionMetadata = value; }
        }

       

        protected LinkedListDataCollectionMetadata GetCollectionMetadata()
        {
            var client = new MongoClient(Globals.MongoConnectionString);
            var db = client.GetDatabase(Globals.MetadataCollectionStoreName);

            var collection = db.GetCollection<BsonDocument>("collectionMetadata");
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var fd = builder.ElemMatch<BsonDocument>("Views", new BsonDocument { { "ViewId", QueryBuilder.ViewId} });
            var queryResult = collection.Find(fd).SingleOrDefaultAsync();
            if (queryResult == null)
                throw new System.Exception(
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
