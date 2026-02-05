namespace KodivaFooFuu.Core.Rules;

using KodivaFooFuu.Core.Interfaces;

/// <summary>
/// Represents a rule that determines whether a number is divisible by a specified divisor and associates an output string with the rule.
/// </summary>
public sealed class DivisibilityRule : IRule
{
    /// <summary>
    /// Gets the divisor used in division operations.
    /// </summary>
    public int Divisor { get; }

    public string Output { get; }

    public DivisibilityRule(int divisor, string output)
    {
        if (divisor == 0) throw new ArgumentException("Divisor must be non-zero.", nameof(divisor));

        Divisor = divisor;
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }

    public bool Matches(int number) => number % Divisor == 0;
}
