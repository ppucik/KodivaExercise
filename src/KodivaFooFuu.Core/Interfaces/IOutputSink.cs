namespace KodivaFooFuu.Core.Interfaces;

/// <summary>
/// Abstraction for writing output so the printer is testable and output-target-agnostic.
/// </summary>
public interface IOutputSink
{
    /// <summary>
    /// Writes the specified message to the output destination.
    /// </summary>
    /// <param name="message">The message to write.</param>
    void Write(string message);
}
