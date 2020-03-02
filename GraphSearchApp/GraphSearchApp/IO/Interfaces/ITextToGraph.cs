using GraphSearchApp.Dtos;

namespace GraphSearchApp.IO.Interfaces
{
    public interface ITextToGraph
    {
        ReadData ReadData(string dataToRead);
    }
}
