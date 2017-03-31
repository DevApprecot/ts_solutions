using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ts_Solutions.IView;
using Ts_Solutions.Model;
using Ts_Solutions.Network;

namespace Ts_Solutions.Presenter
{
    public class MainPresenter : BasePresenter
    {
        private const int Timeout = 5000; // milliseconds

        private IMainView _view;
		private CancellationTokenSource _cancelTokenSource;
		private ServicePointsViewType _viewType = ServicePointsViewType.Map;
        private List<ServicePoint> _servicePoints = new List<ServicePoint>();

		public MainPresenter(IMainView view):base(view)
        {
            _view = view;
        }

		public async Task LoadServicePoints()
        {
            if (_cancelTokenSource != null)
                _cancelTokenSource.Cancel();

		    _cancelTokenSource = new CancellationTokenSource(Timeout);
			
			_view.SetLoading(true);
			var response = await(new Api().GetServicePoints(_cancelTokenSource.Token));

            if (response.EnsureSuccess())
            {
                _view.SetLoading(false);
				_servicePoints = response.Data as List<ServicePoint>;
				if (_viewType== ServicePointsViewType.Map)
					_view.SetMarkers(_servicePoints);
				else
					_view.SetList(_servicePoints);
					                                 
            }
            else
                OnError(response.GetFailureCode());
        }

        public void Call(string phone)
        {
            _view.CallNumber(phone);
        }

		public void ChangeViewTypeClicked()
		{
			if (_viewType == ServicePointsViewType.List)
			{
				_view.SetMarkers(_servicePoints);
				_viewType = ServicePointsViewType.Map;
			}
			else
			{
				_view.SetList(_servicePoints);
				_viewType = ServicePointsViewType.List;
			}
		}

		public void ButtonCheckTapped(string code)
		{
			var rand = new Random();
			switch (rand.Next(1, 5))
			{
				case 1:
					_view.ShowStatus("Your item has been received");
					break;
				case 2:
					_view.ShowStatus("Your item is in the service department");
					break;
				case 3:
					_view.ShowStatus("Your item is fixed");
					break;
				default:
					_view.ShowStatus("Work order not found");
					break;
			}
		}
    }
}
