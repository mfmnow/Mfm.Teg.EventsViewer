using Mfm.Teg.EventsViewer.Data.Contracts;
using Mfm.Teg.EventsViewer.Data.Services;
using Mfm.Teg.EventsViewer.Domain.Contracts;
using Mfm.Teg.EventsViewer.Domain.Models.Config;
using Mfm.Teg.EventsViewer.Domain.Services;

namespace Mfm.Teg.EventsViewer.WebApi.App_Code.ServiceExtensions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class EventsViewerServiceCollectionExtensions
    {
        public static IServiceCollection AddFrankfurterServices(this IServiceCollection services, Action<EventsViewerAppConfig> eventsViewerAppConfigOptions)
        {
            services.Configure(eventsViewerAppConfigOptions);

            services.AddHttpClient<IEventsAndVenuesDataAccessService, S3EventsAndVenuesDataAccessService>((provider, httpClient) =>
            {
                var eventsViewerAppConfig = provider.GetRequiredService<EventsViewerAppConfig>();
                httpClient.BaseAddress = new Uri(eventsViewerAppConfig.S3BaseUrl);
            });

            return services;
        }

        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IEventsViewerDomainService, EventsViewerDomainService>();
            services.AddSingleton<IEventsAndVenuesCacheService, EventsAndVenuesCacheService>();
            return services;
        }

        public static IServiceCollection RegisterConfigs(this IServiceCollection services, IConfiguration configuration)
        {
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
            services.AddSingleton(configuration.GetSection("EventsViewerAppConfig").Get<EventsViewerAppConfig>());
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
            return services;
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
