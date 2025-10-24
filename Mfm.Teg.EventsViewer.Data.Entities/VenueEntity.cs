using System.Text.Json.Serialization;

namespace Mfm.Teg.EventsViewer.Data.Entities{

    public class VenueEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }
    }

}
