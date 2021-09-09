using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WhitelistCompanion.Models.Auth.Minecraft
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    public record MinecraftEntitlementsResponse(
        [property: JsonPropertyName("items")] IEnumerable<MinecraftEntitlementsResponseItem> Items,
        [property: JsonPropertyName("signature")] string Signature,
        [property: JsonPropertyName("keyId")] string KeyId
    );

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    public record MinecraftEntitlementsResponseItem(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("signature")] string Signature
    );
}
