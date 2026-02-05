using CommandLine;

namespace KodivaFooFuuApp.Configuration;

/// <summary>
/// Represents configuration options for specifying a numeric range.
/// </summary>
public class KodivaOptions
{
    /// <summary>
    /// Gets or sets the starting value for the operation.
    /// </summary>
    [Option('s', "start", Required = false, Default = 1, HelpText = "Začiatok intervalu")]
    public int Start { get; init; } = 1;

    /// <summary>
    /// Gets or sets the end value for the operation or range.
    /// </summary>
    [Option('e', "end", Required = false, Default = 100, HelpText = "Koniec intervalu")]
    public int End { get; init; } = 100;

    /// <summary>
    /// Gets the output device type.
    /// </summary>
    [Option('o', "output", Required = false, Default = OutputDeviceType.Console, HelpText = "Typ výstupu.")]
    public OutputDeviceType OutputDevice { get; init; } = OutputDeviceType.Console;

    /// <summary>
    /// Determines whether the current range is valid and provides an error message if it is not.
    /// </summary>
    /// <param name="error">When this method returns, contains an error message.</param>
    /// <returns>true if the range is valid; otherwise, false.</returns>
    public bool IsValid(out string? error)
    {
        if (Start <= 0)
        {
            error = "Začiatok musí byť kladné číslo väčšie ako nula.";
            return false;
        }

        if (End < Start)
        {
            error = $"Koniec ({End}) nesmie byť menší ako začiatok ({Start}).";
            return false;
        }

        error = null;
        return true;
    }
}
