using FluentAssertions;
using KodivaFooFuu.Core.Interfaces;
using KodivaFooFuu.Core.Rules;
using KodivaFooFuu.Core.Services;
using Moq;

/*
 * Testovanie generátora (Integration Testy)
 * Tu testujeme, či generátor správne kombinuje pravidlá a posiela očakávané výstupy do IOutputSink.
 */

namespace KodivaFooFuu.Test;

public class FooFuuGeneratorTests
{
    [Fact]
    public void Generate_Should_SendCorrectSequenceToOutput()
    {
        // Arrange
        var mockSink = new Mock<IOutputSink>();
        var capturedOutputs = new List<string>();

        // Zakaždým, keď generátor zavolá Write, uložíme si správu do zoznamu
        mockSink.Setup(s => s.Write(It.IsAny<string>()))
                .Callback<string>(capturedOutputs.Add);

        var rules = new List<IRule>
        {
            new DivisibilityRule(2, "foo"),
            new DivisibilityRule(4, "fuu")
        };

        INumberProcessor generator = new FooFuuGenerator(rules, mockSink.Object);

        // Act
        generator.Generate(1, 5);

        // Assert
        capturedOutputs.Should().HaveCount(5);
        capturedOutputs[0].Should().Be("1");
        capturedOutputs[1].Should().Be("foo");
        capturedOutputs[2].Should().Be("3");
        capturedOutputs[3].Should().Be("foofuu"); // Číslo 4 je deliteľné 2 aj 4
        capturedOutputs[4].Should().Be("5");
    }
}
