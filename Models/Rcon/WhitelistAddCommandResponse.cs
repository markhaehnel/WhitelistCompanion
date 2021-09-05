using System.Linq;
using System.Text.RegularExpressions;
using CoreRCON.Parsers;

namespace WhitelistCompanion.Models.Rcon
{
    public class WhitelistAddCommandResponse : IParseable
    {
        public bool Success { get; init; }
        public string User { get; init; }
    }

    public class WhitelistAddCommandParser : DefaultParser<WhitelistAddCommandResponse>
    {
        public override string Pattern { get; } = @"Added (?<User>.*) to the whitelist";

        public override WhitelistAddCommandResponse Load(GroupCollection groups)
        {
            var hasValues = groups.Values.Any();

            return new WhitelistAddCommandResponse
            {
                Success = hasValues,
                User = hasValues ? groups["User"].Value : null
            };
        }
    }
}
