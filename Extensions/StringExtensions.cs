namespace ElinsData.Extensions;

internal static class StringExtensions
{
    public static bool IsEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    public static bool IsNotEmpty(this string value)
    {
        return !IsEmpty(value);
    }
}
