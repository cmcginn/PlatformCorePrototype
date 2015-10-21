//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using AutoMapper;
//using PlatformCorePrototype.Core.DataStructures;
//using PlatformCorePrototype.Core.Models;
//using PlatformCorePrototype.Services.DataStructures;

//namespace PlatformCorePrototype.Web.Mapping
//{
//    public class LinkedListQueryBuilderToMongoLinkedListQueryStrategyProfile:Profile
//    {
//        protected override void Configure()
//        {
//            Mapper.CreateMap<LinkedListQueryBuilder, MongoLinkedListQueryStrategy<dynamic, int>>()
//                .ForMember(dest=>dest.QueryBuilder, src=>src.MapFrom(x=>x))
//                .ForMember(dest => dest.Path, src => src.Ignore())
//                .ForMember(dest => dest.IncludeChildren, src => src.UseValue(true))
//                .AfterMap((source, dest) =>
//                {
//                    dest.Path = source.SelectedPath.Navigation.Split('.').ToList();
                   
//                });


//        }
//    }
//}