using System;
using System.Threading.Tasks;

using AsyncAwaitBestPractices;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace GeoLocatorSample
{
    public static class GeolocationService
    {
        #region Constant Fields
        static readonly WeakEventManager<Exception> _geolocationFailedWeakEventManager = new WeakEventManager<Exception>();
        static readonly Lazy<GeolocationRequest> _geolocationRequestHolder = new Lazy<GeolocationRequest>(() => new GeolocationRequest(GeolocationAccuracy.Best));
        #endregion

        #region Events
        public static event EventHandler<Exception> GeolocationFailed
        {
            add => _geolocationFailedWeakEventManager.AddEventHandler(value);
            remove => _geolocationFailedWeakEventManager.RemoveEventHandler(value);
        }
        #endregion

        #region Properties
        static GeolocationRequest GeolocationRequest => _geolocationRequestHolder.Value;
        #endregion

        #region Methods
        public static async Task<Location> GetLocation()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync(GeolocationRequest).ConfigureAwait(false);
                return location;
            }
            catch (PermissionException e) when (e.Message.ToLower().Contains("main thread"))
            {
                var location = await GetLocationOnMainThread();
                return location;
            }
            catch (Exception e)
            {
                OnGeolocationFailed(e);
                throw;
            }
        }

        static Task<Location> GetLocationOnMainThread()
        {
            var taskCompletionSource = new TaskCompletionSource<Location>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var location = await Geolocation.GetLocationAsync(GeolocationRequest).ConfigureAwait(false);
                    taskCompletionSource.SetResult(location);
                }
                catch (Exception e)
                {
                    OnGeolocationFailed(e);
                    taskCompletionSource.SetException(e);
                }
            });

            return taskCompletionSource.Task;
        }

        static void OnGeolocationFailed(Exception exception) => _geolocationFailedWeakEventManager?.HandleEvent(null, exception, nameof(GeolocationFailed));
        #endregion
    }
}
