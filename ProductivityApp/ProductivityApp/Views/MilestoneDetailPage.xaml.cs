using Autofac;
using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.ViewModels;
using Xamarin.Forms;

namespace ProductivityApp.Views
{
	public partial class MilestoneDetailPage : ContentView
	{
        private readonly IDataStore<Milestone> _dataStore;
        public MilestoneDetailPage()
		{
			InitializeComponent();
            using (var scope = App.container.BeginLifetimeScope())
            {
                _dataStore = scope.Resolve<IDataStore<Milestone>>();
            }
            BindingContext = new MilestoneDetailViewModel(_dataStore);
		}
	}
}