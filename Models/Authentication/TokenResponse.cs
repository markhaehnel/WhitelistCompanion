using System.Text.Json.Serialization;

namespace WhitelistCompanion.Models.Auth
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    public record TokenResponse(
       [property: JsonPropertyName("token_type")] string TokenType,
       [property: JsonPropertyName("scope")] string Scope,
       [property: JsonPropertyName("expires_in")] int ExpiresIn,
       [property: JsonPropertyName("ext_expires_in")] int ExtExpiresIn,
       [property: JsonPropertyName("access_token")] string AccessToken
   );
}
