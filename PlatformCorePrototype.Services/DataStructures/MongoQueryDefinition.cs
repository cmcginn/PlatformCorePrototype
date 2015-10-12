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
    public class MongoQueryDefinition:QueryDefinition,IMongoQueryDefinition
    {
        //public static FilterDefinition<BsonDocument> GetFilterDefinition(FilterSpecification filterSpec)
        //{
        //    FilterDefinition<BsonDocument> result = null;
        //    filterSpec.FilterValues.Where(x => x.Active).ToList().ForEach(filterValue =>
        //    {

        //    });
        //    return result;
        //}
        public FilterDefinition<BsonDocument> GetMatchDocument()
        {
           
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            FilterDefinition<BsonDocument> result=null;
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

        public List<BsonDocument> GetPipeline()
        {
            var result = new List<BsonDocument>();
            var matchDefinition = GetMatchDocument();
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>();
            if (matchDefinition != null)
            {
                var matchDocument = new BsonDocument(new BsonElement("$match", matchDefinition.Render(documentSerializer, BsonSerializer.SerializerRegistry)));
                result.Add(matchDocument);
            }

            return result;
        }
    }
}
