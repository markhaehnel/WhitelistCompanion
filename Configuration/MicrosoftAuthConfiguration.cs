using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WhitelistCompanion.Configuration
{
    public class MicrosoftAuthConfiguration
    {
        public const string Section = "Auth:Microsoft";

        [Required]
        public string ClientId { get; init; }

        [Required]
        public string ClientSecret { get; init; }

        [Required]
        [Url]
        [SuppressMessage("Microsoft.Usage", "CA1056")]
        public string RedirectUri { get; init; }
    }
}
