using System;
using Newtonsoft.Json;

namespace Ts_Solutions
{

	public class UrlServices
	{
		[JsonProperty("userimages_base_url")]
		public string UserImagesBaseUrl { get; set; }

		[JsonProperty("storeimages_base_url")]
		public string StoreImagesBaseUrl { get; set; }
	}
}
