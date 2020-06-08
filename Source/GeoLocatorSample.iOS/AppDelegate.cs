using UIKit;
using Foundation;
using Comet.iOS;

namespace GeoLocatorSample.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : UIApplicationDelegate
    {
        UIWindow? _window;

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
#if DEBUG
            Comet.Reload.Init();
#endif
            _window = new UIWindow
            {
                RootViewController = new GeoCoordinatesPage().ToViewController()
            };
            _window.MakeKeyAndVisible();

            return true;
        }
    }
}
