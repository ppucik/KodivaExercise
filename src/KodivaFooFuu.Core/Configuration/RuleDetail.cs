using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace KodivaFooFuu.Core.Configuration;

/// <summary>
/// Represents a rule that associates a divisor with a corresponding output value.
/// </summary>
public class RuleDetail
{
    /// <summary>
    /// Gets or sets the divisor value used in calculations.
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Deliteľ (Divisor) musí byť kladné číslo väčšie ako 0.")]
    public int Divisor { get; set; }

    /// <summary>
    /// Gets or sets the output text for the operation.
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Výstupný text (Output) nesmie byť prázdny.")]
    public string Output { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the color for output text.
    /// </summary>
    // TODO: Implementovať logiku pre farbu výstupu.
    [Obsolete("Táto vlastnosť nie je plne implementovaná. Používa sa predvolená hodnota White.")]
    public Color Color { get; set; } = Color.White;
}
