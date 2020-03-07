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
using GraphSearchApp.Helpers;
using GraphSearchApp.Helpers.Interfaces;
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
        private readonly IMainWindowHelper _helper;

        public MainWindow()
        {
            _helper = new MainWindowHelper(this);
            InitializeComponent();
        }

        private void UploadDataButton_Click() => 
            _helper.UploadData();

        private void LeastCitiesAlgorithm_Checked() => 
            _helper.ChooseLeastCities();

        private void ShortestRouteAlgorithm_Checked() =>
            _helper.ChooseShortestRoutes();

        private void ShortestRouteAlgorithmWithGeolocation_Checked() =>
            _helper.ChooseShortestRoutesGeolocation();
    }
}
