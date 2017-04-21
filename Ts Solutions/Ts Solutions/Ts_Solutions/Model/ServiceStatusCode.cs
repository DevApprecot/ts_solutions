using System;
namespace Ts_Solutions.Core.Models
{
	public enum ServiceStatusCode
	{
		Cancelled = 0,
		Success = 1,
		MissingParameters = -1,
		Offline = -11,
		WorkOrderNotExist = -13
	}
}
