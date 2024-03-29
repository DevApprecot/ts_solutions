﻿using System.Threading.Tasks;

namespace Ts_Solutions.IView
{
    public interface IBaseView
	{
		void SetLoading(bool isLoading);
		void ShowMessage(string message);
		bool IsOnline();
        Task OnConnected();
	}
}
