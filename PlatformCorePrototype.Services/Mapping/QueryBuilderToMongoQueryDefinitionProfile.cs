using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlatformCorePrototype.Services.DataStructures;

namespace PlatformCorePrototype.Services.Mapping
{
    public class QueryBuilderToMongoQueryDefinitionProfile:Profile
    {
        protected override void Configure()
        {
            //Mapper.CreateMap<IMongoQueryStrategy, MongoQueryDefinition>()
            //    .ForMember(d => d.Filters, s => s.MapFrom(n => n.SelectedFilters));

        }
        
    }
}
