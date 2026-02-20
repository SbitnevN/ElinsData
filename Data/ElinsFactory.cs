using ElinsData.Extensions;
using ElinsData.Reader;

namespace ElinsData.Data;

public static class ElinsFactory
{
    private static readonly IElinsReader _elinsParser = new ElinsReader();
    private static readonly IElinsReader _textParser = new Reader.TextReader();

    public static ElinsRecord Create(string path)
    {
        return Read(path).FillCapacitance();
    }

    public static async Task<ElinsRecord> CreateAsync(string path)
    {
        return (await ReadAsync(path)).FillCapacitance();
    }

    private static ElinsRecord Read(string path)
    {
        string extension = Path.GetExtension(path);
        if (extension == ".txt")
            return _textParser.Read(path);

        if (extension == ".edf")
            return _elinsParser.Read(path);

        throw new Exception("Неизвестный тип файла");
    }

    private static Task<ElinsRecord> ReadAsync(string path)
    {
        string extension = Path.GetExtension(path);
        if (extension == ".txt")
            return _textParser.ReadAsync(path);

        if (extension == ".edf")
            return _elinsParser.ReadAsync(path);

        throw new Exception("Неизвестный тип файла");
    }
}
