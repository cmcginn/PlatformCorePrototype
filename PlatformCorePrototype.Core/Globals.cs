using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core
{
    public class Globals
    {
        private static string _MongoConnectionString;

        public static string MongoConnectionString
        {
            get
            {
                if (String.IsNullOrEmpty(_MongoConnectionString))
                    _MongoConnectionString =
                        System.Configuration.ConfigurationManager.AppSettings["mongoConnectionString"];
                return _MongoConnectionString;
            }
        }
    }
}
