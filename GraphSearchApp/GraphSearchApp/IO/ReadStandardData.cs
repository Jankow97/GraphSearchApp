using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphSearchApp.Dtos;
using GraphSearchApp.IO.Interfaces;

namespace GraphSearchApp.IO
{
    class ReadStandardData : ITextToGraph
    {
        public Graph ReadData(string dataToRead)
        {
            Graph graph = new Graph()
            {
                AdjacencyList = new List<List<Connection>>()
            };

            string[] data = dataToRead.Split(new char[] { '\n' });
            foreach (var line in data)
            {
                string[] singleStrings = line.Split(new char[] { ' ' });
                graph.AdjacencyList.Add(new List<Connection>());
                try
                {
                    Connection connection = new Connection()
                    {
                        CityA = int.Parse(singleStrings[0]),
                        CityB = int.Parse(singleStrings[1]),
                        Distance = int.Parse(singleStrings[2])
                    };
                    graph.AdjacencyList[graph.AdjacencyList.Capacity - 1].Add(connection);
                }
                catch (Exception e)
                {
                    throw; // todo
                }
            }

            return graph;
        }
    }
}
