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
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "lat")]
        public double Lat { get; set; }
        [JsonProperty(PropertyName = "lon")]
        public double Lon { get; set; }
       //public string Manager { get; set; }
       //public int AddressNumber { get; set; }
       //public string City { get; set; }
       //public string Country { get; set; }
       //public string PostalCode { get; set; }
    }
}
