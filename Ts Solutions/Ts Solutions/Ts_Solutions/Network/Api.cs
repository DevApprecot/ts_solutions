using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ts_Solutions.Model;

namespace Ts_Solutions.Network
{
    public class Api
    {
        public async Task<ServiceResponse> GetServicePoints()
        {
            var serviceResponse = new ServiceResponse();

            var response =
                await
                    ApprecotRestService.Instance.GetAsync("").ConfigureAwait(false);

            //if (!response.IsValidString())
            //    return ErrorStatusCode(serviceResponse);
            try
            {
                var obj = JObject.Parse(response);

                serviceResponse.StatusCode = (int)obj["rs"];

                //if (serviceResponse.StatusCode != (int)ServiceStatusCode.Success) return serviceResponse;

                var stores = JsonConvert.DeserializeObject<List<ServicePoint>>(obj["stores"].ToString());

                if (stores != null) serviceResponse.Data = stores;
            }
            catch (FormatException)
            {
                //serviceResponse.StatusCode = (int)ServiceStatusCode.NotValidJson;
            }
            catch (Exception)
            {
                //serviceResponse.StatusCode = (int)ServiceStatusCode.GenericError;
            }
            return serviceResponse;
        }
    }
}
