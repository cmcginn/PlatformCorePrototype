using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Core.Services
{
    public interface IDataService
    {
        Task<DataCollectionMetadata> GetDataCollectionMetadata(DataSourceSettings dataSource);
    }
}
