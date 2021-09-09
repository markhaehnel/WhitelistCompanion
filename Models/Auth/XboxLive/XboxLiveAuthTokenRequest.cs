namespace WhitelistCompanion.Models.Auth.XboxLive
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801")]
    public record XboxLiveAuthTokenRequest(
        XboxLiveAuthTokenRequestProperties Properties,
        string RelyingParty,
        string TokenType
    );

    public record XboxLiveAuthTokenRequestProperties(
        string AuthMethod,
        string SiteName,
        string RpsTicket
    );
}
