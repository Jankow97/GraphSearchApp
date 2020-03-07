using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSearchApp.Dtos
{
    public class Connection
    {
        public int CityA { get; set; }
        public int CityB { get; set; }
        public int Distance { get; set; }

        public Connection(int cityA, int cityB, int distance)
        {
            CityA = cityA;
            CityB = cityB;
            Distance = distance;
        }
    }
}
