using ModernHttpClient;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<string> GetAsync(string url, CancellationToken cancelToken)
        {
            string json;

            try
            {
                using (var response = await Client.GetAsync(url).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    json = await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException)
            {
                json = string.Empty;
            }
            catch (WebException)
            {
                json = string.Empty;
            }
            catch (Exception)
            {
                json = string.Empty;
            }

            return json;
        }
    }
}
