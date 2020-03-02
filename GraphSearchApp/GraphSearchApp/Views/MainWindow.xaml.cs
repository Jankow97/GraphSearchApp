using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GraphSearchApp.Algorithms;
using GraphSearchApp.Algorithms.Interfaces;
using GraphSearchApp.Dtos;
using GraphSearchApp.IO;
using GraphSearchApp.IO.Interfaces;
using Microsoft.Win32;

namespace GraphSearchApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string data = "";
        private IGraphSearchExecute graphSearchExecute = new LeastCitiesAlgorithm();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UploadDataButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                data = File.ReadAllText(openFileDialog.FileName);
                UploadedFileContentTb.Text = data;
            }
            //try
            //{
                ITextToGraph textToGraph = new ReadStandardData();
                ReadData readData = textToGraph.ReadData(data);
                var ar = graphSearchExecute.ExecuteSearch(readData.Graph, readData.GraphSearchOptions);
                string cities = "";
                foreach (var city in ar.CitiesTraverseOrder)
                {
                    cities += city + " ";
                }
                string resultText = ar.CitiesTraverseOrder.Count + " " + ar.ShortestRoute + Environment.NewLine + cities;
                ResultContentTb.Text = resultText;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void LeastCitiesAlgorithm_Checked(object sender, RoutedEventArgs e)
        {
            UncheckToggleButton(ShortestRouteTb);
            UncheckToggleButton(ShortestRouteWithGeolocationTb);
        }

        private void ShortestRouteAlgorithm_Checked(object sender, RoutedEventArgs e)
        {
            UncheckToggleButton(LeastCitiesTb);
            UncheckToggleButton(ShortestRouteWithGeolocationTb);
        }

        private void ShortestRouteAlgorithmWithGeolocation_Checked(object sender, RoutedEventArgs e)
        {
            UncheckToggleButton(LeastCitiesTb);
            UncheckToggleButton(ShortestRouteTb);
        }

        private void UncheckToggleButton(ToggleButton tb)
        {
            if (tb?.IsChecked ?? false)
            {
                tb.IsChecked = false;
            }
        }
    }
}
