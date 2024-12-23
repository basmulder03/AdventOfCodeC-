using InvalidOperationException = System.InvalidOperationException;

namespace Core.Extensions;

public static class IntegerExtensions
{
    public static int ButNotLessThan(this int value, int minValue)
    {
        return value < minValue ? minValue : value;
    }

    public static int ButNotLessThanOrEqualTo(this int value, int minValue)
    {
        return value <= minValue ? minValue + 1 : value;
    }

    public static int ButNotGreaterThan(this int value, int maxValue)
    {
        return value > maxValue ? maxValue : value;
    }

    public static int ButNotGreaterThanOrEqualTo(this int value, int maxValue)
    {
        return value >= maxValue ? maxValue - 1 : value;
    }

    public static char ToChar(this int value)
    {
        if (value is < 0 or > 9) throw new InvalidOperationException();
        return value.ToString().ToCharArray()[0];
    }

    /// <summary>
    /// This extension method is used to calculate the modulo of a number.
    /// This will use the floor division to calculate the modulo, which is used by most programming languages.
    /// </summary>
    /// <param name="x">The left part of the modulo operation</param>
    /// <param name="m">The right part of the module operation</param>
    /// <returns>The modulo as created by other (non C-based) programming languages.</returns>
    public static int Modulo(this int x, int m)
    {
        return (x % m + m) % m;
    }
}