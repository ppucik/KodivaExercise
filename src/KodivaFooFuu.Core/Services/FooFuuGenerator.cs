namespace KodivaFooFuu.Core.Services;

using KodivaFooFuu.Core.Interfaces;
using System.Data;

/// <summary>
/// Generates output for a range of numbers using a set of rules and writes the results to the specified output sink.
/// </summary>
public class FooFuuGenerator : INumberProcessor
{
    private readonly IReadOnlyList<IRule> _rules;
    private readonly IOutputSink _outputSink;

    /// <summary>
    /// Initializes a new instance of the FooFuuGenerator class with the specified rules and output sink.
    /// </summary>
    /// <param name="rules">The collection of rules to be used by the generator. Cannot be null.</param>
    /// <param name="outputSink">The output sink that receives the generated results. Cannot be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if rules is null.</exception>
    public FooFuuGenerator(IEnumerable<IRule> rules, IOutputSink outputSink)
    {
        _rules = new List<IRule>(rules ?? throw new ArgumentNullException(nameof(rules)));
        _outputSink = outputSink ?? throw new ArgumentNullException(nameof(outputSink));
    }

    /// <summary>
    /// Generates output for each integer in the specified range, applying all matching rules to each value.
    /// </summary>
    /// <param name="start">The first integer in the range to process. Must be less than or equal to <paramref name="end"/>.</param>
    /// <param name="end">The last integer in the range to process.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="start"/> is greater than <paramref name="end"/>.</exception>
    public void Generate(int start, int end)
    {
        if (start > end) throw new ArgumentException("Start must be <= end.", nameof(start));

        for (int i = start; i <= end; i++)
        {
            var matchResults = _rules
                .Where(r => r.Matches(i))
                .Select(r => r.Output);

            var finalOutput = string.Concat(matchResults);

            _outputSink.Write(string.IsNullOrEmpty(finalOutput)
                ? i.ToString()
                : finalOutput);
        }
    }
}
