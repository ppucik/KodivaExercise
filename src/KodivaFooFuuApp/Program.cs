using CommandLine;
using KodivaFooFuu.Core.Configuration;
using KodivaFooFuu.Core.Extensions;
using KodivaFooFuu.Core.Interfaces;
using KodivaFooFuuApp.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// 1. Príprava buildera pre načítanie konfiguráciu (appsettings.json)
var builder = Host.CreateApplicationBuilder(args);
var config = builder.Configuration;

// 2. Parsovanie CLI vstupnych argumentov
var parser = new Parser(with => with.HelpWriter = Console.Out);
parser.ParseArguments<KodivaOptions>(args)
    .WithParsed(options =>
    {
        // 3. Validácia CLI argumentov
        if (!options.IsValid(out var error))
        {
            // Neplatné vstupné argumenty
            KodivaConsole.WriteLineExit(error!, true);
            return;
        }

        // 4. Nastavenie DI (Dependency Injection) kontajnera
        var serviceProvider = new ServiceCollection()
            .AddFooFuuServices(config, options)
            .BuildServiceProvider();

        // 5. Diagnostika
        var sysInfo = serviceProvider.GetRequiredService<ISystemInfoProvider>();
        KodivaConsole.WriteLineInfo(sysInfo.GetAppHeader());
        KodivaConsole.WriteLineInfo(
            sysInfo.GetRuntimeStatus(),
            sysInfo.IsCompatible ? ConsoleColor.Green : ConsoleColor.Red
        );

        if (!sysInfo.IsCompatible) return;

        // 6. Spustenie spracovania
        var processor = serviceProvider.GetRequiredService<INumberProcessor>();
        processor.Generate(options.Start, options.End);

        KodivaConsole.WriteLineInfo("Koniec spracovania", ConsoleColor.Green);
    })
    .WithNotParsed(errors =>
    {
        // 7. Neplatné argumenty
        if (errors.Any(e =>
            e.Tag != ErrorType.HelpRequestedError &&
            e.Tag != ErrorType.VersionRequestedError))
        {
            KodivaConsole.WriteLineInfo("Použitie: KodivaFooFuuApp.exe -s [Start] -e [End] -o [OutputDevice]");
        }
    });

