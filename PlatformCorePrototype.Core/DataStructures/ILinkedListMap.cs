﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCorePrototype.Core.DataStructures
{
    public interface ILinkedListMap
    {
        string SlicerColumnName { get; set; }
        List<ILinkedListNavigationMap> NavigationMaps { get; set; }
    }
}
