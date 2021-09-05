using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CoreRCON.Parsers;

namespace WhitelistCompanion.Models.Rcon
{
    public class WhitelistListCommandResponse : IParseable
    {
        public IEnumerable<string> Users { get; init; }
    }

    public class WhitelistListCommandParser : DefaultParser<WhitelistListCommandResponse>
    {
        public override string Pattern { get; } = @"There are (\d+|no) whitelisted players(: (?<Users>.+)(,\s*\.+)*|.*)";

        public override WhitelistListCommandResponse Load(GroupCollection groups)
        {
            var usernamesValue = groups["Users"].Value;

            return new WhitelistListCommandResponse
            {
                Users = string.IsNullOrEmpty(usernamesValue) ? Array.Empty<string>() : usernamesValue.Split(", ")
            };
        }
    }
}
