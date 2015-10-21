using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace PlatformCorePrototype.Services.DataStructures
{
    public class TreeNode<T>
    {
        private Ancestor _id;

        public Ancestor Id
        {
            get { return _id ?? (_id = new Ancestor()); }
            set { _id = value; }
        }
        public T Value { get; set; }
        List<Ancestor> _Ancestors;
        public List<Ancestor> Ancestors
        {
            get { return _Ancestors ?? (_Ancestors = new List<Ancestor>()); }
            set { _Ancestors = value; }
        }
        public int Level { get { return Ancestors.Count; } }


    }

}
