using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GraphSearchApp.Dtos;
using GraphSearchApp.IO.Interfaces;

namespace GraphSearchApp.IO
{
    class ReadStandardData : ITextToGraph
    {
        public ReadData ReadData(string dataToRead)
        {
            Graph graph = new Graph();
            try
            {
                string[] data = dataToRead.Split(new string[] {"\r\n"}, StringSplitOptions.None);

                string[] singleNumbers = data[0].Split(new char[] {' '});

                var numberOfConnections = 0;
                try
                {
                    var numberOfCities = int.Parse((string)singleNumbers[0]);
                    for (int i = 0; i < numberOfCities; i++)
                    {
                        graph.DeclareNewNode();
                    }
                }
                catch
                {
                    MessageBox.Show("Wrong number of cities.");
                }
                try
                {
                    numberOfConnections = int.Parse(singleNumbers[1]);
                }
                catch
                {
                    MessageBox.Show("Wrong number of connections.");
                }

                for (int i = 1; i < 1 + numberOfConnections; i++)
                {
                    string line = data[i];
                    string[] singleStrings = line.Split(new char[] { ' ' });
                    try
                    {
                        var cityA = int.Parse(singleStrings[0]);
                        var cityB = int.Parse(singleStrings[1]);
                        var distance = int.Parse(singleStrings[2]);
                        graph.AddUndirectedConnection(new Connection(cityA, cityB, distance));
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.StackTrace);
                        throw;
                    }
                }

                string[] road = data[1 + numberOfConnections].Split(new char[] { ' ' });
                var readData = new ReadData()
                {
                    Graph = graph,
                    GraphSearchOptions = new GraphSearchOptions()
                    {
                        StartingNode = int.Parse(road[0]),
                        EndingNode = int.Parse(road[1])
                    }
                };

                return readData;
            }
            catch (Exception e)
            {

                MessageBox.Show("Problem with the file." + e.StackTrace);
                return null;
            }
        }
    }
}
