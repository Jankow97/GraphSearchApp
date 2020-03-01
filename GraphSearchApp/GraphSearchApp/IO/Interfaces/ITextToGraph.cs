using GraphSearchApp.Dtos;

namespace GraphSearchApp.IO.Interfaces
{
    public interface ITextToGraph
    {
        Graph ReadData(string dataToRead);
    }
}
