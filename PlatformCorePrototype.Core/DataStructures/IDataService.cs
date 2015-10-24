using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public interface IDataService
    {
       
        Task<List<ILinkedListMap>> GetLinkedListMaps(string viewId);
        Task<List<T>> GetDataAsync<T>(IQueryStrategy<T> strategy);
        Task<IDataCollectionMetadata> GetCollectionMetadataByViewId(string viewId);
    }
}
