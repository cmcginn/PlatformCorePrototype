﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public class DataColumnMetadata
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }

        public List<DataColumnMetadata> Columns
        {
            get { return _Columns ?? (_Columns = new List<DataColumnMetadata>()); }
            set { _Columns = value; }
        }

        private List<DataColumnMetadata> _Columns;
    }
}
