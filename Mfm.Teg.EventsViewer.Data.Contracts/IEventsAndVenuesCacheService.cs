namespace Mfm.Teg.EventsViewer.Data.Contracts
{
    public interface IEventsAndVenuesCacheService
    {
        T GetCachedObject<T>(string key);
        void CreateCacheObject<T>(string key, T objectToCache);
    }
}
