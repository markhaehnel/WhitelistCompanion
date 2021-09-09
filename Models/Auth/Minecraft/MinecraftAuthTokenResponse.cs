using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WhitelistCompanion.Models.Auth.Minecraft
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    public record MinecraftAuthTokenResponse(
        [property: JsonPropertyName("username")] string Username,
        [property: JsonPropertyName("roles")] IEnumerable<string> Roles,
        [property: JsonPropertyName("access_token")] string AccessToken,
        [property: JsonPropertyName("token_type")] string TokenType,
        [property: JsonPropertyName("expires_in")] int ExpiresIn
    );

}
