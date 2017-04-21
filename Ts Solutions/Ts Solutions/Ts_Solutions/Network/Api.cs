using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ts_Solutions.Core.Models;
using System.Threading;
using System.Diagnostics;
using Ts_Solutions.Model;

namespace Ts_Solutions.Network
{
    public class Api
    {

		public async Task<ServiceResponse> GetUrlServices(CancellationToken cancelToken)
		{
			var response =
			  await
				ApprecotRestService.Instance.GetAsync($"{ApiUrls.BaseAddress}{ApiUrls.Endpoints}", cancelToken).
				  ConfigureAwait(false);

			if (response.EnsureSuccess())
			{
				try
				{
					var obj = JObject.Parse(response.Json);

					var urls = JsonConvert.DeserializeObject<UrlServices>(obj.ToString());
					if (urls != null) response.Data = urls;
				}
				catch (FormatException e)
				{
					Debug.WriteLine(e.Message);
					response.StatusCode = (int)ServiceStatusCode.MissingParameters;
				}
				catch (Exception e)
				{
					Debug.WriteLine(e.Message);
					response.StatusCode = (int)ServiceStatusCode.MissingParameters;
				}
			}
			return response;
		}

        public async Task<ServiceResponse> GetServicePoints(CancellationToken cancelToken)
		{
            var response =
                await
                    ApprecotRestService.Instance.GetAsync("http://www.anaxoft.com/ts/ts_sample.php", cancelToken).ConfigureAwait(false);

			if (response.EnsureSuccess())
			{
				try
				{
					var obj = JObject.Parse(response.Json);


					var stores = JsonConvert.DeserializeObject<List<ServicePoint>>(obj["service_points"].ToString());

					if (stores != null) response.Data = stores;
				}
				catch (FormatException e)
				{
					Debug.WriteLine(e.Message);
					response.StatusCode = (int)ServiceStatusCode.MissingParameters;
				}
				catch (Exception e)
				{
					Debug.WriteLine(e.Message);
					response.StatusCode = (int)ServiceStatusCode.MissingParameters;
				}
			}
            return response;
        }

		private ServiceResponse ErrorStatusCode(ServiceResponse serviceResponse)
		{
			serviceResponse.StatusCode = (int)ServiceStatusCode.MissingParameters;
			return serviceResponse;
		}
    }
}
