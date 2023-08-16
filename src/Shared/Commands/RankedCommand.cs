using System.CommandLine;
using Weedwacker.Shared.Enums;

namespace Weedwacker.Shared.Commands
{
    public class RankedCommand : Command
    {
        public UserRank Rank { get; }

        public RankedCommand(UserRank rank, string name, string? description = null) : base(name, description)
        {
            Rank = rank;
        }
    }
}
