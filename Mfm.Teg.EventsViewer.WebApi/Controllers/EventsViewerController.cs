using Mfm.Teg.EventsViewer.Domain.Contracts;
using Mfm.Teg.EventsViewer.Domain.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Mfm.Teg.EventsViewer.WebApi.Controllers
{
    /// <summary>
    /// EventsViewerController class
    /// </summary>
    [ApiController]
    [Route("api/v1.0/event-viewer")]
    [EnableRateLimiting("Default")]
    public class EventsViewerController : ControllerBase
    {
        private readonly ILogger<EventsViewerController> _logger;
        private readonly IEventsViewerDomainService _eventsViewerDomainService;

        /// <summary>
        /// Manages Currency Exchange features
        /// </summary>
        /// <param name="eventsViewerDomainService"></param>
        /// <param name="logger"></param>
        public EventsViewerController(IEventsViewerDomainService eventsViewerDomainService, ILogger<EventsViewerController> logger)
        {
            _eventsViewerDomainService = eventsViewerDomainService;
            _logger = logger;
        }

        /// <summary>
        /// Gets List of all Venues
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of all venues</returns>
        [HttpGet("all-venues")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Venue>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllVenues(CancellationToken cancellationToken)
        {
            var allVenues = await _eventsViewerDomainService.GetAllVenues(cancellationToken);
            return Ok(allVenues);
        }

        /// <summary>
        /// Gets List of Events by VenueId
        /// </summary>
        /// <param name="venueId">Venue Id</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        [HttpGet("events/{venueId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Event>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventsByVenueId(int venueId, CancellationToken cancellationToken)
        {
            var events = await _eventsViewerDomainService.GetEventsByVenueId(venueId, cancellationToken);
            return Ok(events);
        }
    }
}
