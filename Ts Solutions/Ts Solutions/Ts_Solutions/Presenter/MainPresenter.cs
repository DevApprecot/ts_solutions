using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ts_Solutions.Interfaces;
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

        public void Start()
        {
            _view.ShowLoading();

            if(!_connectionManager.IsNetworkAvailable())
            {
                _view.HideLoading();
                _view.ShowNoNet();
                return;
            }

            GetServicePoints();
        }

        private void GetServicePoints()
        {

        }
    }
}
