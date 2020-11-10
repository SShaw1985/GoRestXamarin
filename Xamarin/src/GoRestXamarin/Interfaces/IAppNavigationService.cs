using System;
using System.Threading.Tasks;
using Prism.Navigation;

namespace GoRestXamarin.Interfaces
{
    public interface IAppNavigationService : INavigationService
    {
        Task PopToRootAsync(NavigationParameters parameters);
    }

}
