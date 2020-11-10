using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GoRestXamarin.Interfaces;
using GoRestXamarin.Models;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace GoRestXamarin.ViewModels
{
    public class ViewUserViewModel : ViewModelBase
    {
        private IGoRestService GoRest { get; set; }

        public Datum UserModel { get; set; }

        public string ErrorMessage { get; set; }

        public ViewUserViewModel(IAppNavigationService navigationService, IPageDialogService pageDialogService,
                                 IDeviceService deviceService, IGoRestService goRestService)
            : base(navigationService, pageDialogService, deviceService)
        {
            GoRest = goRestService;
            Title = "GoRest Xamarin";
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {

            base.OnNavigatedTo(parameters);
            try
            {
                UserModel = null;
                if (parameters.ContainsKey("UserId"))
                {
                    int userId = parameters.GetValue<int>("UserId");
                    UserDialogs.Instance.ShowLoading();

                    var usr = await GoRest.GetUser(userId);
                    UserModel = usr.data;
                    if (UserModel == null)
                    {
                        await UserDialogs.Instance.AlertAsync("Error retrieving record");
                    }
                }
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

    }
}
