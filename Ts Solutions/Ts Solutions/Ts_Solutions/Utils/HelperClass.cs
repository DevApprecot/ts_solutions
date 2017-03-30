using System;
using Ts_Solutions.Model;

namespace Ts_Solutions
{
	public static class HelperClass
	{
		public static bool IsValidString(this string data)
		{
			return !string.IsNullOrEmpty(data);
		}

		public static bool EnsureSuccess(this ServiceResponse response)
		{
			return (response.StatusCode == (int)ServiceStatusCode.Success);
		}

		public static string GetFailureCode(this ServiceResponse response)
		{
			var code = (ServiceStatusCode)response.StatusCode;
			try
			{
				var j = int.Parse(code.ToString());
				return "UnknownError";
			}
			catch (FormatException)
			{
				return code.ToString();
			}
		}
	}
}
