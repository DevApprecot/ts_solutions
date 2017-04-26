using System;
namespace Ts_Solutions
{
	public static class ApiUrls
	{
		// production branch
		//public const string BaseAddress = "https://anaxoft.com/ts_production/api";

		//development branch
		public const string BaseAddress = "https://anaxoft.com/ts_production/api";

		public static string ServicesEndpoint = string.Empty;
		public static string UserImagesEndpoint = string.Empty;
		public static string StoreImagesEndpoint = string.Empty;

		public const string Endpoints = "/config";

		public const string ServicePoints = "/stores/all";
		public const string WorkStatus = "/workorders/code";
	}
}
