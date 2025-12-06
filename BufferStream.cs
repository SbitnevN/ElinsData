using System.Text;

namespace ElinsDataParser;

public class BufferStream : StreamReader
{
    public BufferStream(string path) : base(path)
    {
    }

    public BufferStream(string path, Encoding encoding) : base(path, encoding)
    {
    }

    public int ReadLine(Span<char> buffer)
    {
        buffer.Clear();

        int count = 0;
        while (Peek() > 0) // Бутылочная боль и узкая голова
        {
            char value = (char)Read();

            buffer[count++] = value;

            if (value == '\n')
                break;
        }

        return count;
    }

    public async ValueTask<int> ReadLineAsync(Memory<char> buffer)
    {
        buffer.Span.Clear();

        int count = 0;
        Memory<char> memory = new char[1];
        while (await ReadAsync(memory) > 0) // Бутылочная боль и узкая голова
        {
            buffer.Span[count++] = memory.Span[0];
            if (memory.Span[0] == '\n')
                break;
        }

        return count;
    }
}
