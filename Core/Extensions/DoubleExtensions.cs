namespace Core.Extensions;

public static class DoubleExtensions
{
    public static int ToInt(this double value)
    {
        return (int)value;
    }

    public static long ToLong(this double value)
    {
        return (long)value;
    }
}