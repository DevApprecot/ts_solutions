using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Ts_Solutions.Interfaces;
using Ts_Solutions.View;

namespace Ts_Solutions.Presenter
{
	public class BasePresenter
	{
		private readonly IConnectionManager _connectionManager;
		private readonly IBaseView _view;

		public BasePresenter(IBaseView view)
		{
			_view = view;
		}

		public virtual void OnError(string message)
		{
			_view.SetLoading(false);
			_view.ShowMessage(message);
		}
}

}
