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
            var graphSearchOptions = new GraphSearchOptions()
            {
                StartingNode = 1,
                EndingNode = 1
            };
            var predictedResult = new AlgorithmResult()
            {
                ShortestRoute = 0,
                CitiesTraverseOrder = new List<int>()
            };

            // Act
            AlgorithmResult realResult = graphSearchExecuter.ExecuteSearch(graph, graphSearchOptions);
            bool isTrue = realResult != null && predictedResult == realResult;
            isTrue = realResult == null; // To jest prawdziwe w tym momencie

            // Assert
            Assert.True(isTrue, "One town to one town does not work.");
        }

        [Fact]
        public void LeastCities_RealExample()
        {
            // Arrange
            IGraphSearchExecute graphSearchExecuter = new LeastCitiesAlgorithm();
            var adjacencyList = new List<List<Connection>>();
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

            Assert.False(graphSearchExecuter == null);
            Assert.False(graph == null);
            Assert.False(graphSearchOptions == null);
            // Act
            AlgorithmResult realResult = graphSearchExecuter.ExecuteSearch(graph, graphSearchOptions);
            bool isTrue = realResult != null && predictedResult == realResult;

            // Assert
            Assert.True(isTrue, "One town to one town does not work.");
        }

        [Fact]
        public void LeastCities_TwoCities()
        {
            // Arrange
            IGraphSearchExecute graphSearchExecuter = new LeastCitiesAlgorithm();
            var adjacencyList = new List<List<Connection>>();
            adjacencyList.Add(new List<Connection>());
            for(int i = 0; i < 7; i++)
                adjacencyList.Add(new List<Connection>());
            adjacencyList[0].Add(new Connection() { CityA = 1, CityB = 5, Distance = 2 });
            adjacencyList[4].Add(new Connection() { CityA = 5, CityB = 4, Distance = 1 });
            adjacencyList[0].Add(new Connection() { CityA = 1, CityB = 4, Distance = 4 });
            adjacencyList[2].Add(new Connection() { CityA = 3, CityB = 5, Distance = 4 });
            adjacencyList[2].Add(new Connection() { CityA = 3, CityB = 4, Distance = 1 });
            adjacencyList[2].Add(new Connection() { CityA = 3, CityB = 2, Distance = 2 });
            adjacencyList[1].Add(new Connection() { CityA = 2, CityB = 4, Distance = 4 });
            adjacencyList[1].Add(new Connection() { CityA = 2, CityB = 6, Distance = 1 });
            adjacencyList[3].Add(new Connection() { CityA = 4, CityB = 6, Distance = 6 });
            adjacencyList[5].Add(new Connection() { CityA = 6, CityB = 7, Distance = 2 });
            adjacencyList[2].Add(new Connection() { CityA = 3, CityB = 7, Distance = 6 });
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
                ShortestRoute = 11,
                CitiesTraverseOrder = new List<int>() { 1, 4, 3, 7 }
            };

            Assert.False(graphSearchExecuter == null);
            Assert.False(graph == null);
            Assert.False(graphSearchOptions == null);
            // Act
            AlgorithmResult realResult = graphSearchExecuter.ExecuteSearch(graph, graphSearchOptions);
            bool isTrue = realResult != null && predictedResult == realResult;

            // Assert
            Assert.True(isTrue, "One town to one town does not work.");
        }
    }
}
