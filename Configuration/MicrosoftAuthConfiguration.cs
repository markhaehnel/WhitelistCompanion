using System;
using System.ComponentModel.DataAnnotations;

namespace WhitelistCompanion.Configuration
{
    public class MicrosoftAuthConfiguration
    {
        public const string Section = "Auth:Microsoft";

        [MinLength(1, ErrorMessage = "Value for {0} cannot be empty")]
        public string ClientId { get; init; }

        [MinLength(1, ErrorMessage = "Value for {0} cannot be empty")]
        public string ClientSecret { get; init; }

        public Uri RedirectUri { get; init; }
    }
}
