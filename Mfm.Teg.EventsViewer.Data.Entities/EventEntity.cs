using System;
using System.Text.Json.Serialization;

namespace Mfm.Teg.EventsViewer.Data.Entities
{
    public class EventEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("StartDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("VenueId")]
        public int VenueId { get; set; }
    }
}
