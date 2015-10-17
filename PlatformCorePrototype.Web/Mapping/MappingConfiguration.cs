using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;


namespace PlatformCorePrototype.Web.Mapping
{
    public class MappingConfiguration
    {
        private static bool _Initialized;
        public static void ConfigureMappings()
        {
            if (_Initialized)
                return;
            Mapper.Initialize(cfg =>
            {
                Core.Configuration.MappingConfiguration.Configure(cfg);
            });
 
            Mapper.AssertConfigurationIsValid();
            _Initialized = true;
           
        }
    }
}