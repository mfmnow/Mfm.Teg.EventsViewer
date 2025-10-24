using Mfm.Teg.EventsViewer.Data.Contracts;
using Mfm.Teg.EventsViewer.Data.Entities;
using Mfm.Teg.EventsViewer.Domain.Models.Exceptions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mfm.Teg.EventsViewer.Data.Services
{
    /// <inheritdoc/>
    public class S3EventsAndVenuesDataAccessService : IEventsAndVenuesDataAccessService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<S3EventsAndVenuesDataAccessService> _logger;
        private readonly IEventsAndVenuesCacheService _eventsAndVenuesCacheService;
        private readonly static SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private const string CACHE_KEY = "EventsAndVenuesCache";

        public S3EventsAndVenuesDataAccessService(HttpClient httpClient,
            IEventsAndVenuesCacheService exchangeRatesCacheService,
            ILogger<S3EventsAndVenuesDataAccessService> logger) 
        {
            _httpClient = httpClient;
            _eventsAndVenuesCacheService = exchangeRatesCacheService;
            _logger = logger;
        }

        public virtual async Task<HttpResponseMessage> ExecuteGetRequestWithRetry(string endpoint, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            int maxRetries = 3;
            int retries = 0;
            while (retries < maxRetries)
            {
                try
                {
                    response = await _httpClient.GetAsync(endpoint, cancellationToken);
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new NotFoundException($"URL: {endpoint} not found");
                    }
                    response.EnsureSuccessStatusCode();
                    return response;
                }
                catch (NotFoundException)
                {
                    throw;
                }
                catch
                {
                    response = null;
                    retries++;
                }
            }
            return null;
        }

        public async Task<EventsAndVenuesResponse> GetLatestEventsAndVenuesResponse(CancellationToken cancellationToken)
        {
            EventsAndVenuesResponse eventsAndVenuesResponse;
            _lock.Wait(cancellationToken);
            try
            {
                var endpoint = $"events/event-data.json";
                var response = await ExecuteGetRequestWithRetry(endpoint, cancellationToken);
                eventsAndVenuesResponse = await response.Content.ReadFromJsonAsync<EventsAndVenuesResponse>();
                if (eventsAndVenuesResponse != null)
                {
                    _eventsAndVenuesCacheService.CreateCacheObject(CACHE_KEY, eventsAndVenuesResponse);
                }
                _logger.LogDebug($"nameof(GetLatestEventsAndVenuesResponse) endpoint{endpoint} {eventsAndVenuesResponse}", endpoint, eventsAndVenuesResponse);
            }
            finally
            {
                _lock.Release();
            }
            if (eventsAndVenuesResponse == null)
            {
                eventsAndVenuesResponse = _eventsAndVenuesCacheService.GetCachedObject<EventsAndVenuesResponse>(CACHE_KEY);
            }
            return eventsAndVenuesResponse;
        }

        public async Task<List<EventEntity>> GetEventsByVenueId(int venueId, CancellationToken cancellationToken)
        {
            var latestEventsAndVenuesResponse = await GetLatestEventsAndVenuesResponse(cancellationToken);
            if (latestEventsAndVenuesResponse == null) { return null; }
            return latestEventsAndVenuesResponse.Events?.Where(e => e.VenueId == venueId).ToList();
        }

        public async Task<List<VenueEntity>> GetAllVenues(CancellationToken cancellationToken)
        {
            var latestEventsAndVenuesResponse = await GetLatestEventsAndVenuesResponse(cancellationToken);
            if (latestEventsAndVenuesResponse == null) { return null; }
            return latestEventsAndVenuesResponse.Venues;
        }
    }
}
