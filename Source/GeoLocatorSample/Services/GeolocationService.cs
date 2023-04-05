using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Dispatching;

namespace GeoLocatorSample;

public class GeolocationService
{
    readonly IDispatcher _dispatcher;
    readonly IGeolocation _geolocation;
    readonly WeakEventManager<Exception> _geolocationFailedWeakEventManager = new();

    public GeolocationService([NotNull] IDispatcher? dispatcher, IGeolocation geolocation)
    {
        ArgumentNullException.ThrowIfNull(dispatcher);
        (_dispatcher, _geolocation) = (dispatcher, geolocation);
    }

    public event EventHandler<Exception> GeolocationFailed
    {
        add => _geolocationFailedWeakEventManager.AddEventHandler(value);
        remove => _geolocationFailedWeakEventManager.RemoveEventHandler(value);
    }

    public async Task<Location> GetLocation()
    {
        try
        {
            var location = await _geolocation.GetLocationAsync(new(GeolocationAccuracy.Best)).ConfigureAwait(false);
            return location ?? throw new InvalidOperationException("Location Cannot Be Null");
        }
        catch (PermissionException e) when (e.Message.ToLower().Contains("main thread"))
        {
            var location = await _dispatcher.DispatchAsync(() => _geolocation.GetLocationAsync(new(GeolocationAccuracy.Best))).ConfigureAwait(false);
            return location ?? throw new InvalidOperationException("Location Cannot Be Null");
        }
        catch (Exception e)
        {
            OnGeolocationFailed(e);
            throw;
        }
    }

    void OnGeolocationFailed(Exception exception) => _geolocationFailedWeakEventManager.RaiseEvent(null, exception, nameof(GeolocationFailed));
}
