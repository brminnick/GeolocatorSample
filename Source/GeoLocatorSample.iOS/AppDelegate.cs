using UIKit;
using Foundation;
using Comet.iOS;

namespace GeoLocatorSample.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        public override UIWindow? Window
        {
            get;
            set;
        }

        UIWindow? window;

        public override void FinishedLaunching(UIApplication application)
        {
            base.FinishedLaunching(application);
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
#if DEBUG
            Comet.Reload.Init();
#endif

            window = new UIWindow
            {
                RootViewController = new GeoCoordinatesPage().ToViewController()
            };
            window.MakeKeyAndVisible();

            return true;
        }
    }
}
