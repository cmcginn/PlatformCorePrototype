using System.Collections.Generic;
using System.Threading.Tasks;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Services.DataStructures
{
    public interface IMongoTreeQueryStrategy<T> : ITreeQueryStrategy<T>,IMongoQueryStrategy<T>
    {

    }
}