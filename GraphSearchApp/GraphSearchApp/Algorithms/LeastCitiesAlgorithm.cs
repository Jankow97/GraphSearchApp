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
        public AlgorithmResult ExecuteSearch(Graph graph, GraphSearchOptions graphSearchOptions)
        {
            var algorithmResult = new AlgorithmResult();
            bool found = false;

            Queue<int> q = new Queue<int>();
            q.Enqueue(graphSearchOptions.StartingNode);
            while (q.Any())
            {
                int currentNode = q.Dequeue();

                algorithmResult.CitiesTraverseOrder.Add(currentNode);
                List<Connection> connections = graph.AdjacencyList[currentNode];
                foreach (var node in connections)
                {
                    if (node.CityB == graphSearchOptions.EndingNode)
                    {
                        if (found != true)
                        {
                            algorithmResult.CitiesTraverseOrder.Add(node.CityB);
                        }
                        found = true;
                    }
                    q.Enqueue(node.CityB);
                }

                if (found)
                {
                    break;
                }
            }

            if (!found)
            {
                return null;
            }

            return algorithmResult;
        }
    }
}
