using Mfm.Teg.EventsViewer.Data.Entities;
using Mfm.Teg.EventsViewer.Domain.Models.Models;
using System;
using System.Collections.Generic;

namespace Mfm.Teg.EventsViewer.Domain.Services.UnitTests.MockProviders
{
    internal static class EventsViewerDomainServiceMockerProvider
    {
        public static int MockedVenueId = 1;
        private static DateTime MockedStartDate = DateTime.Now;
        public static List<VenueEntity> MockedVenuesEntities = new List<VenueEntity> {
            new VenueEntity{ Id = 1, Name = "N1", Location="L1", Capacity=5},
            new VenueEntity{ Id = 2, Name = "N2", Location="L2", Capacity=50}
        };
        public static List<EventEntity> MockedEventsEntities = new List<EventEntity> {
            new EventEntity{ Id = 1, Name = "E1", Description = "D1", StartDate = MockedStartDate,  VenueId=1},
            new EventEntity{ Id = 2, Name = "E2", StartDate = MockedStartDate.AddDays(1), VenueId=1}
        };
        public static List<Venue> ExpectedVenues = new List<Venue> {
            new Venue{ Id = 1, Label = "N1 - L1"},
            new Venue{ Id = 2, Label = "N2 - L2"}
        };
        public static List<Event> ExpectedEvents = new List<Event> {
            new Event{ Id = 1, Name = "E1", Description = "D1", StartDate = MockedStartDate},
            new Event{ Id = 2, Name = "E2", StartDate = MockedStartDate.AddDays(1)}
        };
    }
}
