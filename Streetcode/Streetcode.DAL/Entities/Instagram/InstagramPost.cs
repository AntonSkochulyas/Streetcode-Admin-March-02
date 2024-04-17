using System.Text.Json.Serialization;

namespace Streetcode.DAL.Entities.Instagram
{
    public class InstagramPost
    {
        [JsonPropertyName("caption")]
        public string? Caption { get; set; }
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("media_type")]
        public string? MediaType { get; set; }
        [JsonPropertyName("media_url")]
        public string? MediaUrl { get; set; }
        [JsonPropertyName("permalink")]
        public string? Permalink { get; set; }
        [JsonPropertyName("thumbnail_url")]
        public string? ThumbnailUrl { get; set; }
        [JsonPropertyName("is_pinned")]
        public bool IsPinned { get; set; }
    }
}
