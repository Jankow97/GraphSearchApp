using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GraphSearchApp.Algorithms;
using GraphSearchApp.Algorithms.Interfaces;
using GraphSearchApp.Dtos;
using GraphSearchApp.Helpers.Interfaces;
using GraphSearchApp.IO;
using GraphSearchApp.IO.Interfaces;
using Microsoft.Win32;

namespace GraphSearchApp.Helpers
{
    class MainWindowHelper : IMainWindowHelper
    {
        private readonly MainWindow _mainWindow;
        private IGraphSearchExecute _graphSearchExecute = new LeastCitiesAlgorithm();
        private ITextToGraph _textToGraph = new ReadStandardData();

        public MainWindowHelper(MainWindow mw)
        {
            _mainWindow = mw;
        }

        public async void UploadData()
        {
            string data = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                data = File.ReadAllText(openFileDialog.FileName);
                //_mainWindow.UploadedFileContentTb.Text = data;
            }
            try
            {
                ReadData readData = _textToGraph.ReadData(data);
                if (readData.Graph == null || readData.GraphSearchOptions == null)
                {
                    throw new Exception();
                }

                var stopWatch = Stopwatch.StartNew();
                var ar = _graphSearchExecute.ExecuteSearch(readData.Graph, readData.GraphSearchOptions);
                stopWatch.Stop();
                var stringBuilder = new StringBuilder();
                foreach (var city in ar.CitiesTraverseOrder)
                {
                    stringBuilder.Append(city + " ");
                }
                string resultText = (ar.CitiesTraverseOrder.Count - 2) + " " + ar.ShortestRoute + Environment.NewLine + stringBuilder.ToString();
                var timeElapsed = stopWatch.Elapsed;
                resultText += Environment.NewLine + Environment.NewLine + timeElapsed + Environment.NewLine + Environment.NewLine;
                _mainWindow.ResultContentTb.Text = resultText;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        public void ChooseLeastCities()
        {
            _textToGraph = new ReadStandardData();
            _graphSearchExecute = new LeastCitiesAlgorithm();
        }

        public void ChooseShortestRoutes()
        {
            _graphSearchExecute = new ShortestRoutesAlgorithm();
        }

        public void ChooseShortestRoutesGeolocation()
        {
            _textToGraph = new ReadGeolocationData();
            _graphSearchExecute = new ShortestRoutesAlgorithmWithGeolocation();
        }
    }
}
