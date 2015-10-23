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
            var pipeline = await GetQueryPipeline();
            var db = GetDatabase();
            var collection = db.GetCollection<ExpandoObject>(CollectionMetadata.Id);
            var ag = collection.Aggregate();
            pipeline.ForEach(pl => { ag = ag.AppendStage<ExpandoObject>(pl); });
            return await ag.ToListAsync();
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

        protected virtual FilterDefinition<ExpandoObject> GetNavigationFilterDefinition()
        {
            FilterDefinition<ExpandoObject> result = null;
            if (LinkedListQueryBuilder.SelectedPath != null)
            {
                if (!String.IsNullOrWhiteSpace(LinkedListQueryBuilder.SelectedPath.Navigation))
                {
                    var builder = new FilterDefinitionBuilder<ExpandoObject>();
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

        protected virtual async Task<FilterDefinition<ExpandoObject>> GetLinkedListMapsFilterDefinition()
        {
            var db = GetDatabase();
            var collection = db.GetCollection<BsonDocument>(CollectionMetadata.MapCollectionName);
            var navigationFilter = GetNavigationFilterDefinition();
            var findDocument = navigationFilter != null ? navigationFilter.Serialize() : new BsonDocument();
            var items = collection.FindAsync<BsonDocument>(findDocument).Result.ToListAsync();
            var result = items.ContinueWith(t =>
            {
                FilterDefinition<ExpandoObject> asyncResult = null;
                if (t.Result.Any())
                {
                    var keyValues = new BsonArray();
                    var builder = new FilterDefinitionBuilder<ExpandoObject>();
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

                    el = new BsonElement(String.Format("slicer_{0}", index),
                        new BsonString(String.Format("${0}",
                            LinkedListQueryBuilder.SelectedSlicers[index].Column.ColumnName)));
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
                    result.Add(new BsonElement(String.Format("measure_{0}", index), factDoc));
                }
            }
            return result;
        }

        protected BsonDocument GetQueryGroupDocument()
        {
            BsonDocument result = null;
            var idElements = GetQueryGroupIdElements();
            if (idElements != null)
            {
                result = new BsonDocument { { "_id", new BsonDocument(idElements) } };


                var measureElements = GetQueryMeasureElements();

                if (measureElements != null)
                {
                    result.AddRange(measureElements);
                }
            }

            return result;
        }

        protected virtual async Task<List<BsonDocument>> GetQueryPipeline()
        {
            FilterDefinition<ExpandoObject> linkedListFilters = null;
            if (LinkedListQueryBuilder.SelectedPath != null)
            {
                if (!String.IsNullOrWhiteSpace(LinkedListQueryBuilder.SelectedPath.Navigation))
                    linkedListFilters = await GetLinkedListMapsFilterDefinition();
            }

            var result = new Task<List<BsonDocument>>(() =>
            {
                var asyncResult = new List<BsonDocument>();
                if (linkedListFilters != null)
                    asyncResult.Add(new BsonDocument { { "$match", linkedListFilters.Serialize() } });

                var groupDocument = GetQueryGroupDocument();

                if (groupDocument != null)
                    asyncResult.Add(new BsonDocument { { "$group", groupDocument } });
                return asyncResult;
            });
            result.Start();
            return await result;
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