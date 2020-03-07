﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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

        public MainWindowHelper(MainWindow mw)
        {
            _mainWindow = mw;
        }

        public void UploadData()
        {
            string data = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                data = File.ReadAllText(openFileDialog.FileName);
                _mainWindow.UploadedFileContentTb.Text = data;
            }
            try
            {
                ITextToGraph textToGraph = new ReadStandardData();
                ReadData readData = textToGraph.ReadData(data);
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
            _graphSearchExecute = new LeastCitiesAlgorithm();
        }

        public void ChooseShortestRoutes()
        {
            throw new NotImplementedException();
        }

        public void ChooseShortestRoutesGeolocation()
        {
            throw new NotImplementedException();
        }
    }
}
