using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ts_Solutions.Interfaces;
using Ts_Solutions.Model;
using Ts_Solutions.Network;
using Ts_Solutions.View;

namespace Ts_Solutions.Presenter
{
    public class MainPresenter
    {
        private IMainView _view;
        private IConnectionManager _connectionManager;

        public MainPresenter(IMainView view, IConnectionManager connectionManager)
        {
            _view = view;
            _connectionManager = connectionManager;
        }

		public async Task Start()
        {
            _view.ShowLoading();

            if(!_connectionManager.IsNetworkAvailable())
            {
                _view.HideLoading();
                _view.ShowNoNet();
                return;
            }

            await GetServicePoints();
        }

		private async Task<List<ServicePoint>> GetServicePoints()
        {
			var response = await(new Api().GetServicePoints());

			if (response.EnsureSuccess()) 
				return response.Data as List<ServicePoint>;

			return new List<ServicePoint>(); /// error view?

        }
    }
}
