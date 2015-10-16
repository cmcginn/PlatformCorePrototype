using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Services.DataStructures
{
    public class MongoLinkedListQueryStrategy<T,V>:IMongoLinkedListQueryStrategy<T,V>
    {
        protected FilterDefinition<T> GetMapFilterDefinition()
        {
            var builder = new FilterDefinitionBuilder<T>();
            FilterDefinition<T> result = null;
            if (LinkedListMap != null)
            {
                result = new BsonDocument();
                if (LinkedListMap.Navigation.Any())
                {
                    var val = new BsonArray();
                    LinkedListMap.Navigation.ForEach(x =>
                    {
                        val.Add(x);
                    });
                    result &= builder.Eq("Navigation", val);

                }
                V d = default(V);
                if (!LinkedListMap.Key.Equals(d))
                {
                    result &= builder.Eq("Key", LinkedListMap.Key);
                }
            }
            

            return result;
        }

        protected List<BsonDocument> GetMapFilterPipeline()
        {
            var result = new List<BsonDocument>(); 
            var matchDefinition = GetMapFilterDefinition();
            if (matchDefinition != null)
            {
                result.Add(new BsonDocument { { "$match", ToDocument(matchDefinition) } });
            }
            result.Add(new BsonDocument {{"$project", BsonDocument.Parse("{ \"$project\" : { \"_id\":0,\"Key\" : \"$Key\" } }]")}});
            return result;
        }


        IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(DataSourceLocation);
            var result = client.GetDatabase(DataSourceName);
            return result;
        }

        protected async Task<List<LinkedListMap<V>>> GetLinkListMaps()
        {
            var pipeline = new List<BsonDocument> {GetMapFilterPipeline().First()};
            var db = GetDatabase();
            var collection = db.GetCollection<BsonDocument>(LinkedListSettings.MapCollectionName);

            var asyncResult = collection.AggregateAsync<BsonDocument>(pipeline);
            var result = asyncResult.ContinueWith<List<LinkedListMap<V>>>((t) =>
            {
                var asyncTaskResult = new List<LinkedListMap<V>>();
                var f = t.Result.ForEachAsync(doc =>
                {
                    var item = new LinkedListMap<V>();
                    BsonValue bsonValue = null;
                    if (doc.TryGetValue("Key", out bsonValue))
                    {
                        BsonValue bsonArray = null;
                        if (doc.TryGetValue("Navigation", out bsonArray))
                        {
                            bsonArray.AsBsonArray.ToList().ForEach(s =>
                            {
                                item.Navigation.Add(s.ToString());
                            });
                            if (typeof (V) == typeof (int))
                                item.Key = (V) (object) int.Parse(bsonValue.ToString());
                            else if(typeof(V)==typeof(double))
                                item.Key = (V)(object)double.Parse(bsonValue.ToString());
                            else if (typeof (V) == typeof (Guid))
                                item.Key = (V)(object) Guid.Parse(bsonValue.ToString());
                            else
                                item.Key = (V)(object)bsonValue.ToString();
                            asyncTaskResult.Add(item);
                        }
                        

                    }
                    
                });
                return asyncTaskResult;
            });
            return await result;
        }
        //TODO: Base Class
        protected BsonDocument ToDocument(FilterDefinition<T> source)
        {
            var serializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return source.Render(serializer, BsonSerializer.SerializerRegistry);
        }
        
        //protected FilterDefinition<T> GetMapFilterDefinition()
        //{
        //    var builder = new FilterDefinitionBuilder<T>();
        //    FilterDefinition<T> result = null;
        //    var activeFilters = Filters.Where(x => x.FilterValues.Any(y => y.Active)).ToList();
        //    if (activeFilters.Any())
        //    {

        //        result = new BsonDocument();
        //        activeFilters.ForEach(activeFilter =>
        //        {

        //            var valuesList = new List<BsonValue>();
        //            activeFilter.FilterValues.Where(x => x.Active).ToList().ForEach(activeFilterValue =>
        //            {
        //                BsonValue elementValue;
        //                switch (activeFilter.Column.DataType)
        //                {
        //                    case Globals.StringDataTypeName:
        //                        valuesList.Add(new BsonString(activeFilterValue.Value));
        //                        break;
        //                }

        //            });

        //            result &= builder.In(activeFilter.Column.ColumnName, valuesList);

        //        });
        //    }
        //    if (LinkedListMap != null)
        //    {
        //        var navigationBuilder = new FilterDefinitionBuilder<List<string>>();
        //        FilterDefinition<T> linkedListFilterDefinition = null;
        //        if (LinkedListMap.Navigation.Any())
        //        {
        //            var navigationFilterDefinition = navigationBuilder
        //            //linkedListFilterDefinition = builder.All<List<string>>("Navigation", LinkedListMap.Navigation);
        //            //var navigationFilterArray = LinkedListMap.Navigation.ToArray();
        //           // builder &= builder.Eq<List<string>>("Navigation", LinkedListMap.Navigation);
        //            //builder &= builder.ElemMatch<BsonArray>("Navigation",)
        //        }
        //    }
        //    return result;
        //}
        public bool IncludeChildren { get; set; }

        protected FilterDefinition<T> GetListFilterDefinition()
        {
            var builder = new FilterDefinitionBuilder<T>();
            FilterDefinition<T> result = null;
            var map = GetLinkListMaps().Result;
            var ids = new BsonArray();
            map.ForEach(linkedListMap =>
            {
                BsonValue val = null;
                if (typeof (V) == typeof (int))
                    val = new BsonInt32(int.Parse(linkedListMap.Key.ToString()));
                ids.Add(val);
      
            });

            result = builder.In(LinkedListSettings.KeyColumn.ColumnName, ids);
            return result;
        }

        protected BsonDocument GetListMatchDocument()
        {
            BsonDocument result = null;
            var filterDefinition = GetListFilterDefinition();
            if(filterDefinition != null)
                result = new BsonDocument {{"$match", ToDocument(filterDefinition)}};
            return result;
        }
        public LinkedListMap<V> LinkedListMap { get; set; }

        private List<string> _Path;

        public List<string> Path
        {
            get { return _Path ?? (_Path = new List<string>()); }
            set { _Path = value; }
        }

        public string CollectionName { get; set; }

        public string DataSourceName { get; set; }

        public string DataSourceLocation { get; set; }

        private List<FilterSpecification> _Filters;
        public List<FilterSpecification> Filters
        {
            get { return _Filters ?? (_Filters = new List<FilterSpecification>()); }
            set
            {
                _Filters = value;
            }
        }

        public async Task<List<T>> RunQuery()
        {
            var pl = new List<BsonDocument>();
            var matchDocument = GetListMatchDocument();
            if (matchDocument != null)
                pl.Add(matchDocument);
            var db = GetDatabase();
            var collection = db.GetCollection<T>(CollectionName);
            var asyncResult = collection.AggregateAsync<T>(pl);
            var result = asyncResult.Result.ToListAsync<T>();
            return await result;
        }


        public LinkedListSettings LinkedListSettings { get; set; }
    }
}
