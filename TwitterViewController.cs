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
	/// ViewControllers can access the data.</remarks>
    [Register]
    public class TwitterViewController : UIViewController
    {
        private UITableView tableView;
		public IList<Tweet> TwitterFeed;
		
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
			
            Console.WriteLine("Is you're using the simulator, switch to it now.");
        }

        private class TableViewDelegate : UITableViewDelegate
        {
			private TwitterViewController tvc;
			
            public TableViewDelegate(TwitterViewController controller)
            {
				tvc = controller;
            }

			/// <summary>
			/// If there are subsections in the hierarchy, navigate to those
			/// ASSUMES there are _never_ Categories hanging off the root in the hierarchy
			/// </summary>
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
            {
                /*UITableViewCell cell = tableView.DequeueReusableCell (kCellIdentifier);
                if (cell == null)
                {
                    cell = new UITableViewCell (UITableViewCellStyle.Default, kCellIdentifier);
                }
                cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				cell.TextLabel.Text = rssvc.RssFeed[indexPath.Row].Title;
                return cell;*/
			}
        }

        private class TableViewDataSource : UITableViewDataSource
        {
            static NSString kCellIdentifier = new NSString ("MyIdentifier");

			private TwitterViewController tvc;
            public TableViewDataSource (TwitterViewController controller)
            {
				tvc = controller;
				tvc.TwitterFeed = TwitterRepository.GetFeeds("http://search.twitter.com/search.atom?q=@PDC09");
            }

            public override int RowsInSection (UITableView tableview, int section)
            {
                return tvc.TwitterFeed.Count;
            }

			/// <summary>
			/// 
			/// </summary>
			/// <remarks>
			/// Sets custom font
			/// http://www.go-mono.com/docs/index.aspx?link=P%3AMonoTouch.UIKit.UIFont.FamilyNames
			/// </remarks>
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = tableView.DequeueReusableCell (kCellIdentifier);
                if (cell == null)
                {
                    cell = new UITableViewCell (UITableViewCellStyle.Default, kCellIdentifier);
                }
                cell.Accessory = UITableViewCellAccessory.None;
				
				UIFont f = UIFont.SystemFontOfSize(12.0f);
				cell.TextLabel.Font = f;
				cell.TextLabel.ContentMode = UIViewContentMode.ScaleToFill;
				cell.TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				cell.TextLabel.Lines = 3;
				cell.TextLabel.Text = tvc.TwitterFeed[indexPath.Row].Title;
                return cell;
            }
        }
    }
}
