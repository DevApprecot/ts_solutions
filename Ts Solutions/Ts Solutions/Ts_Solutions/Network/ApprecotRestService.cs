using ModernHttpClient;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ts_Solutions.Model;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace Ts_Solutions.Network
{
    public class ApprecotRestService
    {
        public static readonly HttpClient Client = new HttpClient(new NativeMessageHandler());

        private static readonly ApprecotRestService _instance = new ApprecotRestService();

        static ApprecotRestService()
        {

        }

        private ApprecotRestService()
        {

        }

        public static ApprecotRestService Instance => _instance;

        public async Task<ServiceResponse> GetAsync(string url, CancellationToken cancelToken)
        {
			string json = string.Empty;
			var serviceResponse = new ServiceResponse();

            try
            {
				using (var response = await Client.GetAsync(url, cancelToken).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    json = await response.Content.ReadAsStringAsync();

					var obj = JObject.Parse(json);
					serviceResponse.StatusCode = (int)obj["rs"];
                }
            }
			catch(TaskCanceledException e)
			{
				Debug.WriteLine(e.Message);
				serviceResponse.StatusCode = (int)ServiceStatusCode.Cancelled;
			}
            catch (HttpRequestException e)
            {
				Debug.WriteLine(e.Message);
				serviceResponse.StatusCode = (int)ServiceStatusCode.Offline;
            }
            catch (WebException e)
            {
				Debug.WriteLine(e.Message);
				serviceResponse.StatusCode = (int)ServiceStatusCode.MissingParameters;
            }
			catch (Exception e)
            {
				Debug.WriteLine(e.Message);
				serviceResponse.StatusCode = (int)ServiceStatusCode.MissingParameters;
            }


			serviceResponse.Json = json;
			return serviceResponse;
        }
    }
}
