using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;

namespace Monospace2
{
	/// <summary>
	/// ROOT of this application; referenced in "Main.cs"
	/// </summary>
	[Register ("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;
		
		TabBarController tabBarController;

		public static Database SessionDatabase{get;set;}
		
		/// <summary>
		/// Create the TabBarController which will drive the different views in this app.
		/// IT will create the 'sub' NavigationControllers necessary
		/// </summary>
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
			SessionDatabase = new Database ("Conference.db");
			
			// Create the tab
			tabBarController = new Monospace2.TabBarController ();
			
            // Create the main window and add the navigation controller as a subview
            window = new UIWindow (UIScreen.MainScreen.Bounds);
            window.AddSubview(tabBarController.View);
            window.MakeKeyAndVisible ();
            return true;
        }
		
        // This method is allegedly required in iPhoneOS 3.0
        public override void OnActivated (UIApplication application)
        {
        }
    }
}
