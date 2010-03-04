using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
    public class DaysViewController : UITableViewController
    {
        private UITableView tableView;
		private Database _database;
		private List<SessionDate> _dates;
		
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

			_database = AppDelegate.SessionDatabase;
			_dates = _database.GetDates().ToList();
			
			
			UIImageView imageView = new UIImageView(UIImage.FromFile("BackgroundMonospace.png"));
			imageView.Frame = new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height);
			imageView.UserInteractionEnabled = true;
			
			// no XIB !
			tableView = new UITableView()
			{
			    Delegate = new TableViewDelegate(this, _dates),
			    DataSource = new TableViewDataSource(this, _dates),
			    AutoresizingMask = UIViewAutoresizing.FlexibleHeight|
			                       UIViewAutoresizing.FlexibleWidth,
			    BackgroundColor = UIColor.Clear,
				Frame = new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height),
			};
			// Set the table view to fit the width of the app.
			//tableView.SizeToFit();
			// Reposition and resize the receiver
			//tableView.Frame = new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height);
			// Add the table view as a subview
			//this.View.AddSubview(tableView);
			
			
			imageView.AddSubview(tableView);
			this.View.AddSubview(imageView);
        }

        private class TableViewDelegate : UITableViewDelegate
        {
			private DaysViewController _dvc;
			private List<SessionDate> _dates;
            public TableViewDelegate(DaysViewController controller, List<SessionDate> dates)
            {
				_dvc = controller;
				_dates = dates;
            }

			/// <summary>
			/// If there are subsections in the hierarchy, navigate to those
			/// ASSUMES there are _never_ Categories hanging off the root in the hierarchy
			/// </summary>
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
            {
				string date = _dates[indexPath.Row].Date;
                Console.WriteLine("DaysViewController:TableViewDelegate.RowSelected: Label="+date);
				
				SessionsViewController sessionsView = new SessionsViewController(date);
				sessionsView.Title = date;
				_dvc.NavigationController.PushViewController(sessionsView,true);
			}
        }

        private class TableViewDataSource : UITableViewDataSource
        {
            static NSString kCellIdentifier = new NSString ("MyDateIdentifier");

			private DaysViewController _dvc;
			private List<SessionDate> _dates;
            public TableViewDataSource (DaysViewController controller, List<SessionDate> dates)
            {
				_dvc = controller;
				_dates = dates;
            }

            public override int RowsInSection (UITableView tableview, int section)
            {
                return _dates.Count;
            }

            public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = tableView.DequeueReusableCell (kCellIdentifier);
                if (cell == null)
                {
                    cell = new UITableViewCell (UITableViewCellStyle.Default, kCellIdentifier);
                }
				string date = _dates[indexPath.Row].Date;
				
				
				
                cell.TextLabel.Text = date;
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                return cell;
            }
        }
		private string GetDisplayDate(string date)
		{
			string output = date;
			try 
			{
				string[] array = date.Split('-');
				int day = Convert.ToInt16(array[0]);
				string month = array[1];
				int year = DateTime.Now.Year;
				DateTime dt1 = Convert.ToDateTime(month + " 01, 1900");
				DateTime dt = new DateTime(year, dt1.Month, day);
				
				output = String.Format("{0} {1} {2}",dt.DayOfWeek, day, dt.ToString("MMMM"));
			}
			catch (Exception)
			{}
			return output;
		}
    }
}
