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

            string[] data = dataToRead.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            string[] singleNumbers = data[0].Split(new char[] { ' ' });

            int.TryParse((string)singleNumbers[0], out var numberOfCities);
            for (int i = 0; i < numberOfCities; i++)
            {
                graph.AdjacencyList.Add(new List<Connection>());
            }

            int.TryParse(singleNumbers[1], out var numberOfConnections);

            for(int i = 1; i < 1 + numberOfConnections; i++)
            {
                string line = data[i];
                string[] singleStrings = line.Split(new char[] { ' ' });
                //try
                //{
                    Connection connection = new Connection()
                    {
                        CityA = int.Parse(singleStrings[0]),
                        CityB = int.Parse(singleStrings[1]),
                        Distance = int.Parse(singleStrings[2])
                    };
                    graph.AdjacencyList[connection.CityA - 1].Add(connection);
                    connection = new Connection()
                    {
                        CityA = int.Parse(singleStrings[1]),
                        CityB = int.Parse(singleStrings[0]),
                        Distance = int.Parse(singleStrings[2])
                    };
                    graph.AdjacencyList[connection.CityA - 1].Add(connection); //CityA is now different
                //}
                //catch (Exception e)
                //{
                //    throw; // todo
                //}
            }

            string[] road = data[1 + numberOfConnections].Split(new char[] { ' ' }); // todo: finish
            //road[0] is a starting node
            //road[1] is an ending node

            return graph;
        }
    }
}
