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
            var graph = new Graph();
            graph.DeclareNewNode();
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
            var graph = new Graph();
            graph.DeclareNewNode();
            graph.DeclareNewNode();
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
            var graph = new Graph();
            for (int i = 0; i < 2; i++)
            {
                graph.DeclareNewNode();
            }
            graph.AddUndirectedConnection(new Connection(1, 2, 10));
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
            var graph = new Graph();
            for (int i = 0; i < 7; i++)
            {
                graph.DeclareNewNode();
            }

            graph.AddUndirectedConnection(new Connection(1, 5, 2));
            graph.AddUndirectedConnection(new Connection(5, 4, 1));
            graph.AddUndirectedConnection(new Connection(1, 4, 4));
            graph.AddUndirectedConnection(new Connection(3, 5, 4));
            graph.AddUndirectedConnection(new Connection(3, 4, 1));
            graph.AddUndirectedConnection(new Connection(3, 2, 2));
            graph.AddUndirectedConnection(new Connection(2, 4, 4));
            graph.AddUndirectedConnection(new Connection(2, 6, 1));
            graph.AddUndirectedConnection(new Connection(4, 6, 6));
            graph.AddUndirectedConnection(new Connection(6, 7, 2));
            graph.AddUndirectedConnection(new Connection(3, 7, 6));

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
