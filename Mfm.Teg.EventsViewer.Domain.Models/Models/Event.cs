using System;

namespace Mfm.Teg.EventsViewer.Domain.Models.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
    }
}