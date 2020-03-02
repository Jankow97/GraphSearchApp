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
            var realResult = graphSearchExecuter.ExecuteSearch(graph, graphSearchOptions);
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
            graphSearchExecuter.ExecuteSearch(graph, graphSearchOptions);
            AlgorithmResult realResult = graphSearchExecuter.ExecuteSearch(graph, graphSearchOptions);
            bool isTrue = realResult != null && predictedResult == realResult;

            // Assert
            Assert.True(isTrue, "One town to one town does not work.");
        }
    }
}
