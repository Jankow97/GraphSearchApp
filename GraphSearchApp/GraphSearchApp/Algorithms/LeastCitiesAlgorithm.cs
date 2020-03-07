using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphSearchApp.Algorithms.Interfaces;
using GraphSearchApp.Dtos;

namespace GraphSearchApp.Algorithms
{
    public class LeastCitiesAlgorithm : IGraphSearchExecute
    {
        Queue<int> q = new Queue<int>();
        bool[] scheduled = null;
        bool[] visited = null;
        private int[] predecessor = null;
        private int[] distances = null;
        private int[] moves = null;

        public AlgorithmResult ExecuteSearch(Graph graph, GraphSearchOptions graphSearchOptions)
        {
            q = new Queue<int>();
            scheduled = new bool[graph.Count];
            visited = new bool[graph.Count];
            predecessor = new int[graph.Count];
            distances = new int[graph.Count];
            moves = new int[graph.Count];

            var algorithmResult = new AlgorithmResult();
            bool found = false;

            ScheduleNodeToVisit(graphSearchOptions.StartingNode);
            predecessor[graphSearchOptions.StartingNode - 1] = graphSearchOptions.StartingNode - 1;
            moves[graphSearchOptions.StartingNode - 1] = 0;

            while (q.Any() && q.Peek() != graphSearchOptions.EndingNode - 1)
            {
                int currentNode = q.Dequeue();
                visited[currentNode - 1] = true;

                List<Connection> connections = graph.GetNodeList(currentNode);
                foreach (var node in connections)
                {
                    if (!WasScheduled(node.CityB) || (moves[node.CityB] == moves[node.CityA] + 1 && distances[node.CityB] > distances[node.CityA] + node.Distance))
                    {
                        predecessor[node.CityB] = node.CityA;
                        distances[node.CityB] = distances[node.CityA] + node.Distance;
                        moves[node.CityB] = moves[node.CityA] + 1;
                        if (node.CityB == graphSearchOptions.EndingNode - 1)
                        {
                            found = true;
                        }
                        if(!WasScheduled(node.CityB))
                            ScheduleNodeToVisit(node.CityB + 1);
                    }
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
            if (visited[node])
                return true;
            else
                return false;
        }

        private bool WasScheduled(int node)
        {
            if (scheduled[node])
                return true;
            else
                return false;
        }

        private void ScheduleNodeToVisit(int node)
        {
            q.Enqueue(node);
            scheduled[node - 1] = true;
        }
    }
}
