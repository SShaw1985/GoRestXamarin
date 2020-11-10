using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GoRestXamarin.Interfaces;
using GoRestXamarin.Models;
using GoRestXamarin.Views;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace GoRestXamarin.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private IGoRestService GoRest { get; set; }

        public ObservableCollection<Datum> Users { get; set; }
        public ObservableCollection<Datum> MasterList { get; set; }

        public string SearchTerm { get; set; }

        public bool ShowLoadMore { get; set; }

        public int Paging { get; set; } = 1;

        public ICommand SearchCommand { get { return new Command(Search); } }

        public ICommand LoadMoreCommand { get { return new Command(LoadMore); } }

        public ICommand GoToItemCommand { get { return new Command(GoToItem); } }

        public ICommand AddUserCommand { get { return new Command(AddUser); } }


        public string ErrorMessage { get; set; }

        public HomePageViewModel(IAppNavigationService navigationService, IPageDialogService pageDialogService,
                                 IDeviceService deviceService, IGoRestService goRestService)
            : base(navigationService, pageDialogService, deviceService)
        {
            GoRest = goRestService;
            Title = "GoRest Xamarin";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {

            base.OnNavigatedTo(parameters);

            GetData();
        }

        private async void GetData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading();
                Paging = 1;
                var model = await GoRest.GetUsers(Paging);
                MasterList = new ObservableCollection<Datum>(model.data);
                Search();
                ErrorMessage = string.Empty;
                if (Users.Count >= 999)
                {
                    ShowLoadMore = false;
                }
                else
                {
                    ShowLoadMore = true;
                }
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }


        private void Search()
        {
            if (SearchTerm != null)
            {
                Users = new ObservableCollection<Datum>(MasterList.Where(c => c.name.ToLower().Contains(SearchTerm.ToLower())).ToList());
            }
            else
            {
                Users = MasterList;
            }
        }

        private async void LoadMore(object obj)
        {

            UserDialogs.Instance.ShowLoading();
            Paging++;

            var model = await GoRest.GetUsers(Paging);
            foreach (var mod in model.data)
            {
                MasterList.Add(mod);
            }
            Search();


            Search();

            if (MasterList.Count >= 999)
            {
                ShowLoadMore = false;
            }
            UserDialogs.Instance.HideLoading();

        }


        private async void GoToItem(object obj)
        {
            int id = 0;
            int.TryParse(obj.ToString(), out id);
            if (id > 0)
            {

                UserDialogs.Instance.ShowLoading();
                NavigationParameters paramaters = new NavigationParameters();
                paramaters.Add("UserId", id);
                await _navigationService.NavigateAsync("ViewUser", paramaters);
                UserDialogs.Instance.HideLoading();
            }

        }

        private async void AddUser(object obj)
        {
            UserDialogs.Instance.ShowLoading();
            await _navigationService.NavigateAsync("AddUser", null);
            UserDialogs.Instance.HideLoading();
        }

    }
}
