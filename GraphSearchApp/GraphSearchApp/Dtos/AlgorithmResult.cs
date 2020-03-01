using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSearchApp.Dtos
{
    public class AlgorithmResult
    {
        public int ShortestRoute { get; set; }
        public List<int> CitiesTraverseOrder { get; set; }
    }
}
