using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSearchApp.Dtos
{
    public class Graph
    {
        public int[] YGeolocations = new int[2000000];
        public int[] XGeolocations = new int[2000000];

        private List<List<Connection>> AdjacencyList { get; set; }

        public Graph()
        {
            AdjacencyList = new List<List<Connection>>();
        }

        public void DeclareNewNode() => AdjacencyList.Add(new List<Connection>());

        public void AddDirectedConnection(Connection connection)
        {
            AdjacencyList[connection.CityA - 1].Add(new Connection(connection.CityA, connection.CityB, connection.Distance));
        }

        public void AddUndirectedConnection(Connection connection)
        {
            AddDirectedConnection(new Connection(connection.CityA, connection.CityB, connection.Distance));
            AddDirectedConnection(new Connection(connection.CityB, connection.CityA, connection.Distance));
        }

        public List<Connection> GetNodeList(int nodeNumber)
        {
            return AdjacencyList[nodeNumber - 1];
        }

        public int Count => AdjacencyList.Count;
    }
}
