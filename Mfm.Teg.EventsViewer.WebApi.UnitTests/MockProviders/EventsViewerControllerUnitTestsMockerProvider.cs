using Mfm.Teg.EventsViewer.Domain.Models.Models;
using System;
using System.Collections.Generic;

namespace Mfm.Teg.EventsViewer.App.UnitTests.MockProviders
{
    internal static class EventsViewerControllerUnitTestsMockerProvider
    {
        public static int MockedVenueId = 1;
        public static List<Venue> MockedVenues = new List<Venue> {
            new Venue{ Id = 1, Label = "V1"},
            new Venue{ Id = 2, Label = "V2"}
        };
        public static List<Event> MockedEvents = new List<Event> {
            new Event{ Id = 1, Name = "E1", Description = "D1", StartDate = DateTime.Now},
            new Event{ Id = 2, Name = "E2", StartDate = DateTime.Now.AddDays(1)}
        };
    }
}
