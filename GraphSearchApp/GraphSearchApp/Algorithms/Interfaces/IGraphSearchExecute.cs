using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphSearchApp.Dtos;

namespace GraphSearchApp.Algorithms.Interfaces
{
    interface IGraphSearchExecute
    {
        AlgorithmResult ExecuteSearch(Graph graph, GraphSearchOptions graphSearchOptions);
    }
}
