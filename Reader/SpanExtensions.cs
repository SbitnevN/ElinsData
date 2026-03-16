using System.Globalization;

namespace ElinsData.Reader;

internal static class SpanExtensions
{
    public static ReadOnlySpan<char> ReadToken(this ref ReadOnlySpan<char> span)
    {
        int spaceIndex = span.IndexOf(' ');
        if (spaceIndex < 0)
        {
            ReadOnlySpan<char> token = span;
            return token;
        }

        ReadOnlySpan<char> result = span.Slice(0, spaceIndex);
        span = span.Slice(spaceIndex + 1).TrimStart();
        return result;
    }

    public static double ReadDouble(this ref ReadOnlySpan<char> span)
    {
        ReadOnlySpan<char> token = span.ReadToken();
        return double.Parse(token, CultureInfo.InvariantCulture);
    }

    public static int ReadInt(this ref ReadOnlySpan<char> span)
    {
        ReadOnlySpan<char> token = span.ReadToken();
        return int.Parse(token, CultureInfo.InvariantCulture);
    }

    public static ReadOnlySpan<char> ReadToken(this ref Span<char> span)
    {
        int spaceIndex = span.IndexOf(' ');
        if (spaceIndex < 0)
        {
            ReadOnlySpan<char> token = span;
            return token;
        }

        ReadOnlySpan<char> result = span.Slice(0, spaceIndex);
        span = span.Slice(spaceIndex + 1).TrimStart();
        return result;
    }

    public static double ReadDouble(this ref Span<char> span)
    {
        ReadOnlySpan<char> token = span.ReadToken();
        return double.Parse(token, CultureInfo.InvariantCulture);
    }

    public static int ReadInt(this ref Span<char> span)
    {
        ReadOnlySpan<char> token = span.ReadToken();
        return int.Parse(token, CultureInfo.InvariantCulture);
    }

    public static bool IsEmpty(this Span<char> span)
    {
        return span.All(x => x == '\0');
    }

    public static bool All<T>(this Span<T> span, Func<T, bool> predicate)
    {
        foreach (T value in span)
        {
            if (!predicate(value))
                return false;
        }

        return true;
    }
}
