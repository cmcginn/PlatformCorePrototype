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
using PlatformCorePrototype.Services.Helpers;

namespace PlatformCorePrototype.Services.DataStructures
{
    public class MongoLinkedListQueryStrategy<T,V>:IMongoLinkedListQueryStrategy<T,V>
    {

        #region public interface

        public bool IncludeChildren { get; set; }

        public List<string> Path { get; set; }

        public V Key { get; set; }

        public IDataCollectionMetadata CollectionMetadata { get; set; }

        public List<FilterSpecification> Filters { get; set; }

        public string ViewId { get; set; }

       
        public Task<List<T>> RunQuery()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region utilities

        /// <summary>
        /// Gets the filter definition for the linked list map based on Navigation (Path Property)
        /// Preserves order of filters by Index (i.e.  { "Navigation.0" : "Account", "Navigation.1" : "SalesPerson" })
        /// </summary>
        /// <returns></returns>
        protected virtual FilterDefinition<T> GetNavigationFilterDefinition()
        {
      
            FilterDefinition<T> result = null;
            if (Path != null && Path.Any())
            {
                var builder = new FilterDefinitionBuilder<T>();
                result = new BsonDocument();
                var filterDefinitions = new List<FilterDefinition<T>>();
                for (var i = 0; i < Path.Count; i++)
                {
       
                    result &= builder.Eq(String.Format("Navigation.{0}", i), Path[i]);
                }
               
            }
            return result;
        }

        protected virtual async Task<FilterDefinition<T>> GetLinkedListMapsFilterDefinition()
        {
            var db = GetDatabase();
            var collection = db.GetCollection<BsonDocument>(LinkedListDataCollectionMetadata.MapCollectionName);
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
                    asyncResult = builder.In(LinkedListDataCollectionMetadata.KeyColumnName, keyValues);

                }
                return asyncResult;
            });
            return await result;
        }

        
        protected virtual async Task<List<BsonDocument>> GetQueryPipeline()
        {
            var asyncResult = new List<BsonDocument>();
            FilterDefinition<T> linkedListFilters = null;
            if (Path != null && Path.Any())
                linkedListFilters = await GetLinkedListMapsFilterDefinition();
            if (linkedListFilters != null)
                asyncResult.Add(linkedListFilters.Serialize());
            return asyncResult;

        }

        private LinkedListDataCollectionMetadata _LinkedListDataCollectionMetadata;
        protected LinkedListDataCollectionMetadata LinkedListDataCollectionMetadata
        {
            get
            {
                if (_LinkedListDataCollectionMetadata == null)
                {
                    _LinkedListDataCollectionMetadata = CollectionMetadata as LinkedListDataCollectionMetadata;
                    if (_LinkedListDataCollectionMetadata == null)
                        throw new System.Exception(
                            "LinkedListQueryBuilder Strategy requires IDataCollectionMetadata of type LinkedListDataCollectionMetadata");
                    
                }
                return _LinkedListDataCollectionMetadata;
            }
            set { _LinkedListDataCollectionMetadata = value; }
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
