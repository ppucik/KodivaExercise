using FluentAssertions;
using KodivaFooFuu.Core.Configuration;
using KodivaFooFuu.Core.Extensions;
using KodivaFooFuu.Core.Interfaces;
using KodivaFooFuu.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
 * Test konfigurácie (DI Smoke Test)
 * Tu testujeme, či IoC kontajner správne rieši závislosti a poskytuje inštancie.
 */

namespace KodivaFooFuu.Test;

public class DependencyInjectionTests
{
    [Fact]
    public void IoCContainer_Should_Resolve_INumberProcessor_As_FooFuuGenerator()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddFooFuuServices(); // Použijeme rovnakú konfiguráciu ako v appke
        var provider = services.BuildServiceProvider();

        // Act
        var processor = provider.GetService<INumberProcessor>();

        // Assert
        processor.Should().NotBeNull();
        processor.Should().BeOfType<FooFuuGenerator>();
    }

    [Fact]
    public void IoCContainer_Should_Resolve_All_Dependencies_For_Generator()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddFooFuuServices();
        var provider = services.BuildServiceProvider();

        // Act & Assert
        // Tento riadok vyhodí výnimku, ak v kontajneri chýba napr. IOutputSink alebo pravidlá
        var action = () => provider.GetRequiredService<INumberProcessor>();

        action.Should().NotThrow();
    }

    [Fact]
    public void AddFooFuuServices_Should_Correctly_Map_Configuration_To_Rules()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string?>
        {
            {$"{KodivaFooFuuSettings.SECTION_NAME}:Rules:0:Divisor", "10"},
            {$"{KodivaFooFuuSettings.SECTION_NAME}:Rules:0:Output", "ten"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        // Musíme vytvoriť simulované CLI options, inak DI zlyhá pri registrácii IOutputSink
        var fakeCliOptions = new KodivaOptions
        {
            Start = 1,
            End = 10,
            OutputDevice = OutputDeviceType.Console
        };

        var services = new ServiceCollection();

        // Act
        services.AddFooFuuServices(configuration, fakeCliOptions);
        var provider = services.BuildServiceProvider();

        // Assert
        var processor = provider.GetRequiredService<INumberProcessor>();
        var rules = provider.GetRequiredService<IEnumerable<IRule>>().ToList();

        processor.Should().NotBeNull();
        rules.Should().HaveCount(1);
        rules[0].Output.Should().Be("ten");
    }
}
