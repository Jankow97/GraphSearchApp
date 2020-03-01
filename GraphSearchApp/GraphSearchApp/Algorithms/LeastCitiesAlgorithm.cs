using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphSearchApp.Algorithms.Interfaces;
using GraphSearchApp.Dtos;

namespace GraphSearchApp.Algorithms
{
    class LeastCitiesAlgorithm : IGraphSearchExecute
    {
        public AlgorithmResult ExecuteSearch(Graph graph)//, int startingNode, int endingNode)
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(startingNode);
            while (q.Any())
            {
                List<Connection> v = graphDto.Graph[q.Dequeue()];
                foreach (var node in v)
                {
                    node.CityA = 1;
                }
            }
            throw new NotImplementedException();
        }
    }
}
