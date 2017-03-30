using System;
using System.Collections.Generic;
using Foundation;
using ObjCRuntime;
using Ts_Solutions.Model;
using UIKit;

namespace Ts_Solutions.iOS
{
	public class StoresTableSource : UITableViewSource
	{
		string CellIdentifier = "StoresTableViewCell";
		List<ServicePoint> _servicePoints { get; set; }
		BaseController _owner;

		//public WeakReference Owner { get; set; }

		public StoresTableSource(List<ServicePoint> servicePoints, BaseController owner)
		{
			_servicePoints = servicePoints;
			_owner = owner;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 112;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			try
			{
				tableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
				var cell = tableView.DequeueReusableCell(CellIdentifier) as StoresTableViewCell;
				if (cell == null)
				{
					var views = NSBundle.MainBundle.LoadNib(CellIdentifier, tableView, null);
					cell = Runtime.GetNSObject(views.ValueAt(0)) as StoresTableViewCell;
				}
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
				//_height = cell.UpdateCell(Stores[indexPath.Row], GrandOwner.Target as BaseController);
				//GetHeightForRow(tableView, indexPath);
				cell.Update(_servicePoints[indexPath.Row], _owner);
				return cell;
			}
			catch (Exception exp)
			{
				return new UITableViewCell();
			}
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			if (_servicePoints != null)
				return _servicePoints.Count;
			else
				return 0;
		}
	}
}
