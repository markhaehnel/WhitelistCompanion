using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhitelistCompanion.Extensions.Validation
{
    public static class ValidationExtensions
    {
        public static T ValidateAndThrow<T>(this T obj)
        {
            Validator.ValidateObject(obj, new ValidationContext(obj), true);
            return obj;
        }

        public static ICollection<ValidationResult> Validate<T>(this T obj)
        {
            var Results = new List<ValidationResult>();
            var Context = new ValidationContext(obj);
            if (!Validator.TryValidateObject(obj, Context, Results, true))
                return Results;
            return null;
        }
    }
}
