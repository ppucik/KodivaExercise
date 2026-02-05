using KodivaFooFuu.Core.Interfaces;

namespace KodivaFooFuu.Core.Rules;

/// <summary>
/// Represents a rule that matches numbers containing a specific digit and provides an associated output string.
/// </summary>
/// <param name="digit">The digit to check for within the number.</param>
/// <param name="output">The output string to return when the rule matches.</param>
public class ContainsDigitRule(int digit, string output) : IRule
{
    private readonly string _digitChar = digit.ToString();

    public bool Matches(int number) => number.ToString().Contains(_digitChar);

    public string Output => output;
}
