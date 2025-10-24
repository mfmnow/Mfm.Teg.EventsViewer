using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Mfm.Teg.EventsViewer.WebApi.Controllers;
using Mfm.Teg.EventsViewer.Domain.Contracts;
using System.Threading;
using Mfm.Teg.EventsViewer.App.UnitTests.MockProviders;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace Mfm.Teg.EventsViewer.App.UnitTests
{
    public class EventsViewerControllerUnitTests
    {
        private readonly Mock<IEventsViewerDomainService> _mockedEventsViewerDomainService;
        private readonly Mock<ILogger<EventsViewerController>> _mockedLogger;
        private readonly Mock<EventsViewerController> _eventsViewerController;

        public EventsViewerControllerUnitTests()
        {
            _mockedEventsViewerDomainService = new Mock<IEventsViewerDomainService>();
            _mockedLogger = new Mock<ILogger<EventsViewerController>>();
            _eventsViewerController = new Mock<EventsViewerController>(_mockedEventsViewerDomainService.Object, _mockedLogger.Object)
            { CallBase = true };
        }

        [Fact]
        public async Task GetAllVenues_Should_Follow_LogicalFlow_And_Return_Valid_Result_When_Called()
        {
            //Arrange
            _mockedEventsViewerDomainService.Setup(e => e.GetAllVenues(CancellationToken.None))
                .Returns(Task.FromResult(EventsViewerControllerUnitTestsMockerProvider.MockedVenues));

            //Execute
            var response = await _eventsViewerController.Object.GetAllVenues(CancellationToken.None);

            //Assert
            Assert.NotNull(response);
            var content = response as OkObjectResult;
            content?.Value?.Should().BeEquivalentTo(EventsViewerControllerUnitTestsMockerProvider.MockedVenues); 
            _mockedEventsViewerDomainService.Verify(t => t.GetAllVenues(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task ConvertCurrencyAmount_Should_Follow_LogicalFlow_And_Return_Valid_Result_When_Called()
        {
            //Arrange
            _mockedEventsViewerDomainService.Setup(e => e.GetEventsByVenueId(EventsViewerControllerUnitTestsMockerProvider.MockedVenueId, CancellationToken.None))
                .Returns(Task.FromResult(EventsViewerControllerUnitTestsMockerProvider.MockedEvents));

            //Execute
            var response = await _eventsViewerController.Object.GetEventsByVenueId(EventsViewerControllerUnitTestsMockerProvider.MockedVenueId, CancellationToken.None);

            //Assert
            Assert.NotNull(response);
            var content = response as OkObjectResult;
            content?.Value?.Should().BeEquivalentTo(EventsViewerControllerUnitTestsMockerProvider.MockedEvents);
            _mockedEventsViewerDomainService.Verify(t => t.GetEventsByVenueId(EventsViewerControllerUnitTestsMockerProvider.MockedVenueId, CancellationToken.None), Times.Once);
        }
    }
}