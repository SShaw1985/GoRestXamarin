using System.Collections.Generic;
using System.Threading.Tasks;
using GoRestXamarin.Models;

namespace GoRestXamarin.Interfaces
{
    public interface IGoRestService
    {
        Task<Root> GetUsers(int paging);

        Task<RootSingle> GetUser(int id);

        Task<RootSingle> CreateUser(Datum user);
    }
}
