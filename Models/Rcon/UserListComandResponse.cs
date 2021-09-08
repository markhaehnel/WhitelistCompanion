using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using CoreRCON.Parsers;

namespace WhitelistCompanion.Models.Rcon
{

    public class UserListCommandResponse : IParseable
    {
        public int CurrentUserCount { get; init; }
        public int MaxUserCount { get; init; }
        public IEnumerable<string> Users { get; init; }
    }

    public class UserListCommandParser : DefaultParser<UserListCommandResponse>
    {
        public override string Pattern { get; } = @"There are (?<CurrentUserCount>\d+) of a max of (?<MaxUserCount>\d+) players online(: (?<Users>.+)(,\s*\.+)*|.*)";

        public override UserListCommandResponse Load(GroupCollection groups)
        {
            if (groups is null) throw new ArgumentNullException(nameof(groups));

            var usernamesValue = groups["Users"].Value;

            return new UserListCommandResponse
            {
                CurrentUserCount = int.Parse(groups["CurrentUserCount"].Value, CultureInfo.InvariantCulture),
                MaxUserCount = int.Parse(groups["MaxUserCount"].Value, CultureInfo.InvariantCulture),
                Users = string.IsNullOrEmpty(usernamesValue) ? Array.Empty<string>() : usernamesValue.Split(", ")
            };
        }
    }
}
