namespace Core.Extensions;

public static class DecimalExtensions
{
    public static int ToInt(this decimal value)
    {
        return (int)value;
    }

    public static long ToLong(this decimal value)
    {
        return (long)value;
    }
}