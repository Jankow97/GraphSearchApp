using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSearchApp.Dtos
{
    public class AlgorithmResult : IEquatable<AlgorithmResult>
    {
        public AlgorithmResult()
        {
            ShortestRoute = 0;
            CitiesTraverseOrder = new List<int>();
        }
        public int ShortestRoute { get; set; }
        public List<int> CitiesTraverseOrder { get; set; }

        public bool Equals(AlgorithmResult other)
        {
            if (ReferenceEquals(null, other)) return false;
            bool isEqual = true;
            for (int i = 0; i < CitiesTraverseOrder.Count; i++)
            {
                if (CitiesTraverseOrder[i] != other.CitiesTraverseOrder?[i])
                {
                    isEqual = false;
                }
            }
            return ShortestRoute == other.ShortestRoute && isEqual;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AlgorithmResult) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ShortestRoute * 397) ^ (CitiesTraverseOrder != null ? CitiesTraverseOrder.GetHashCode() : 0);
            }
        }
    }
}
