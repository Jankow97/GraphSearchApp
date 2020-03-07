using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphSearchApp.Helpers.Interfaces
{
    interface IMainWindowHelper
    {
        void UploadData();

        void ChooseLeastCities();
        void ChooseShortestRoutes();
        void ChooseShortestRoutesGeolocation();
    }
}
