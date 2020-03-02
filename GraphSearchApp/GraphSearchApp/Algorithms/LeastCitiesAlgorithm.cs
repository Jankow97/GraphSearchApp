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
        Queue<int> q = new Queue<int>();
        bool[] visited = null;
        private int[] predecessor = null;
        private int[] distances = null;
        private int[] moves = null;

        public AlgorithmResult ExecuteSearch(Graph graph, GraphSearchOptions graphSearchOptions)
        {
            q = new Queue<int>();
            visited = new bool[graph.AdjacencyList.Count];
            predecessor = new int[graph.AdjacencyList.Count];
            distances = new int[graph.AdjacencyList.Count];
            moves = new int[graph.AdjacencyList.Count];

            var algorithmResult = new AlgorithmResult();
            bool found = false;

            ScheduleNodeToVisit(graphSearchOptions.StartingNode);
            predecessor[graphSearchOptions.StartingNode - 1] = graphSearchOptions.StartingNode - 1;

            while (q.Any())
            {
                int currentNode = q.Dequeue();

                List<Connection> connections = graph.AdjacencyList[currentNode - 1];
                foreach (var node in connections)
                {
                    if (!WasVisited(node.CityB) || moves[node.CityB - 1] == moves[node.CityA - 1] + 1)
                    {
                        moves[node.CityB - 1] = moves[node.CityA - 1] + 1;
                        predecessor[node.CityB - 1] = node.CityA - 1;
                        distances[node.CityB - 1] = distances[node.CityA - 1] + node.Distance;
                        if (node.CityB == graphSearchOptions.EndingNode)
                        {
                            found = true;
                        }
                        ScheduleNodeToVisit(node.CityB);
                    }
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

            var tempNode = graphSearchOptions.EndingNode - 1;
            while (true)
            {
                algorithmResult.CitiesTraverseOrder.Add(tempNode + 1);
                if (predecessor[tempNode] == tempNode)
                    break;
                tempNode = predecessor[tempNode];
            }
            algorithmResult.CitiesTraverseOrder.Reverse();
            algorithmResult.ShortestRoute = distances[graphSearchOptions.EndingNode - 1];
            return algorithmResult;
        }

        private bool WasVisited(int node)
        {
            if (visited[node - 1])
                return true;
            else
                return false;
        }

        private void ScheduleNodeToVisit(int node)
        {
            q.Enqueue(node);
            visited[node - 1] = true; // todo: move to functions and private fields
        }
    }
}
