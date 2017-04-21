using System;
using Newtonsoft.Json;

namespace Ts_Solutions.Model
{
	public class WorkStatus
	{
		[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }
		[JsonProperty(PropertyName = "code")]
		public int Code { get; set; }
		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }
	}
}
