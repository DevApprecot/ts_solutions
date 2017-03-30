using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ts_Solutions.Interfaces;
using Ts_Solutions.Model;
using Ts_Solutions.Network;
using Ts_Solutions.View;

namespace Ts_Solutions.Presenter
{
	public class MainPresenter : BasePresenter
    {
        private IMainView _view;
		CancellationTokenSource _cancelTokenSource;

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
                _view.SetMarkers(response.Data as List<ServicePoint>);
            }
            else
                OnError(response.GetFailureCode());
        }
    }
}
