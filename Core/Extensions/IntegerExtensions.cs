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
}