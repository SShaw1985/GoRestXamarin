using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using GoRestXamarin.Interfaces;
using GoRestXamarin.Models;
using MonkeyCache;
using Newtonsoft.Json;

namespace GoRestXamarin.Services
{
    public class GoRestService : IGoRestService
    {
        private IRestService _RestService { get; set; }
        private IBarrel Sql;
        private JsonSerializerSettings SerializerSettings { get; set; }

        public GoRestService(IRestService rest)
        {
            _RestService = rest;

        }

        public async Task<Root> GetUsers(int paging)
        {
            var res = await _RestService.GetAsync("/public-api/users?page=" + paging);
            if (res != null && res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Root>(res.Content);
            }
            else
            {
                return null;
            }
        }

        public async Task<RootSingle> CreateUser(Datum user)
        {
            var res = await _RestService.PostAsync("/public-api/users", JsonConvert.SerializeObject(user));
            if (res != null && res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    return JsonConvert.DeserializeObject<RootSingle>(res.Content);
                }
                catch (Exception ex)
                {
                    var error = JsonConvert.DeserializeObject<RootError>(res.Content);
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("An error occurred while saving");
                    foreach (var err in error.data)
                    {
                        sb.AppendLine(err.field + err.message);
                    }
                    await UserDialogs.Instance.AlertAsync(sb.ToString());
                    return null;

                }
            }
            else
            {
                return null;
            }
        }


        public async Task<RootSingle> GetUser(int id)
        {
            var res = await _RestService.GetAsync("/public-api/users/" + id);
            if (res != null && res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<RootSingle>(res.Content);
            }
            else
            {
                return null;
            }
        }
    }
}
