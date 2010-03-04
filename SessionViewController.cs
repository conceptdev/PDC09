using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Monospace2
{
	/// <summary>
	/// Parts of speech include Noun, Adjective, Phrase. This view
	/// shows all the related works in these groupings.
	/// </summary>
	/// <remarks>
	/// Uses UIWebView since we want to format the text display (with HTML)
	/// </remarks>
	public class SessionViewController : UIViewController
	{
		Session _session;
		
		public SessionViewController (Session session) : base()
		{
			_session = session;
			Console.WriteLine(_session.ToString());
		}
		
		public UIWebView webView;
		
		public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
			
			// no XIB !
			webView = new UIWebView()
			{
				ScalesPageToFit = false,
			};
			webView.LoadHtmlString(FormatText(), new NSUrl());
			
			
			// Set the web view to fit the width of the app.
            webView.SizeToFit();
			
            // Reposition and resize the receiver
            webView.Frame = new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height-80);

            // Add the table view as a subview
            this.View.AddSubview(webView);
		}		
		/// <summary>
		/// Format the parts-of-speech text for UIWebView
		/// </summary>
		private string FormatText()
		{
			StringBuilder sb = new StringBuilder();
			
			sb.Append("<style>" +
				"body,b,i,p,h2{font-family:Helvetica;}" +
				"h1,h2{color:#0090C6;}" +
				"</style>");
			
			sb.Append("<h2>"+_session.Title+"</h2>"+ Environment.NewLine);
			
			sb.Append("<b style='color:#666666'>"+_session.StartTime+" - " 
			          +_session.EndTime+"</b><br/>"+ Environment.NewLine);
			
			sb.Append("<i style='color:#666666'>"+_session.Location+"</i><br/>"+ Environment.NewLine);
			sb.Append("<br/>"+ Environment.NewLine);
			sb.Append("<span style='color:#347235'>"+_session.Brief+"</span>"+ Environment.NewLine);
			
			if (!string.IsNullOrEmpty(_session.Url))
			sb.Append("<br/>Link: <a href='"+_session.Url+"'>"+_session.Url+"</a><br/>"+ Environment.NewLine);
				
			return sb.ToString();
		}
	}

}
