using System;
namespace ApprecotTemplate.Core.Models
{
	public enum ServiceStatusCode
	{
		Cancelled = 0,
		Success = 1,
		MissingParameters = -1,
		Offline = -11
	}

}
