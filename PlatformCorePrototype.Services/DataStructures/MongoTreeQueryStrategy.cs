//using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MongoDB.Bson;
//using MongoDB.Bson.Serialization;
//using MongoDB.Driver;
//using PlatformCorePrototype.Core;
//using PlatformCorePrototype.Core.DataStructures;

//namespace PlatformCorePrototype.Services.DataStructures
//{


//    public class MongoTreeQueryStrategy<T> : IMongoTreeQueryStrategy<T>
//    {
//        public bool IncludeChildren { get; set; }

//        protected FilterDefinition<T> GetParentFilterDefinition()
//        {
//            var builder = new FilterDefinitionBuilder<T>();
//            FilterDefinition<T> result = null;
//            var activeFilters = Filters.Where(x => x.FilterValues.Any(y => y.Active)).ToList();
//            if (activeFilters.Any())
//            {

//                result = new BsonDocument();
//                activeFilters.ForEach(activeFilter =>
//                {

//                    var valuesList = new List<BsonValue>();
//                    activeFilter.FilterValues.Where(x => x.Active).ToList().ForEach(activeFilterValue =>
//                    {
//                        BsonValue elementValue;
//                        switch (activeFilter.Column.DataType)
//                        {
//                            case Globals.StringDataTypeName:
//                                valuesList.Add(new BsonString(activeFilterValue.Value));
//                                break;
//                        }

//                    });

//                    result &= builder.In(activeFilter.Column.ColumnName, valuesList);

//                });
//            }
//            return result;
//        }
//        protected List<BsonDocument> GetParentPipeline()
//        {
//            var result = new List<BsonDocument>();
//            var filterDefinition = GetParentFilterDefinition();
//            if (filterDefinition != null)
//            {
//                var matchDocument = new BsonDocument {{"$match", ToDocument(filterDefinition)}};
//                result.Add(matchDocument);
//            }
//            var projection = new BsonDocument {{"$project", new BsonDocument {{"_id", "$_id"}}}};
//            result.Add(projection);
//            return result;
//        }
//        protected async Task<List<T>> GetParentsAsync()
//        {
//            var collection = GetCollection();
//            var asyncResult = collection.AggregateAsync<T>(GetParentPipeline()).Result;
//            return await asyncResult.ToListAsync<T>();
//        }
//        protected FilterDefinition<T> GetChildrenFilterDefinition()
//        {
//            var parents = GetParentsAsync().Result.Select(x => x).ToList();
//            var builder = new FilterDefinitionBuilder<T>();
//            var result = builder.In("Parents", parents);
//            return result;
//        }
//        protected FilterDefinition<T> GetFilterDefinition()
//        {
//            FilterDefinition<T> result = null;
//            if (!IncludeChildren)
//                result = GetParentFilterDefinition();
//            else
//            {
//                var builder = new FilterDefinitionBuilder<T>();
//                result =
//                    builder.Or(new List<FilterDefinition<T>>
//                    {
//                        GetParentFilterDefinition(),
//                        GetChildrenFilterDefinition()
//                    });
//            }
//            return result;
//        }

//        protected List<BsonDocument> GetPipeline()
//        {
//            var result = new List<BsonDocument>();
//            var filterDefinition = GetFilterDefinition();
//            if (filterDefinition != null)
//            {
//                var matchDocument = new BsonDocument {{"$match", ToDocument(filterDefinition)}};
//                result.Add(matchDocument);
//            }
//            return result;
//        }
//        public async Task<List<T>> RunQuery()
//        {
//            var pl = GetPipeline();
//            var collection = GetCollection();
//            var asyncResult = collection.AggregateAsync<T>(pl).Result;
//            return await asyncResult.ToListAsync<T>();
//        }
//        protected BsonDocument ToDocument(FilterDefinition<T> source){
//            var serializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
//            return source.Render(serializer, BsonSerializer.SerializerRegistry);
//        }
//        IMongoCollection<T> GetCollection()
//        {
//            var client = new MongoClient(CollectionMetadata.DataSourceLocation);
//            var db = client.GetDatabase(CollectionMetadata.DataSourceName);
//            var result = db.GetCollection<T>(CollectionMetadata.Id);
//            return result;
//        }
//        public List<FilterSpecification> Filters
//        {
//            get { return _Filters ?? (_Filters = new List<FilterSpecification>()); }
//            set { _Filters = value; }
//        }

//        private List<FilterSpecification> _Filters;

//        public IDataCollectionMetadata CollectionMetadata { get; set; }

//        public ViewDefinitionMetadata ViewDefinitionMetadata { get; set; }


//        public string ViewId { get; set; }
//    }
//}
