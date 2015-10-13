using System;
using System.Collections.Generic;
using System.Dynamic;
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


    public class MongoTreeQueryStrategy<T> : IMongoTreeQueryStrategy<T>
    {
        public bool IncludeChildren { get; set; }

        protected FilterDefinition<dynamic> GetParentFilterDefinition()
        {
            var builder = new FilterDefinitionBuilder<dynamic>();
            FilterDefinition<dynamic> result = null;
            var activeFilters = Filters.Where(x => x.FilterValues.Any(y => y.Active)).ToList();
            if (activeFilters.Any())
            {

                result = new BsonDocument();
                activeFilters.ForEach(activeFilter =>
                {

                    var valuesList = new List<BsonValue>();
                    activeFilter.FilterValues.Where(x => x.Active).ToList().ForEach(activeFilterValue =>
                    {
                        BsonValue elementValue;
                        switch (activeFilter.Column.DataType)
                        {
                            case Globals.StringDatatypeName:
                                valuesList.Add(new BsonString(activeFilterValue.Value));
                                break;
                        }

                    });

                    result &= builder.In(activeFilter.Column.ColumnName, valuesList);

                });
            }
            return result;
        }
        protected List<BsonDocument> GetParentPipeline()
        {
            var result = new List<BsonDocument>();
            var filterDefinition = GetParentFilterDefinition();
            if (filterDefinition != null)
            {
                var matchDocument = new BsonDocument {{"$match", ToDocument(filterDefinition)}};
                result.Add(matchDocument);
            }
            var projection = new BsonDocument {{"$project", new BsonDocument {{"_id", "$_id"}}}};
            result.Add(projection);
            return result;
        }
        protected async Task<List<dynamic>> GetParentsAsync()
        {
            var collection = GetCollection();
            var asyncResult = collection.AggregateAsync<dynamic>(GetParentPipeline()).Result;
            return await asyncResult.ToListAsync<dynamic>();
        }
        protected FilterDefinition<dynamic> GetChildrenFilterDefinition()
        {
            var parents = GetParentsAsync().Result.Select(x => x._id).ToList();
            var builder = new FilterDefinitionBuilder<dynamic>();
            var result = builder.In("Parents", parents);
            return result;
        }
        protected FilterDefinition<dynamic> GetFilterDefinition()
        {
            FilterDefinition<dynamic> result = null;
            if (!IncludeChildren)
                result = GetParentFilterDefinition();
            else
            {
                var builder = new FilterDefinitionBuilder<dynamic>();
                result =
                    builder.Or(new List<FilterDefinition<dynamic>>
                    {
                        GetParentFilterDefinition(),
                        GetChildrenFilterDefinition()
                    });
            }
            return result;
        }

        protected List<BsonDocument> GetPipeline()
        {
            var result = new List<BsonDocument>();
            var filterDefinition = GetFilterDefinition();
            if (filterDefinition != null)
            {
                var matchDocument = new BsonDocument {{"$match", ToDocument(filterDefinition)}};
                result.Add(matchDocument);
            }
            return result;
        }
        public async Task<List<T>> RunQuery()
        {
            var pl = GetPipeline();
            var collection = GetCollection();
            var asyncResult = collection.AggregateAsync<T>(pl).Result;
            return await asyncResult.ToListAsync<T>();
        }
        protected BsonDocument ToDocument(FilterDefinition<dynamic> source){
            var serializer = BsonSerializer.SerializerRegistry.GetSerializer<dynamic>();
            return source.Render(serializer, BsonSerializer.SerializerRegistry);
        }
        IMongoCollection<dynamic> GetCollection()
        {
            var client = new MongoClient(DataSourceLocation);
            var db = client.GetDatabase(DataSourceName);
            var result = db.GetCollection<dynamic>(CollectionName);
            return result;
        }
        public string CollectionName { get; set; }
        public string DataSourceName { get; set; }
        public string DataSourceLocation { get; set; }

        public List<FilterSpecification> Filters
        {
            get { return _Filters ?? (_Filters = new List<FilterSpecification>()); }
            set { _Filters = value; }
        }

        private List<FilterSpecification> _Filters;
    }
}
