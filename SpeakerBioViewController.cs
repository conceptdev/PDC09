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
	public class SpeakerBioViewController : UIViewController
	{
		Speaker _speaker;
		
		public SpeakerBioViewController (Speaker speaker) : base()
		{
			_speaker = speaker;
			Console.WriteLine(_speaker.ToString());
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
            webView.Frame = new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height-100);

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
			
			sb.Append("<h2>"+_speaker.Name+"</h2>"+ Environment.NewLine);
			
			sb.Append("<i>"+(_speaker.Position??"")+", "+(_speaker.Company??"")+"</i><br/>"+ Environment.NewLine);
			if (!string.IsNullOrEmpty(_speaker.Bio))
			{
				sb.Append("<br />"+_speaker.Bio+ "<br/>"+ Environment.NewLine);
				
			}
			sb.Append("<br />");
			if (!string.IsNullOrEmpty(_speaker.BlogUrl))
				sb.Append("Blog: <a href='"+_speaker.BlogUrl+"'>"+_speaker.BlogUrl+"</a><br/>"+ Environment.NewLine);
			
			if (!string.IsNullOrEmpty(_speaker.TwitterName))
				sb.Append("Twitter: <a href='http://twitter.com/"+_speaker.TwitterName.Replace("@","")+"'>"+_speaker.TwitterName+"</a><br/>"+ Environment.NewLine);
			
			return sb.ToString();
		}
	}

}
