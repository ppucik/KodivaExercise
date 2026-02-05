using KodivaFooFuu.Core.Interfaces;

namespace KodivaFooFuu.Core.Infrastructure;

/// <summary>
/// Provides an output sink that writes messages to the memory.
/// </summary>
public class MemoryOutputSink : IOutputSink
{
    private readonly List<string> _results = new();

    public void Write(string message) => _results.Add(message);

    public IReadOnlyList<string> GetResults() => _results;
}
