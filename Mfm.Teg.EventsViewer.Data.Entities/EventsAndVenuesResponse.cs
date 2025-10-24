using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mfm.Teg.EventsViewer.Data.Entities
{
    public class EventsAndVenuesResponse
    {
        [JsonPropertyName("events")]
        public List<EventEntity> Events { get; set; }

        [JsonPropertyName("venues")]
        public List<VenueEntity> Venues { get; set; }
    }
}
