﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSearchApp.Dtos
{
    public class Graph
    {
        public List<List<Connection>> AdjacencyList { get; set; }
    }
}
