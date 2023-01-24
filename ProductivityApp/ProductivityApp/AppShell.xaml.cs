using ProductivityApp.ViewModels;
using ProductivityApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProductivityApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MilestoneDetailPage), typeof(MilestoneDetailPage));
            Routing.RegisterRoute(nameof(AddMilestonePage), typeof(AddMilestonePage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
