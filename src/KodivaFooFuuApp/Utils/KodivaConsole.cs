namespace KodivaFooFuuApp.Utils;

/// <summary>
/// Provides utility methods for writing informational and exit messages to the console.
/// </summary>
public static class KodivaConsole
{
    public static void WriteLineInfo(string message, ConsoleColor? color = null)
    {
        if (color.HasValue)
            Console.ForegroundColor = color.Value;

        System.Console.WriteLine(message);

        if (color.HasValue)
            Console.ResetColor();
    }

    public static void WriteLineExit(string message, bool error = false)
    {
        if (error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine(message);
        }

        Environment.Exit(0);
    }

    public static void ReadLineExit()
    {
        Console.ReadLine();
    }
}
