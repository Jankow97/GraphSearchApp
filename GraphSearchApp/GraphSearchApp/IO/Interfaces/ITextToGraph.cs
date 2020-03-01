using GraphSearchApp.Dtos;

namespace GraphSearchApp.IO.Interfaces
{
    interface ITextToGraph
    {
        Graph ReadData(string dataToRead);
    }
}
