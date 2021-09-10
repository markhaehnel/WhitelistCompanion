using System.ComponentModel.DataAnnotations;

namespace WhitelistCompanion.Configuration
{
    public class MinecraftConfiguration
    {
        public const string Section = "Mc";

        [Required]
        [RegularExpression(@"^(([a-zA-Z]|[a-zA-Z][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z]|[A-Za-z][A-Za-z0-9\-]*[A-Za-z0-9])$", ErrorMessage = "The {0} field must be a valid hostname")]
        public string Hostname { get; init; }

        [Required]
        [Range(0, 65536)]
        public ushort Port { get; init; }

        [Required]
        public string Password { get; init; }
    }
}
