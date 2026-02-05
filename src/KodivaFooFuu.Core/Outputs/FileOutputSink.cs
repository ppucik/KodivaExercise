using KodivaFooFuu.Core.Interfaces;

namespace KodivaFooFuu.Core.Infrastructure;

/// <summary>
/// Provides an output sink that writes messages to a text file.
/// </summary>
/// <param name="filePath">The path to the file where messages will be written. If the file exists, it will be overwritten.</param>
public class FileOutputSink(string filePath) : IOutputSink, IDisposable
{
    private readonly StreamWriter _writer = new(filePath, append: false)
    {
        AutoFlush = true
    };

    public void Write(string message)
    {
        _writer.WriteLine(message);
    }

    public void Dispose()
    {
        _writer.Dispose();
        GC.SuppressFinalize(this);
    }
}
