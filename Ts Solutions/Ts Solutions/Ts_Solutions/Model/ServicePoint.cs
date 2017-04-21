using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ts_Solutions.Model
{
	public class ServicePoint
	{
	  	[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }
		[JsonProperty(PropertyName = "country_id")]
		public int CountryId { get; set; }
		[JsonProperty(PropertyName = "country")]
		public string Country { get; set; }
		[JsonProperty(PropertyName = "city")]
		public string City { get; set; }
		[JsonProperty(PropertyName = "street")]
		public string Street { get; set; }
		[JsonProperty(PropertyName = "street_number")]
		public string StreetNumber { get; set; }
		[JsonProperty(PropertyName = "post_code")]
		public int PostCode { get; set; }
		[JsonProperty(PropertyName = "phone_number")]
		public string Phone { get; set; }
		[JsonProperty(PropertyName = "map_latitude")]
		public double Lat { get; set; }
		[JsonProperty(PropertyName = "map_longitude")]
		public double Lon { get; set; }
		[JsonProperty(PropertyName = "is_active")]
		public int Active { get; set; }
	}
}
