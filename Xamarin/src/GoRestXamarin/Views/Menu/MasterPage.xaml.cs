using System;
using System.Collections.Generic;
using GoRestXamarin.Models;
using Prism.Navigation;
using Xamarin.Forms;

namespace GoRestXamarin.Views
{
    public partial class MasterPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public MasterPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            this.IsPresented = false;
        }

        public bool IsPresentedAfterNavigation
        {
            get { return Device.Idiom != TargetIdiom.Phone; }
        }

    }
}