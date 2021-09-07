using System.Text.Json.Serialization;

namespace WhitelistCompanion.Models.Auth
{
    public record TokenResponse(
        [property: JsonPropertyName("token_type")] string TokenType,
        [property: JsonPropertyName("scope")] string Scope,
        [property: JsonPropertyName("expires_in")] int ExpiresIn,
        [property: JsonPropertyName("ext_expires_in")] int ExtExpiresIn,
        [property: JsonPropertyName("access_token")] string AccessToken
    );
}
