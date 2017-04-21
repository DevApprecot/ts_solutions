using System;
using System.Threading;
using System.Threading.Tasks;
using Ts_Solutions.Interfaces;
using Ts_Solutions.IViews;
using Ts_Solutions.Network;
using Ts_Solutions.Presenter;

namespace Ts_Solutions.Presenters
{
	public class SplashPresenter : BasePresenter
	{
		private const int Timeout = 5000; // milliseconds
		private readonly ISplashView _view;
		private CancellationTokenSource _cancelTokenSource;

		public SplashPresenter(ISplashView view) : base(view)
		{
			_view = view;
		}

		public async Task LoadUrlServices()
		{
			_view.SetLoading(true);

			if (_cancelTokenSource != null)
				_cancelTokenSource.Cancel();

			_cancelTokenSource = new CancellationTokenSource(Timeout);

			var response = await (new Api().GetUrlServices(_cancelTokenSource.Token).ConfigureAwait(false));

			if (response.EnsureSuccess())
			{
				SetupEndpoints(response.Data as UrlServices);
				_view.NavigateToMainScreen();
			}
			else
				OnError(response.GetFailureCode());
		}

		private void SetupEndpoints(UrlServices endpoints)
		{
			ApiUrls.UserImagesEndpoint = endpoints.UserImagesBaseUrl;
			ApiUrls.StoreImagesEndpoint = endpoints.StoreImagesBaseUrl;
		}
	}
}
