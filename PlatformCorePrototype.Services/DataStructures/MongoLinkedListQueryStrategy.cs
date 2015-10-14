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

        public Task<List<T>> RunQuery()
        {
            throw new NotImplementedException();
        }


        public LinkedListSettings LinkedListSettings { get; set; }
    }
}
