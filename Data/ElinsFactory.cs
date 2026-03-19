using ElinsData.Reader;

namespace ElinsData.Data;

public static class ElinsFactory
{
    private static readonly IElinsReader _elinsParser = new ElinsReader();
    private static readonly IElinsReader _textParser = new Reader.TextReader();

    public static ElinsRecord Create(string path, Filter filter = Filter.Impedance)
    {
        return Read(path).FillCapacitance();
    }

    public static async Task<ElinsRecord> CreateAsync(string path, Filter filter = Filter.Impedance)
    {
        return (await ReadAsync(path)).FillCapacitance();
    }

    private static ElinsRecord Read(string path, Filter filter = Filter.All)
    {
        string extension = Path.GetExtension(path);
        if (extension == ".txt")
            return _textParser.Read(path, filter);

        if (extension == ".edf")
            return _elinsParser.Read(path, filter);

        throw new Exception("Неизвестный тип файла");
    }

    private static Task<ElinsRecord> ReadAsync(string path, Filter filter = Filter.All)
    {
        string extension = Path.GetExtension(path);
        if (extension == ".txt")
            return _textParser.ReadAsync(path, filter);

        if (extension == ".edf")
            return _elinsParser.ReadAsync(path, filter);

        throw new Exception("Неизвестный тип файла");
    }
}
