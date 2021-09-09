using System.Collections.Generic;

namespace WhitelistCompanion.Models.Auth.Xsts
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    public record XstsAuthTokenRequest(
        XstsAuthTokenRequestProperties Properties,
        string RelyingParty,
        string TokenType
    );

    public record XstsAuthTokenRequestProperties(
        string SandboxId,
        IEnumerable<string> UserTokens
    );
}
