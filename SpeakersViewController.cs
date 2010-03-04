using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;

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
    public class SpeakersViewController : UIViewController
    {
        private UITableView tableView;
		
		private Database _database;
		private List<Speaker> _speakers;
		
		/// <summary>
		/// 
		/// </summary>
		/// <remarks>
		/// Background image idea from 
		/// http://mikebluestein.wordpress.com/2009/10/05/setting-an-image-background-on-a-uitableview-using-monotouch/
		/// </remarks>
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
			
			_database = AppDelegate.SessionDatabase;
			_speakers = _database.GetSpeakers().ToList();

			UIImageView imageView = new UIImageView(UIImage.FromFile("BackgroundMonospace.png"));
			imageView.Frame = new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height);
			imageView.UserInteractionEnabled = true;
			
			// no XIB !
			tableView = new UITableView()
			{
			    Delegate = new TableViewDelegate(this, _speakers),
			    DataSource = new TableViewDataSource(_speakers),
			    AutoresizingMask = UIViewAutoresizing.FlexibleHeight|
			                       UIViewAutoresizing.FlexibleWidth,
			    BackgroundColor = UIColor.Clear,
				Frame = new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height-100)
			};
			
			imageView.AddSubview(tableView);
			this.View.AddSubview(imageView);
        }

        private class TableViewDelegate : UITableViewDelegate
        {
			private SpeakersViewController _svc;
			private List<Speaker> _speakers;
			
            public TableViewDelegate(SpeakersViewController controller, List<Speaker> speakers)
            {
				_svc = controller;
				_speakers = speakers;
            }

			/// <summary>
			/// If there are subsections in the hierarchy, navigate to those
			/// ASSUMES there are _never_ Categories hanging off the root in the hierarchy
			/// </summary>
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
            {
				Speaker s = _speakers[indexPath.Row];
				SpeakerBioViewController bioView = new SpeakerBioViewController(s);
				bioView.Title = s.Name;
				_svc.NavigationController.PushViewController(bioView, true);
			}
        }

        private class TableViewDataSource : UITableViewDataSource
        {
            static NSString kCellIdentifier = new NSString ("MySpeakerIdentifier");

			private List<Speaker> _speakers;
            public TableViewDataSource (List<Speaker> speakers)
            {
				_speakers = speakers;
            }

            public override int RowsInSection (UITableView tableview, int section)
            {
                return _speakers.Count;
            }

            public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = tableView.DequeueReusableCell (kCellIdentifier);
                if (cell == null)
                {
                    cell = new UITableViewCell (UITableViewCellStyle.Default, kCellIdentifier);
                }
                cell.TextLabel.Text = _speakers[indexPath.Row].Name;
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                return cell;
            }
        }
    }
}
