using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WhitelistCompanion.Models.Auth.Xsts
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    public record XstsAuthTokenResponse(
        DateTime IssueInstant,
        DateTime NotAfter,
        string Token,
        XstsAuthTokenResponseDisplayClaims DisplayClaims
    );

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    public record XstsAuthTokenResponseDisplayClaims(
        [property: JsonPropertyName("xui")] IEnumerable<XstsAuthTokenResponseDisplayClaimsXui> Xui
    );

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    public record XstsAuthTokenResponseDisplayClaimsXui(
        [property: JsonPropertyName("uhs")] string Uhs
    );
}
