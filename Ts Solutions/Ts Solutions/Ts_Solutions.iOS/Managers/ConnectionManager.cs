using System;
using Ts_Solutions.Interfaces;

namespace Ts_Solutions.iOS
{

	public class ConnectionManager : IConnectionManager
	{
		BaseController _controller;

		public ConnectionManager(BaseController controller)
		{
			_controller = controller;
		}

		public bool IsNetworkAvailable()
		{
			return _controller.IsOnline();
		}
	}
}
