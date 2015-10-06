using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Services.DataStructures
{
    public class MongoQueryDefinition:QueryDefinition,IMongoQueryDefinition
    {
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
    }
}
