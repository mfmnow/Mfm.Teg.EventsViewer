using FluentAssertions;
using Mfm.Teg.EventsViewer.Data.Entities;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Mfm.Teg.EventsViewer.Data.Services.UnitTests.Services
{
    public class EventsAndVenuesCacheServiceUnitTests
    {
        private readonly Mock<EventsAndVenuesCacheService> _eventsAndVenuesCacheService;

        public EventsAndVenuesCacheServiceUnitTests()
        {
            _eventsAndVenuesCacheService = new Mock<EventsAndVenuesCacheService>() { CallBase = true };
        }

        [Fact]
        public void CreateCacheObject_Then_GetCachedObject_Should_Work_AS_Expected_When_Using_Valid_Key()
        {
            var mockedEventsAndVenuesResponse = new EventsAndVenuesResponse
            {
                Events = new List<EventEntity> {
                    new EventEntity{ VenueId = 1 }
                },
                Venues = new List<VenueEntity> {
                    new VenueEntity{ Id = 2 }
                }
            };
            var mockedCacheKey = "mockedCacheKey";

            //Execute
            _eventsAndVenuesCacheService.Object.CreateCacheObject<EventsAndVenuesResponse>(mockedCacheKey, mockedEventsAndVenuesResponse);
            var response = _eventsAndVenuesCacheService.Object.GetCachedObject<EventsAndVenuesResponse>(mockedCacheKey);

            //Assert
            Assert.NotNull(response);
            response.Should().BeEquivalentTo(mockedEventsAndVenuesResponse);
        }

        [Fact]
        public void CreateCacheObject_Then_GetCachedObject_Should_Return_Null_When_Using_Non_Valid_Key()
        {
            var mockedCacheKey = "mockedCacheKey";

            //Execute
            var response = _eventsAndVenuesCacheService.Object.GetCachedObject<EventsAndVenuesResponse>(mockedCacheKey);

            //Assert
            Assert.Null(response);
        }
    }
}
