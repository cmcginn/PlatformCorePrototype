﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Services.DataStructures;
using PlatformCorePrototype.Services.Models;

namespace PlatformCorePrototype.Services.Mapping
{
    public class ViewDefinitionMetadataToViewDefinitionProfile:Profile
    {
        protected override void Configure()
        {
            //Mapper.CreateMap<ViewDefinitionMetadata, ViewDefinitionModel>()
            //    .AfterMap((source, dest) =>
            //    {
            //        dest.QueryBuilder = new IMongoQueryStrategy {ViewId = source.Id};
            //    });

        }
    }
}