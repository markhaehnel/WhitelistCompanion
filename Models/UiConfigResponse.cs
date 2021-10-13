
using System.Diagnostics.CodeAnalysis;

namespace WhitelistCompanion.Models
{
    public class UiConfigResponse
    {
        public string ServerAddress { get; init; }
        [SuppressMessage("Microsoft.Usage", "CA1056")]
        public string MapUri { get; init; }
    }
}
