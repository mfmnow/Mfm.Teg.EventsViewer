using Mfm.Teg.EventsViewer.Data.Contracts;
using System.Collections.Concurrent;

namespace Mfm.Teg.EventsViewer.Data.Services
{
    /// <inheritdoc/>
    public class EventsAndVenuesCacheService : IEventsAndVenuesCacheService
    {
        private readonly ConcurrentDictionary<string, object> _cachedObjects = new ConcurrentDictionary<string, object>();

        public T GetCachedObject<T>(string key)
        {
            object cachedObject;
            _cachedObjects.TryGetValue(key, out cachedObject);
            if(cachedObject == null)
            {
                return default;
            }
            return (T)cachedObject;
        }

        public void CreateCacheObject<T>(string key, T objectToCache)
        {
            _cachedObjects.GetOrAdd(key, objectToCache);
        }
    }
}
