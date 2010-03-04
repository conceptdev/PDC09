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
    public class SessionsViewController : UIViewController
    {
        private UITableView tableView;
		private Database _database;
		private List<Session> _sessions;
		private string _date;
		
		public SessionsViewController(string date)
		{
			_date = date;
		}
		
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

			_database = AppDelegate.SessionDatabase;
			_sessions = _database.GetSessions(_date).ToList();
			
			// no XIB !
			tableView = new UITableView()
			{
			    Delegate = new TableViewDelegate(this, _date, _sessions),
			    DataSource = new TableViewDataSource(this, _date, _sessions),
			    AutoresizingMask = UIViewAutoresizing.FlexibleHeight|
			                       UIViewAutoresizing.FlexibleWidth,
			    BackgroundColor = UIColor.White,
			};
			
			// Set the table view to fit the width of the app.
			tableView.SizeToFit();
			
			// Reposition and resize the receiver
			tableView.Frame = new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height);
			
			// Add the table view as a subview
			this.View.AddSubview(tableView);
        }

        private class TableViewDelegate : UITableViewDelegate
        {
			private SessionsViewController _svc;
			private string _date;
			private List<Session> _sessions;
            public TableViewDelegate(SessionsViewController controller, string date, List<Session> sessions)
            {
				_svc = controller;
				_date = date;
				_sessions = sessions;
            }

			/// <summary>
			/// If there are subsections in the hierarchy, navigate to those
			/// ASSUMES there are _never_ Categories hanging off the root in the hierarchy
			/// </summary>
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
            {
				Session s = _sessions[indexPath.Row];
                Console.WriteLine("SessionsViewController:TableViewDelegate.RowSelected: Label=");
				SessionViewController sessionView = new SessionViewController(s);
				sessionView.Title = s.Title;
				_svc.NavigationController.PushViewController(sessionView, true);
			}
        }

        private class TableViewDataSource : UITableViewDataSource
        {
            static NSString kCellIdentifier = new NSString ("MySessionIdentifier");

			private SessionsViewController _dvc;
			private List<Session> _sessions;
			private Dictionary<int, SessionCellController> controllers = null;
			
            public TableViewDataSource (SessionsViewController controller, string date, List<Session> sessions)
            {
				_dvc = controller;
				_sessions = sessions;
				controllers = new Dictionary<int, SessionCellController>();
            }

            public override int RowsInSection (UITableView tableview, int section)
            {
                return _sessions.Count;
            }

            public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = tableView.DequeueReusableCell (kCellIdentifier);
				Session s = _sessions[indexPath.Row];
				SessionCellController cellController = null;

                if (cell == null)
                {
					Console.WriteLine("no cell");
					cellController = new SessionCellController();
					NSBundle.MainBundle.LoadNib("SessionCellController", cellController, null);
					cell = cellController.Cell;
					cell.Tag = Environment.TickCount;
					controllers.Add(cell.Tag, cellController);			
				}
				else
				{
					Console.WriteLine("reuse cell");
					cellController = controllers[cell.Tag];
				}
				Console.WriteLine("setting disclosure");
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				Console.WriteLine("loading data");
				try
				{
				cellController.SessionTitle = s.Title;
				cellController.Subtitle = s.Location;
				cellController.Time = s.StartTime;
				cellController.EndTime = s.EndTime;
				} catch (Exception){}
                return cell;
            }
        }
    }
}
