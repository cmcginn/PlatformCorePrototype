using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace PlatformCorePrototype.Services.DataStructures
{
    public class Ancestor
    {
        ObjectId _id;
        public string ColumnName { get; set; }
        public ObjectId Id
        {
            get { return _id == ObjectId.Empty ? _id = ObjectId.GenerateNewId() : _id; }

            set { _id = value; }
            
        }
    }
}
