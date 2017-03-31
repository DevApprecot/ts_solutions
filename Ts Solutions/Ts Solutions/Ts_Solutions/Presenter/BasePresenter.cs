using Ts_Solutions.IView;

namespace Ts_Solutions.Presenter
{
    public class BasePresenter
	{
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
