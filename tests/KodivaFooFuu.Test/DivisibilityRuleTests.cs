using FluentAssertions;
using KodivaFooFuu.Core.Interfaces;
using KodivaFooFuu.Core.Rules;
using KodivaFooFuu.Core.Services;
using Moq;

/*
 * Testovanie samotných pravidiel (Unit Testy)
 * Najprv testujeme, či naše pravidlá pre deliteľnosť fungujú správne izolovane.
 */

namespace KodivaFooFuu.Test;

public class DivisibilityRuleTests
{
    [Theory]
    [InlineData(2, true)]  // 2 je deliteľné 2
    [InlineData(4, true)]  // 4 je deliteľné 2
    [InlineData(3, false)] // 3 nie je deliteľné 2
    public void DivisibilityRule_Should_ValidateCorrectNumbers(int input, bool expected)
    {
        // Arrange
        var rule = new DivisibilityRule(2, "foo");

        // Act
        var result = rule.Matches(input);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void DivisibilityRule_Should_UseRulesFromConfigObject()
    {
        // Arrange: Ručne vytvoríme objekt nastavení (žiadny JSON súbor)
        var fakeRules = new List<IRule> { new DivisibilityRule(10, "ten") };
        var mockSink = new Mock<IOutputSink>();
        var sut = new FooFuuGenerator(fakeRules, mockSink.Object);

        // Act
        sut.Generate(10, 10);

        // Assert
        mockSink.Verify(s => s.Write("ten"), Times.Once);
    }
}
