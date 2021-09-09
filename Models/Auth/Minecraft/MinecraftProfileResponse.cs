using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WhitelistCompanion.Models.Auth.Minecraft
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    public record MinecraftProfileResponse(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("skins")] IEnumerable<MinecraftProfileResponseSkins> Skins
    );

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1054")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1056")]
    public record MinecraftProfileResponseSkins(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("state")] string State,
        [property: JsonPropertyName("url")] string Url,
        [property: JsonPropertyName("variant")] string Variant,
        [property: JsonPropertyName("alias")] string Alias
    );
}
