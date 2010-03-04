using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Monospace2
{
	/// <summary>
	/// Display sponsor logos and links
	/// </summary>
	/// <remarks>
	/// Uses UIWebView since we want to format the text display (with HTML)
	/// </remarks>
	public class SponsorsViewController : UIViewController
	{
		public UITextView textView;
		public UIWebView webView;
		private string basedir;
		
		public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
			// no XIB !
			webView = new UIWebView()
			{
				ScalesPageToFit = false
			};
			
			basedir = Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			basedir = basedir.Replace("Documents", "PDC09.app");
			
			webView.LoadHtmlString(FormatText(), null);//new NSUrl(basedir+"/Sponsors/", true));
			
			// Set the web view to fit the width of the app.
            webView.SizeToFit();

            // Reposition and resize the receiver
            webView.Frame = new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height-90);

            // Add the table view as a subview
            this.View.AddSubview(webView);
		}		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			
			try {
				basedir = Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
				basedir = basedir.Replace("Documents", "PDC09.app");
				
				webView.LoadHtmlString(FormatText(), null);//new NSUrl(basedir+"/Sponsors/", true));
			} catch (Exception ex) {Console.WriteLine(ex);}
		}
		/// <summary>
		/// Format the parts-of-speech text for UIWebView
		/// </summary>
		private string FormatText()
		{
			StringBuilder sb = new StringBuilder();
			
			var basedir = Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			basedir = basedir.Replace("Documents", "PDC09.app");
			basedir = Path.Combine(basedir, "Sponsors");
			return File.ReadAllText(Path.Combine(basedir, "Sponsors.htm"));
		}
	}

}


















