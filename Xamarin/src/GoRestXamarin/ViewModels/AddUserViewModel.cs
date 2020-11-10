using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public class AddUserViewModel : ViewModelBase
    {
        private IGoRestService GoRest { get; set; }
        public const string EmailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";


        public Datum UserModel { get; set; }

        public ICommand SaveUserCommand { get { return new Command(Save); } }
        public string ErrorMessage { get; set; }

        public AddUserViewModel(IAppNavigationService navigationService, IPageDialogService pageDialogService,
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
                UserModel = new Datum();
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }


        private async void Save()
        {
            try
            {
                if (ValidateModel())
                {
                    UserDialogs.Instance.ShowLoading();
                    var result = await GoRest.CreateUser(UserModel);
                    if (result.code == 201)
                    {
                        UserDialogs.Instance.HideLoading();
                        await UserDialogs.Instance.AlertAsync("User Added Successfully");
                        await _navigationService.GoBackAsync();
                    }
                }
                else
                {

                    await UserDialogs.Instance.AlertAsync(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync("An error occurred while saving");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private bool ValidateModel()
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(UserModel.name))
            {
                sb.AppendLine("Please enter a name");
            }

            if (string.IsNullOrEmpty(UserModel.email) || !CheckValidEmail(UserModel.email))
            {
                sb.AppendLine("Please enter a valid email");
            }

            if (string.IsNullOrEmpty(UserModel.gender))
            {
                sb.AppendLine("Please select a gender");
            }

            if (string.IsNullOrEmpty(UserModel.status))
            {
                sb.AppendLine("Please enter a status");
            }

            ErrorMessage = sb.ToString();
            return string.IsNullOrEmpty(ErrorMessage);
        }


        public string ValidationMessage { get; set; }

        public bool CheckValidEmail(string value)
        {
            var str = value;
            bool isValid = false;
            isValid = Regex.IsMatch(str, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$", RegexOptions.IgnoreCase);
            return isValid;
        }

    }
}
