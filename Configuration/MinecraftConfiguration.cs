using System.ComponentModel.DataAnnotations;

namespace WhitelistCompanion.Configuration
{
    public class MinecraftConfiguration
    {
        public const string Section = "Mc";

        [MinLength(1, ErrorMessage = "Value for {0} cannot be empty"), RegularExpression(@"^(([a-zA-Z]|[a-zA-Z][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z]|[A-Za-z][A-Za-z0-9\-]*[A-Za-z0-9])$", ErrorMessage = "Value for {0} must be a hostname")]
        public string Hostname { get; init; }

        [Range(0, 65536, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public ushort Port { get; init; }

        [MinLength(1, ErrorMessage = "Value for {0} cannot be empty")]
        public string Password { get; init; }
    }
}