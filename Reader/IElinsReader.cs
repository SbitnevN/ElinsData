using ElinsData.Data;

namespace ElinsData.Reader;

public interface IElinsReader
{
    public ElinsRecord Read(string filePath, Filter filter = Filter.All, Name nameFrom = Name.FileName);
    public Task<ElinsRecord> ReadAsync(string filePath, Filter filter = Filter.All, Name nameFrom = Name.FileName);
}
