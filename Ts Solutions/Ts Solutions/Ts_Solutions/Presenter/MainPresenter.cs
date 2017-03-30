using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ts_Solutions.Interfaces;
using Ts_Solutions.IView;
using Ts_Solutions.Model;
using Ts_Solutions.Network;

namespace Ts_Solutions.Presenter
{
	public class MainPresenter : BasePresenter
    {
        private IMainView _view;
		CancellationTokenSource _cancelTokenSource;
		ServicePointsViewType _viewType = ServicePointsViewType.Map;
		List<ServicePoint> _servicePoints;

		public MainPresenter(IMainView view):base(view)
        {
            _view = view;
        }

		public async Task LoadServicePoints()
        {
			if (_cancelTokenSource == null)
				_cancelTokenSource = new CancellationTokenSource();
			
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
			//api service call
			var result="Here the status from service";
			_view.ShowStatus(result);
		}
    }
}
