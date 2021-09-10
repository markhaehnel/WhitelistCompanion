using System.ComponentModel.DataAnnotations;

namespace WhitelistCompanion.Configuration
{
    public class ApiConfiguration
    {
        public const string Section = "Api";

        [Required]
        [MinLength(8)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "The {0} field must be alphanumeric.")]
        public string Key { get; init; }
    }
}
