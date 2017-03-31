using System;
namespace Ts_Solutions.Model
{
	public enum ServiceStatusCode
	{
		Cancelled = 0,
		Success = 1,
		MissingParameters = -1,
		Offline = -11
	}

}
