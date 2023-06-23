using Autofac;
using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.ViewModels;
using System;
using Xamarin.Forms;

namespace ProductivityApp.Views
{
    public partial class MilestonesPage : ContentPage
	{
        private readonly MilestonesViewModel _viewModel;
        private readonly IDataService<MilestoneModel> _dataStore;

        public MilestonesPage()
        {
            InitializeComponent();
            using (var scope = App.container.BeginLifetimeScope())
            {
                _dataStore = scope.Resolve<IDataService<MilestoneModel>>();
            }
            BindingContext = _viewModel = new MilestonesViewModel(_dataStore); ;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}