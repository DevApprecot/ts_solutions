using Newtonsoft.Json;

namespace Ts_Solutions.Model
{
	public class ServiceResponse
	{
		[JsonProperty(PropertyName = "rs")]
		public int StatusCode { get; set; }
		[JsonProperty(PropertyName = "rm")]
		public string Message { get; set; }
		public object Data { get; set; }
	}
}
