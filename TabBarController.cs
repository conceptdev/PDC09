using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SQLite;

namespace Monospace2
{
	public class TabBarController : UITabBarController
	{
		/// <summary>
		/// One ViewController for each tab
		/// </summary>
		MonoTouch.UIKit.UINavigationController navSessionController
				, navSpeakerController
				, navBlogController
				, navTwitterController
				, navScheduleController;
		
		/// <summary>
		/// Create the four ViewControllers that we are going to use for the tabs:
		/// Sessions, Speakers, Rss, Twitter
		/// </summary>
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			var dvc = new DaysViewController ();
			navScheduleController = new MonoTouch.UIKit.UINavigationController();
			navScheduleController.PushViewController(dvc, false);
			navScheduleController.NavigationBar.BarStyle = UIBarStyle.Black;
			navScheduleController.TopViewController.Title ="Schedule";
			navScheduleController.TabBarItem = new UITabBarItem("Schedule", UIImage.FromFile("tabsession.png"), 0);
			//http://discussions.apple.com/thread.jspa?threadID=1649012&tstart=0
			//navSessionController.NavigationBar.TintColor = UIColor.Cyan;
			
			
			var svc = new SpeakersViewController();
			navSpeakerController = new MonoTouch.UIKit.UINavigationController();
			navSpeakerController.PushViewController(svc, false);
			navSpeakerController.TopViewController.View.BackgroundColor = new UIColor(65.0f,169.0f,198.0f,255.0f);
			navSpeakerController.NavigationBar.BarStyle = UIBarStyle.Black;
			navSpeakerController.TopViewController.Title ="Speakers";
			navSpeakerController.TabBarItem = new UITabBarItem("Speakers", UIImage.FromFile("tabspeaker.png"), 0);
			
			var rvc = new BlogViewController();
			navBlogController = new MonoTouch.UIKit.UINavigationController();
			navBlogController.PushViewController(rvc, false);
			navBlogController.NavigationBar.BarStyle = UIBarStyle.Black;
			navBlogController.TopViewController.Title ="Blog";
			navBlogController.TabBarItem = new UITabBarItem("Blog", UIImage.FromFile("tabblogger.png"), 0);
			
			
			var ssvc = new RssSessionsViewController();
			navSessionController = new MonoTouch.UIKit.UINavigationController();
			navSessionController.PushViewController(ssvc, false);
			navSessionController.NavigationBar.BarStyle = UIBarStyle.Black;
			navSessionController.TopViewController.Title ="Sessions";
			navSessionController.TabBarItem = new UITabBarItem("Sessions", UIImage.FromFile("tabblogger.png"), 0);
			
			
			var tvc = new TwitterViewController();
			navTwitterController = new MonoTouch.UIKit.UINavigationController();
			navTwitterController.PushViewController(tvc, false);
			navTwitterController.NavigationBar.BarStyle = UIBarStyle.Black;
			navTwitterController.TopViewController.Title ="Twitter";
			navTwitterController.TabBarItem = new UITabBarItem("Twitter", UIImage.FromFile("tabtwitter.png"), 0);
			
			
			var mvc = new MapFlipViewController();
			mvc.Title = "Map";
			mvc.TabBarItem = new UITabBarItem("Map", UIImage.FromFile("tabmap.png"), 0);
			
			
			var pvc = new SponsorsViewController();
			pvc.Title = "Sponsors";
			pvc.TabBarItem = new UITabBarItem("Sponsors", UIImage.FromFile("tabsponsor.png"), 0);
			
			var u = new UIViewController[]{
				  navScheduleController
				, navSpeakerController
				, navSessionController
				, mvc
				, pvc
				, navTwitterController
				, navBlogController};
			
			this.SelectedViewController = navScheduleController;
			
			this.ViewControllers = u;
			
			this.MoreNavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
		}

	}
}
