namespace KodivaFooFuu.Core.Interfaces;

/// <summary>
/// Defines a contract for retrieving system and application environment information.
/// </summary>
public interface ISystemInfoProvider
{
    string GetRuntimeStatus();
    bool IsCompatible { get; }
    string GetAppHeader();
}
