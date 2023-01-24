using Autofac;
using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductivityApp.Views
{
	public partial class AddMilestonePage : ContentPage
	{
		public Milestone Milestone { get; set; }
        private readonly IDataStore<Milestone> _dataStore;
        public AddMilestonePage()
		{
			InitializeComponent();
            using (var scope = App.container.BeginLifetimeScope())
            {
                _dataStore = scope.Resolve<IDataStore<Milestone>>();
            }
            BindingContext = new AddMilestoneViewModel(_dataStore);
        }
	}
}