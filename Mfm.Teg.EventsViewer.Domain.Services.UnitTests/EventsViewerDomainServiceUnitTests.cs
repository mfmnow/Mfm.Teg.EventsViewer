using FluentAssertions;
using Mfm.Teg.EventsViewer.Data.Contracts;
using Mfm.Teg.EventsViewer.Domain.Services.UnitTests.MockProviders;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Mfm.Teg.EventsViewer.Domain.Services.UnitTests
{
    public class EventsViewerDomainServiceUnitTests
    {
        private readonly Mock<IEventsAndVenuesDataAccessService> _mockedEventsAndVenuesDataAccessService;
        private readonly Mock<EventsViewerDomainService> _eventsViewerDomainService;

        public EventsViewerDomainServiceUnitTests()
        {
            _mockedEventsAndVenuesDataAccessService = new Mock<IEventsAndVenuesDataAccessService>();
            _eventsViewerDomainService = new Mock<EventsViewerDomainService>(_mockedEventsAndVenuesDataAccessService.Object)
            { CallBase = true };
        }

        [Fact]
        public async Task GetAllVenues_Should_Follow_LogicalFlow_And_Return_Valid_Result_When_Called()
        {
            //Arrange
            _mockedEventsAndVenuesDataAccessService.Setup(e => e.GetAllVenues(CancellationToken.None))
                .Returns(Task.FromResult(EventsViewerDomainServiceMockerProvider.MockedVenuesEntities));

            //Execute
            var response = await _eventsViewerDomainService.Object.GetAllVenues(CancellationToken.None);

            //Assert
            Assert.NotNull(response);
            response.Should().BeEquivalentTo(EventsViewerDomainServiceMockerProvider.ExpectedVenues);
            _mockedEventsAndVenuesDataAccessService.Verify(t => t.GetAllVenues(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task GetEventsByVenueId_Should_Follow_LogicalFlow_And_Return_Valid_Result_When_Called()
        {
            //Arrange
            _mockedEventsAndVenuesDataAccessService.Setup(e => e.GetEventsByVenueId(EventsViewerDomainServiceMockerProvider.MockedVenueId, CancellationToken.None))
                .Returns(Task.FromResult(EventsViewerDomainServiceMockerProvider.MockedEventsEntities));

            //Execute
            var response = await _eventsViewerDomainService.Object.GetEventsByVenueId
                (EventsViewerDomainServiceMockerProvider.MockedVenueId, CancellationToken.None);

            //Assert
            Assert.NotNull(response);
            response.Should().BeEquivalentTo(EventsViewerDomainServiceMockerProvider.ExpectedEvents);
            _mockedEventsAndVenuesDataAccessService.Verify(t => t.GetEventsByVenueId(EventsViewerDomainServiceMockerProvider.MockedVenueId, CancellationToken.None), Times.Once);
        }

        //TODO public async Task GetAllVenues_Should_Follow_LogicalFlow_And_Throw_Exception_When_Called_And_Data_Is_Null()
        //TODO public async Task GetEventsByVenueId_Should_Follow_LogicalFlow_And_Throw_Exception_When_Called_And_Data_Is_Null()

    }
}