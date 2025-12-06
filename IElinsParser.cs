using ElinsDataParser.Data;

namespace ElinsDataParser;

public interface IElinsParser
{
    public ElinsData Parse(string filePath, Filter filter = Filter.All, Name nameFrom = Name.FileName);
    public Task<ElinsData> ParseAsync(string filePath, Filter filter = Filter.All, Name nameFrom = Name.FileName);
}
