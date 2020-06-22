using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphSearchApp.Algorithms.Interfaces;
using GraphSearchApp.Dtos;

namespace GraphSearchApp.Algorithms
{
    class ShortestRoutesAlgorithmWithGeolocation : IGraphSearchExecute
    {
        private MinHeap2 heap = new MinHeap2(2000000);
        bool[] scheduled = null;
        bool[] visited = null;
        private int[] predecessor = null;
        private int[] distances = null;

        public AlgorithmResult ExecuteSearch(Graph graph, GraphSearchOptions graphSearchOptions)
        {
            heap = new MinHeap2(2000000);
            scheduled = new bool[graph.Count];
            visited = new bool[graph.Count];
            predecessor = new int[graph.Count];
            distances = new int[graph.Count];

            var algorithmResult = new AlgorithmResult();
            bool found = false;

            ScheduleNodeToVisit(new NodeWithDistanceAndGeolocation() { Node = graphSearchOptions.StartingNode, Distance = 0, Predecessor = graphSearchOptions.StartingNode });
            predecessor[graphSearchOptions.StartingNode - 1] = graphSearchOptions.StartingNode - 1;
            NodeWithDistanceAndGeolocation.EndingNode = new NodeWithDistanceAndGeolocation()
            {
                Node = graphSearchOptions.EndingNode,
                X = graph.XGeolocations[graphSearchOptions.EndingNode - 1],
                Y = graph.YGeolocations[graphSearchOptions.EndingNode - 1],
                Heuristic = 0
            };
            try
            {
                while (!heap.IsEmpty())
                {
                    NodeWithDistanceAndGeolocation currentNode = heap.Peek();
                    heap.Pop();
                    if (visited[currentNode.Node - 1])
                        continue;
                    visited[currentNode.Node - 1] = true;
                    predecessor[currentNode.Node - 1] = currentNode.Predecessor - 1;
                    distances[currentNode.Node - 1] = currentNode.Distance;
                    if (currentNode.Node - 1 == graphSearchOptions.EndingNode - 1)
                        break;

                    List<Connection> connections = graph.GetNodeList(currentNode.Node);
                    foreach (var node in connections)
                    {
                        if (!WasVisited(node.CityB))
                        {
                            if (node.CityB == graphSearchOptions.EndingNode)
                            {
                                found = true;
                            }

                            ScheduleNodeToVisit(new NodeWithDistanceAndGeolocation()
                            {
                                Node = node.CityB,
                                Distance = distances[node.CityA - 1] + node.Distance, 
                                Predecessor = node.CityA,
                                X = graph.XGeolocations[node.CityB - 1],
                                Y = graph.YGeolocations[node.CityB - 1],
                                Heuristic = Math.Sqrt(
                                    Math.Abs(NodeWithDistanceAndGeolocation.EndingNode.X -
                                             graph.XGeolocations[node.CityB - 1]) *
                                    Math.Abs(NodeWithDistanceAndGeolocation.EndingNode.X -
                                             graph.XGeolocations[node.CityB - 1]) +
                                    Math.Abs(NodeWithDistanceAndGeolocation.EndingNode.Y -
                                             graph.YGeolocations[node.CityB - 1]) *
                                    Math.Abs(NodeWithDistanceAndGeolocation.EndingNode.Y -
                                             graph.YGeolocations[node.CityB - 1]))
                            });
                        }
                    }
                }
            }
            catch
            {
                //
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

        private bool WasScheduled(int node)
        {
            if (scheduled[node - 1])
                return true;
            else
                return false;
        }

        private void ScheduleNodeToVisit(NodeWithDistanceAndGeolocation node)
        {
            heap.Add(node);
            scheduled[node.Node - 1] = true;
        }
    }

    public class NodeWithDistanceAndGeolocation : IComparable
    {
        public static NodeWithDistanceAndGeolocation EndingNode { get; set; }
        public int Node { get; set; }
        public int Distance { get; set; }
        public int Predecessor { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Heuristic { get; set; }
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            var otherNode = obj as NodeWithDistanceAndGeolocation;
            //double result = Math.Sqrt(Math.Abs(EndingNode.X - this.X) * Math.Abs(EndingNode.X - this.X) +
            //                          Math.Abs(EndingNode.Y - this.Y) * Math.Abs(EndingNode.Y - this.Y));
            //double resultOther = Math.Sqrt(Math.Abs(EndingNode.X - otherNode.X) * Math.Abs(EndingNode.X - otherNode.X) + 
            //                               Math.Abs(EndingNode.Y - otherNode.Y) * Math.Abs(EndingNode.Y - otherNode.Y));

            if (this.Distance.CompareTo(otherNode.Distance) == 0 && this.Heuristic.CompareTo(otherNode.Heuristic) == 0)
                return this.Node.CompareTo(otherNode.Node);
            else if (this.Heuristic.CompareTo(otherNode.Heuristic) == 0)
                return this.Distance.CompareTo(otherNode.Distance);
            else
                return this.Heuristic.CompareTo(otherNode.Heuristic);
        }
    }

    public class MinHeap2
    {
        private readonly NodeWithDistanceAndGeolocation[] _elements;
        private int _size;

        public MinHeap2(int size)
        {
            _elements = new NodeWithDistanceAndGeolocation[size];
        }

        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
        private bool IsRoot(int elementIndex) => elementIndex == 0;

        private NodeWithDistanceAndGeolocation GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
        private NodeWithDistanceAndGeolocation GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
        private NodeWithDistanceAndGeolocation GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = _elements[firstIndex];
            _elements[firstIndex] = _elements[secondIndex];
            _elements[secondIndex] = temp;
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public NodeWithDistanceAndGeolocation Peek()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            return _elements[0];
        }

        public NodeWithDistanceAndGeolocation Pop()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            var result = _elements[0];
            _elements[0] = _elements[_size - 1];
            _size--;

            ReCalculateDown();

            return result;
        }

        public void Add(NodeWithDistanceAndGeolocation element)
        {
            if (_size == _elements.Length)
                throw new IndexOutOfRangeException();

            _elements[_size] = element;
            _size++;

            ReCalculateUp();
        }

        private void ReCalculateDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                var smallerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index).CompareTo(GetLeftChild(index)) < 0)
                {
                    smallerIndex = GetRightChildIndex(index);
                }

                if (_elements[smallerIndex].CompareTo(_elements[index]) >= 0)
                {
                    break;
                }

                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }

        private void ReCalculateUp()
        {
            var index = _size - 1;
            while (!IsRoot(index) && _elements[index].CompareTo(GetParent(index)) < 0)
            {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }
    }
}
