using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Monospace2
{
	/// <summary>
	/// First view that users see - lists the top level of the hierarchy xml
	/// </summary>
	/// <remarks>
	/// LOADS data from the xml files into public properties (deserialization)
	/// then we pass around references to the MainViewController so other
	/// ViewControllers can access the data.
	/// 
	/// http://softwareandservice.wordpress.com/2009/09/21/building-a-rss-reader-iphone-app-using-monotouch/
	/// </remarks>
    [Register]
    public class BlogViewController : UIViewController
    {
        private UITableView tableView;
		public IList<RSSEntry> BlogFeed;
		
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

			// no XIB !
			tableView = new UITableView()
			{
			    Delegate = new TableViewDelegate(this),
			    DataSource = new TableViewDataSource(this),
			    AutoresizingMask = UIViewAutoresizing.FlexibleHeight|
			                       UIViewAutoresizing.FlexibleWidth,
			    BackgroundColor = UIColor.White,
			};
			
			// Set the table view to fit the width of the app.
			tableView.SizeToFit();
			
			// Reposition and resize the receiver
			tableView.Frame = new RectangleF (
			    0, 0, this.View.Frame.Width, this.View.Frame.Height);
			
			// Add the table view as a subview
			this.View.AddSubview(tableView);
		}

        private class TableViewDelegate : UITableViewDelegate
        {
			private BlogViewController rssvc;
			
            public TableViewDelegate(BlogViewController controller)
            {
				rssvc = controller;
            }

			/// <summary>
			/// If there are subsections in the hierarchy, navigate to those
			/// ASSUMES there are _never_ Categories hanging off the root in the hierarchy
			/// </summary>
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
            {
                Console.WriteLine("Blog Entry selected");
				RSSEntry fi = rssvc.BlogFeed[indexPath.Row];
				BlogEntryViewController revc = new BlogEntryViewController(fi.Title, fi.Content);
				revc.Title = fi.Title;
				rssvc.NavigationController.PushViewController(revc, true);
			}
        }

        private class TableViewDataSource : UITableViewDataSource
        {
            static NSString kCellIdentifier = new NSString ("MyIdentifier");

			private BlogViewController rssvc;
            public TableViewDataSource (BlogViewController controller)
            {
				rssvc = controller;
				//rssvc.BlogFeed = AtomRepository.GetFeeds("http://feeds.feedburner.com/MonospaceConference");
				//rssvc.BlogFeed = AtomRepository.GetFeeds("http://microsoftpdc.com/rss");
				rssvc.BlogFeed = RSSRepository.GetFeeds("http://microsoftpdc.com/rss");
            }

            public override int RowsInSection (UITableView tableview, int section)
            {
                return rssvc.BlogFeed.Count;
            }

            public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = tableView.DequeueReusableCell (kCellIdentifier);
                if (cell == null)
                {
                    cell = new UITableViewCell (UITableViewCellStyle.Default, kCellIdentifier);
                }
                cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				cell.TextLabel.Text = rssvc.BlogFeed[indexPath.Row].Title;
				
				UIFont f = UIFont.SystemFontOfSize(14.0f);
				cell.TextLabel.Font = f;
				cell.TextLabel.ContentMode = UIViewContentMode.ScaleToFill;
				cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				cell.TextLabel.Lines = 2;
				
                return cell;
            }
        }
    }
}
