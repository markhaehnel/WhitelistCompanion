using System.Collections.Generic;

namespace WhitelistCompanion.Models
{
    public class UserListResponse
    {
        public int CurrentUserCount { get; init; }
        public int MaxUserCount { get; init; }
        public IEnumerable<string> Users { get; init; }
    }
}
