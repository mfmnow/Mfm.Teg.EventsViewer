using Mfm.Teg.EventsViewer.Domain.Models.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mfm.Teg.EventsViewer.Domain.Contracts
{
    public interface IEventsViewerDomainService
    {
        /// <summary>
        /// Gets List of all Venues
        /// </summary>
        /// <param name="currency">Short name of a currency.</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Cached object <see cref="EventsAndVenuesResponse"/></returns>
        Task<List<Venue>> GetAllVenues(CancellationToken cancellationToken);

        /// <summary>
        /// Gets List of Events by VenueId
        /// </summary>
        /// <param name="venueId">Venue Id</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        Task<List<Event>> GetEventsByVenueId(int venueId, CancellationToken cancellationToken);
    }
}
