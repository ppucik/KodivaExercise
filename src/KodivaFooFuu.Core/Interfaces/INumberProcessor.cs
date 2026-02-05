namespace KodivaFooFuu.Core.Interfaces;

/// <summary>
/// Defines a contract for generating a sequence of numbers within a specified range.
/// </summary>
public interface INumberProcessor
{
    /// <summary>
    /// Generates output for each integer in the specified range, applying all matching rules to each value.
    /// </summary>
    /// <param name="start">The first integer in the range to process. Must be less than or equal to <paramref name="end"/>.</param>
    /// <param name="end">The last integer in the range to process.</param>
    void Generate(int start, int end);
}
