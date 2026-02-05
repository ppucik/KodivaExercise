using KodivaFooFuu.Core.Configuration;
using KodivaFooFuu.Core.Infrastructure;
using KodivaFooFuu.Core.Interfaces;
using KodivaFooFuu.Core.Rules;
using KodivaFooFuu.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace KodivaFooFuu.Core.Extensions;

/// <summary>
/// Provides extension methods for registering FooFuu-related services with the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the FooFuu core services and related dependencies to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to which the FooFuu services will be added. Cannot be null.</param>
    /// <returns>The same instance of <see cref="IServiceCollection"/> that was provided, to support method and outpus chaining.</returns>
    public static IServiceCollection AddFooFuuServices(this IServiceCollection services)
    {
        services.AddSingleton<ISystemInfoProvider, SystemInfoProvider>();

        services.AddSingleton<IEnumerable<IRule>>(_ => [
            new DivisibilityRule(2, "foo"),
            new DivisibilityRule(4, "fuu"),
            new ContainsDigitRule(7, "lucky") // Naše nové pravidlo
            // new LambdaRule(x => x == 7, "lucky") // Alternatívne pravidlo
        ]);

        //.AddSingleton<IOutputSink>(_ => new FileOutputSink("vystup.txt"))
        services.AddSingleton<IOutputSink, ConsoleOutputSink>();
        services.AddSingleton<INumberProcessor, FooFuuGenerator>();

        return services;
    }

    /// <summary>
    /// Adds and configures FooFuu-related services, options, and dependencies to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to which the FooFuu services will be added.</param>
    /// <param name="configuration">The application configuration used to bind FooFuu settings.</param>
    /// <param name="options">The options object specifying additional FooFuu configuration, such as output device selection.</param>
    /// <returns>The same service collection instance, enabling method chaining.</returns>
    public static IServiceCollection AddFooFuuServices(this IServiceCollection services,
        IConfiguration configuration,
        KodivaOptions options)
    {
        // Registrácia poskytovateľa systémových informácií
        services.AddSingleton<ISystemInfoProvider, SystemInfoProvider>();

        // Nastavenie Options s validáciou
        services.AddOptions<KodivaFooFuuSettings>()
            .Bind(configuration.GetSection(KodivaFooFuuSettings.SECTION_NAME))
            .ValidateDataAnnotations()  // Aktivuje atribúty [Required], [Range] atď.
            .ValidateOnStart();         // Aplikácia spadne hneď pri štarte, nie až pri použití

        // Dynamická registrácia pravidiel z nastaveni
        services.AddSingleton<IEnumerable<IRule>>(sp =>
        {
            // Tu získame validované nastavenia cez IOptions
            var settings = sp.GetRequiredService<IOptions<KodivaFooFuuSettings>>().Value;
            return settings.Rules.Select(r => new DivisibilityRule(r.Divisor, r.Output));
        });

        // Dynamická registrácia výstupného zariadenia
        services.AddSingleton<IOutputSink>(sp =>
        {
            return options.OutputDevice switch
            {
                OutputDeviceType.File => new FileOutputSink("output.txt"),
                OutputDeviceType.Console => new ConsoleOutputSink(),
                _ => throw new ArgumentOutOfRangeException(nameof(options.OutputDevice), "Nepodporovaný typ výstupu.")
            };
        });

        // Registrácia procesora
        services.AddScoped<INumberProcessor, FooFuuGenerator>();

        return services;
    }
}
