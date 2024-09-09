using System.Diagnostics.CodeAnalysis;

namespace space_invaders_score_api.Entities
{
    public class SubmitPlayerRequest
    {
        [NotNull]
        public string? Key { get; set; }
        public string? Name { get; set; }
        public int Score { get; set; }
    }
}
