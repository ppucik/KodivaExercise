using KodivaFooFuu.Core.Interfaces;

namespace KodivaFooFuu.Core.Rules;

/// <summary>
/// Represents a rule that determines whether a given integer matches a specified condition and provides an associated output string.
/// </summary>
/// <param name="condition">A delegate that defines the condition to evaluate for each integer.</param>
/// <param name="output">The output string associated with the rule.</param>
/// <example>
/// new LambdaRule(n => n > 50 && n < 60, "nifty")
/// </example>
public class LambdaRule(Func<int, bool> condition, string output) : IRule
{
    public bool Matches(int number) => condition(number);
    public string Output => output;
}
