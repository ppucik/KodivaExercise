using System.ComponentModel.DataAnnotations;

namespace KodivaFooFuu.Core.Configuration;

/// <summary>
/// Represents the configuration settings for the Kodiva FooFuu feature, including the collection of rule details to be applied.
/// </summary>
public class KodivaFooFuuSettings
{
    public const string SECTION_NAME = nameof(KodivaFooFuuSettings);

    [Required, MinLength(1, ErrorMessage = "Musíte definovať aspoň jedno pravidlo.")]
    public List<RuleDetail> Rules { get; set; } = [];
}
