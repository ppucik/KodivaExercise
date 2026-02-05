using System.ComponentModel.DataAnnotations;

namespace KodivaFooFuu.Test;

public static class ValidationHelper
{
    public static IList<ValidationResult> ValidateModel(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model, null, null);

        Validator.TryValidateObject(model, context, results, validateAllProperties: true);

        return results;
    }
}
