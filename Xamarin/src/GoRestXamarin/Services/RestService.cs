using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GoRestXamarin.Interfaces;
using GoRestXamarin.Models;
using ModernHttpClient;

namespace GoRestXamarin.Services
{
    public class RestService : IRestService
    {
        public string BaseUri { get; }
        public string AccessToken { get; }

        public RestService()
        {
            BaseUri = "https://gorest.co.in/";
            AccessToken = "07cd4b79cdd709a6c56544fa5cde155a9161c0a591b51ab40677f5645fc9bd3f";
        }

        public async Task<ResponseWrapper> GetAsync(string uri)
        {
            using (var httpClient = CreateHttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(uri))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // Log
                    }
                    return await HandleResponse(response);
                }
            }
        }

        public async Task<ResponseWrapper> PostAsync(string uri, string body)
        {

            using (var httpClient = CreateHttpClient())
            {
                var httpContent = new StringContent(body, Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = await httpClient.PostAsync(uri, httpContent))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        // Log
                    }
                    return await HandleResponse(response);
                }
            }
        }

        public async Task<ResponseWrapper> PutAsync(string uri, string body)
        {
            using (var httpClient = CreateHttpClient())
            {
                var httpContent = new StringContent(body, Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = await httpClient.PutAsync(uri, httpContent))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // Log
                    }
                    return await HandleResponse(response);
                }
            }
        }

        public async Task<ResponseWrapper> DeleteAsync(string uri)
        {
            using (var httpClient = CreateHttpClient())
            {
                using (HttpResponseMessage response = await httpClient.DeleteAsync(uri))
                {
                    return await HandleResponse(response);
                }
            }
        }

        public async Task<ResponseWrapper> PatchAsync(string uri, string body)
        {

            using (var httpClient = CreateHttpClient())
            {
                var httpContent = new StringContent(body, Encoding.UTF8, "application/json-patch+json");
                using (HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), uri) { Content = httpContent }))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // Log
                    }
                    return await HandleResponse(response);
                }
            };

        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient(new NativeMessageHandler());
            httpClient.BaseAddress = new Uri(BaseUri);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            return httpClient;
        }

        private async Task<ResponseWrapper> HandleResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            return new ResponseWrapper
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = content,
            };
        }
    }
}