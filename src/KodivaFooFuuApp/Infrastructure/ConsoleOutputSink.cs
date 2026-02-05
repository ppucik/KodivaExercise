using KodivaFooFuu.Core.Interfaces;

namespace KodivaFooFuuApp.Infrastructure;

/// <summary>
/// Provides an output sink that writes messages to the console.
/// </summary>
public class ConsoleOutputSink : IOutputSink
{
    public void Write(string message) => Console.WriteLine(message);
}
