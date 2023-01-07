using ProductivityApp.ViewModels;
using ProductivityApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProductivityApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(MilestoneDetailPage), typeof(MilestoneDetailPage));
            Routing.RegisterRoute(nameof(NewMilestonePage), typeof(NewMilestonePage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
