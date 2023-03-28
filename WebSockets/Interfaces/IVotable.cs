using System.Text.Json.Serialization;

namespace WebSockets.Interfaces
{
    public interface IVotable
    {
        ///Number of upvotes
        [JsonPropertyName("ups")]
        public int Ups { get; set; }

        ///Number of downvotes
        [JsonPropertyName("downs")]
        public int Downs { get; set; }

        ///True = If liked by th user, false if disliked, if not voted then null
        [JsonPropertyName("likes")]
        public bool Likes { get; set; }
    }
}
