using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using GraphSearchApp.Algorithms;
using GraphSearchApp.Algorithms.Interfaces;
using GraphSearchApp.Dtos;

namespace GraphSearchApp.Tests.Algorithms
{
    public class LeastCitiesTest
    {
        [Fact]
        public void LeastCities_OneTown()
        {
            // Arrange
            IGraphSearchExecute graphSearchExecuter = new LeastCitiesAlgorithm();
            var graph = new Graph()
            {
                AdjacencyList = new List<List<Connection>>()
            };
            graph.AdjacencyList.Add(new List<Connection>());
            var graphSearchOptions = new GraphSearchOptions()
            {
                StartingNode = 1,
                EndingNode = 1
            };

            // Act
            AlgorithmResult realResult = graphSearchExecuter.ExecuteSearch(graph, graphSearchOptions);
            bool isTrue = realResult == null; // Nie znalazł i zwraca null

            // Assert
            Assert.True(isTrue, "One town to one town does not work.");
        }

        [Fact]
        public void LeastCities_NotConnectedEndTown()
        {
            // Arrange
            IGraphSearchExecute graphSearchExecuter = new LeastCitiesAlgorithm();
            var graph = new Graph()
            {
                AdjacencyList = new List<List<Connection>>()
            };
            graph.AdjacencyList.Add(new List<Connection>());
            graph.AdjacencyList.Add(new List<Connection>());
            var graphSearchOptions = new GraphSearchOptions()
            {
                StartingNode = 1,
                EndingNode = 2
            };

            // Act
            AlgorithmResult realResult = graphSearchExecuter.ExecuteSearch(graph, graphSearchOptions);
            bool isTrue = realResult == null; // Nie znalazł i zwraca null

            // Assert
            Assert.True(isTrue, "Unconnected town and algorithm returns null - not found.");
        }

        [Fact]
        public void LeastCities_TwoCities()
        {
            // Arrange
            IGraphSearchExecute graphSearchExecuter = new LeastCitiesAlgorithm();
            var adjacencyList = new List<List<Connection>>();
            for(int i = 0; i < 2; i++)
                adjacencyList.Add(new List<Connection>());
            adjacencyList[0].Add(new Connection() {CityA = 1, CityB = 2, Distance = 10});
            var graph = new Graph()
            {
                AdjacencyList = adjacencyList
            };
            var graphSearchOptions = new GraphSearchOptions()
            {
                StartingNode = 1,
                EndingNode = 2
            };
            var predictedResult = new AlgorithmResult()
            {
                ShortestRoute = 10,
                CitiesTraverseOrder = new List<int>() { 1, 2}
            };

            // Act
            AlgorithmResult realResult = graphSearchExecuter.ExecuteSearch(graph, graphSearchOptions);
            bool isTrue = realResult != null &&
                          predictedResult.Equals(realResult);

            // Assert
            Assert.True(isTrue, "One town to second town does not work.");
        }

        [Fact]
        public void LeastCities_RealExample()
        {
            // Arrange
            IGraphSearchExecute graphSearchExecuter = new LeastCitiesAlgorithm();
            var adjacencyList = new List<List<Connection>>();
            for(int i = 0; i < 7; i++)
                adjacencyList.Add(new List<Connection>());

            adjacencyList[0].Add(new Connection() { CityA = 1, CityB = 5, Distance = 2 });
            adjacencyList[4].Add(new Connection() { CityA = 5, CityB = 1, Distance = 2 });

            adjacencyList[4].Add(new Connection() { CityA = 5, CityB = 4, Distance = 1 });
            adjacencyList[3].Add(new Connection() { CityA = 4, CityB = 5, Distance = 1 });

            adjacencyList[0].Add(new Connection() { CityA = 1, CityB = 4, Distance = 4 });
            adjacencyList[3].Add(new Connection() { CityA = 4, CityB = 1, Distance = 4 });

            adjacencyList[2].Add(new Connection() { CityA = 3, CityB = 5, Distance = 4 });
            adjacencyList[4].Add(new Connection() { CityA = 5, CityB = 3, Distance = 4 });

            adjacencyList[2].Add(new Connection() { CityA = 3, CityB = 4, Distance = 1 });
            adjacencyList[3].Add(new Connection() { CityA = 4, CityB = 3, Distance = 1 });

            adjacencyList[2].Add(new Connection() { CityA = 3, CityB = 2, Distance = 2 });
            adjacencyList[1].Add(new Connection() { CityA = 2, CityB = 3, Distance = 2 });

            adjacencyList[1].Add(new Connection() { CityA = 2, CityB = 4, Distance = 4 });
            adjacencyList[3].Add(new Connection() { CityA = 4, CityB = 2, Distance = 4 });

            adjacencyList[1].Add(new Connection() { CityA = 2, CityB = 6, Distance = 1 });
            adjacencyList[5].Add(new Connection() { CityA = 6, CityB = 2, Distance = 1 });

            adjacencyList[3].Add(new Connection() { CityA = 4, CityB = 6, Distance = 6 });
            adjacencyList[5].Add(new Connection() { CityA = 6, CityB = 4, Distance = 6 });

            adjacencyList[5].Add(new Connection() { CityA = 6, CityB = 7, Distance = 2 });
            adjacencyList[6].Add(new Connection() { CityA = 7, CityB = 6, Distance = 2 });

            adjacencyList[2].Add(new Connection() { CityA = 3, CityB = 7, Distance = 6 });
            adjacencyList[6].Add(new Connection() { CityA = 7, CityB = 3, Distance = 6 });


            var graph = new Graph()
            {
                AdjacencyList = adjacencyList
            };
            var graphSearchOptions = new GraphSearchOptions()
            {
                StartingNode = 1,
                EndingNode = 7
            };
            var predictedResult = new AlgorithmResult()
            {
                ShortestRoute = 11,
                CitiesTraverseOrder = new List<int>() { 1, 4, 3, 7 }
            };

            // Act
            AlgorithmResult realResult = graphSearchExecuter.ExecuteSearch(graph, graphSearchOptions);
            bool isTrue = realResult != null && 
                          predictedResult.Equals(realResult);

            // Assert
            Assert.True(isTrue, "In1 file does not work.");
        }
    }
}
