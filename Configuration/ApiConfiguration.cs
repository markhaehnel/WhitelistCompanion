using System.ComponentModel.DataAnnotations;

namespace WhitelistCompanion.Configuration
{
    public class ApiConfiguration
    {
        public const string Section = "Api";

        [RegularExpression(@"^([a-zA-Z0-9]){8,}$", ErrorMessage = "Value for {0} must be alphanumeric and have length of 8 or more")]
        public string Key { get; init; }
    }
}
