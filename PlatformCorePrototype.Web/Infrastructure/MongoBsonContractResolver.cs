using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Serialization;

namespace PlatformCorePrototype.Web.Infrastructure
{
    public class MongoBsonContractResolver:DefaultContractResolver
    {
        public override JsonContract ResolveContract(Type type)
        {
            return base.ResolveContract(type);
        }
    }
}