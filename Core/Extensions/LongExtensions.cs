namespace Core.Extensions;

public static class LongExtensions
{
    public static long Modulo(this long x, long m)
    {
        return (x % m + m) % m;
    }
}