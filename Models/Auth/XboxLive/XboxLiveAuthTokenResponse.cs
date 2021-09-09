using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WhitelistCompanion.Models.Auth.XboxLive
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    public record XboxLiveAuthTokenResponse(
        DateTime IssueInstant,
        DateTime NotAfter,
        string Token,
        XboxLiveAuthTokenResponseDisplayClaims DisplayClaims
    );

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    public record XboxLiveAuthTokenResponseDisplayClaims(
        [property: JsonPropertyName("xui")] IEnumerable<XboxLiveAuthTokenResponseDisplayClaimsXui> Xui
    );

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    public record XboxLiveAuthTokenResponseDisplayClaimsXui(
        [property: JsonPropertyName("uhs")] string Uhs
    );
}
