using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WhitelistCompanion.Configuration
{
    public class UiConfiguration
    {
        public const string Section = "Ui";

        [Required]
        [RegularExpression(@"^(([a-zA-Z]|[a-zA-Z][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z]|[A-Za-z][A-Za-z0-9\-]*[A-Za-z0-9])$", ErrorMessage = "The {0} field must be a valid hostname")]
        public string ServerAddress { get; init; }

        [SuppressMessage("Microsoft.Usage", "CA1056")]
        public string MapUri { get; init; }
    }
}
