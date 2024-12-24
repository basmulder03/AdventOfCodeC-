namespace Core.Interfaces;

/// <summary>
/// Defines the basic structure for a day in the Advent of Code.
/// </summary>
public class BaseDay
{
    /// <summary>
    /// Defines a method to solve the first part of the day,
    /// if the day has a first part.
    /// <remarks>
    /// This method should be overridden in the day class.
    /// When the result is numeric.
    /// </remarks>
    /// </summary>
    /// <param name="input">A string containing the input of that day.</param>
    /// <returns>A <see cref="long" /> containing the answer for Part1 of the specified day.</returns>
    /// <exception cref="NotImplementedException">
    /// Throws a <see cref="NotImplementedException" /> when not the method is called
    /// but not overriden.
    /// </exception>
    public virtual long Part1(string input)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Defines a method to solve the second part of the day,
    /// if the day has a second part.
    /// <remarks>
    /// This method should be overridden in the day class.
    /// When the result is numeric.
    /// </remarks>
    /// </summary>
    /// <param name="input">A string containing the input of that day.</param>
    /// <returns>A <see cref="long" /> containing the answer for Part2 of the specified day.</returns>
    /// <exception cref="NotImplementedException">
    /// Throws a <see cref="NotImplementedException" /> when not the method is called
    /// but not overriden.
    /// </exception>
    public virtual long Part2(string input)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns by default the result of Part1 as a string.
    /// This method can be overriden in the case that the result of a part of a specific day is not numeric.
    /// </summary>
    /// <param name="input">A string containing the input of that day.</param>
    /// <returns>The result of the first part of the day.</returns>
    public virtual string Part1String(string input)
    {
        return Part1(input).ToString();
    }

    /// <summary>
    /// Returns by default the result of Part2 as a string.
    /// This method can be overriden in the case that the result of a part of a specific day is not numeric.
    /// </summary>
    /// <param name="input">A string containing the input of that day.</param>
    /// <returns>The result of the second part of the day.</returns>
    public virtual string Part2String(string input)
    {
        return Part2(input).ToString();
    }
}