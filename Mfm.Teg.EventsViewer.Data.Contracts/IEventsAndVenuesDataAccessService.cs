using Mfm.Teg.EventsViewer.Data.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mfm.Teg.EventsViewer.Data.Contracts
{
    public interface IEventsAndVenuesDataAccessService
    {
        /// <summary>
        ///  In some cases, the S3 API may not respond to the first request but to the second or third. A retry logic is needed.
        /// </summary>
        /// <param name="endpoint">Endpoint</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        Task<HttpResponseMessage> ExecuteGetRequestWithRetry(string endpoint, CancellationToken cancellationToken);

        /// <summary>
        /// Gets Latest Events And Venues Response, if failed to get live data, data will be retreived from cache
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        Task<EventsAndVenuesResponse> GetLatestEventsAndVenuesResponse(CancellationToken cancellationToken);

        /// <summary>
        /// Gets List of Events from Latest Events And Venues Response by VenueId
        /// </summary>
        /// <param name="venueId">Venue Id</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        Task<List<EventEntity>> GetEventsByVenueId(int venueId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets List of all Venues from Latest Events And Venues Response
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        Task<List<VenueEntity>> GetAllVenues(CancellationToken cancellationToken);
    }
}
