using KodivaFooFuu.Core.Interfaces;

namespace KodivaFooFuu.Core.Services;

public class SystemInfoProvider : ISystemInfoProvider
{
    private readonly Version _runtimeVersion = Environment.Version;
    private const int MinMajorVersion = 10;

    public bool IsCompatible => _runtimeVersion.Major >= MinMajorVersion;

    public string GetRuntimeStatus() => IsCompatible
        ? $".NET {_runtimeVersion} runtime is installed."
        : $".NET 10+ runtime is not installed (Current: {_runtimeVersion}).";

    public string GetAppHeader()
    {
        var assemblyVersion = typeof(SystemInfoProvider).Assembly.GetName().Version;
        return $"(c) PeterPucik.SK {DateTime.Now.Year} - KodivaFooFuuApp v{assemblyVersion}";
    }
}
