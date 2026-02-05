using FluentAssertions;
using KodivaFooFuu.Core.Configuration;

namespace KodivaFooFuu.Test;

public class KodivaFooFuuSettingsTests
{
    [Fact]
    public void RuleDetail_Should_Fail_When_Divisor_Is_Zero_Or_Negative()
    {
        // Arrange
        var rule = new RuleDetail { Divisor = 0, Output = "test" };

        // Act
        var results = ValidationHelper.ValidateModel(rule);

        // Assert
        results.Should().Contain(r => r.ErrorMessage != null && r.ErrorMessage.Contains("kladné číslo"));
    }

    [Fact]
    public void RuleDetail_Should_Fail_When_Output_Is_Empty()
    {
        // Arrange
        var rule = new RuleDetail { Divisor = 2, Output = "" };

        // Act
        var results = ValidationHelper.ValidateModel(rule);

        // Assert
        results.Should().Contain(r => r.ErrorMessage != null && r.ErrorMessage.Contains("nesmie byť prázdny"));
    }

    [Fact]
    public void FooFuuSettings_Should_Fail_When_Rules_List_Is_Empty()
    {
        // Arrange
        var settings = new KodivaFooFuuSettings { Rules = new List<RuleDetail>() };

        // Act
        var results = ValidationHelper.ValidateModel(settings);

        // Assert
        results.Should().Contain(r => r.ErrorMessage != null && r.ErrorMessage.Contains("aspoň jedno pravidlo"));
    }

    [Fact]
    public void RuleDetail_Should_Pass_With_Valid_Data()
    {
        // Arrange
        var rule = new RuleDetail { Divisor = 4, Output = "fuu" };

        // Act
        var results = ValidationHelper.ValidateModel(rule);

        // Assert
        results.Should().BeEmpty();
    }
}
