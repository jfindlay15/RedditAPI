using System.Text.Json.Serialization;

namespace WebSockets.Interfaces
{
    public interface ICreated
    {
        ///eoch-second format of creation time
        [JsonPropertyName("created")]
        public DateTime CreatedDateTime { get; set; }

        ///time of creation in UTC epoch second format
        [JsonPropertyName("created_utc")]
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
