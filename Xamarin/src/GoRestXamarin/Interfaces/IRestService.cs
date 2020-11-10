using System;
using System.Threading.Tasks;
using GoRestXamarin.Models;
using Newtonsoft.Json;

namespace GoRestXamarin.Interfaces
{
    public interface IRestService
    {
        Task<ResponseWrapper> GetAsync(string uri);

        Task<ResponseWrapper> PostAsync(string uri, string body);

        Task<ResponseWrapper> PutAsync(string uri, string body);

        Task<ResponseWrapper> DeleteAsync(string uri);

        Task<ResponseWrapper> PatchAsync(string v, string body);
    }
}
