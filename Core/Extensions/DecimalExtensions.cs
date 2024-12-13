namespace Core.Extensions;

public static class DecimalExtensions
{
    public static int ToInt(this decimal value) => (int) value;
    public static long ToLong(this decimal value) => (long) value;
}