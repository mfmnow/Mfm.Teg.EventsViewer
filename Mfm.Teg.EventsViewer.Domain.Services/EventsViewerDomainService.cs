using Mfm.Teg.EventsViewer.Data.Contracts;
using Mfm.Teg.EventsViewer.Domain.Contracts;
using Mfm.Teg.EventsViewer.Domain.Models.Exceptions;
using Mfm.Teg.EventsViewer.Domain.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mfm.Teg.EventsViewer.Domain.Services
{
    /// <inheritdoc/>
    public class EventsViewerDomainService : IEventsViewerDomainService
    {
        private readonly IEventsAndVenuesDataAccessService _eventsAndVenuesDataAccessService;

        public EventsViewerDomainService(IEventsAndVenuesDataAccessService eventsAndVenuesDataAccessService)
        {
            _eventsAndVenuesDataAccessService = eventsAndVenuesDataAccessService;
        }
        public async Task<List<Venue>> GetAllVenues(CancellationToken cancellationToken)
        {
            var allVenues = await _eventsAndVenuesDataAccessService.GetAllVenues(cancellationToken);
            if (allVenues == null)
            {
                throw new BusinessValidationException("Unable to load venues");
            }
            return allVenues.Select(venue => new Venue
            {
                Id = venue.Id,
                Label = $"{venue.Name} - {venue.Location}"
            }).OrderBy(v => v.Label).ToList();
        }

        public async Task<List<Event>> GetEventsByVenueId(int venueId, CancellationToken cancellationToken)
        {
            var allEvents = await _eventsAndVenuesDataAccessService.GetEventsByVenueId(venueId, cancellationToken);
            if (allEvents == null)
            {
                throw new BusinessValidationException("Unable to load events");
            }
            return allEvents.Select(ev => new Event
            {
                Id = ev.Id,
                Name = ev.Name,
                Description = ev.Description,
                StartDate = ev.StartDate
            }).OrderBy(e => e.StartDate).ToList();
        }
    }
}
