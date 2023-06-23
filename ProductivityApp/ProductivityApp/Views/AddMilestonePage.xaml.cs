using Autofac;
using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.ViewModels;
using Xamarin.Forms;

namespace ProductivityApp.Views
{
	public partial class AddMilestonePage : ContentPage
	{
        public AddMilestonePage()
		{
			InitializeComponent();
            using (var scope = App.container.BeginLifetimeScope())
            {
                var _dataStore = scope.Resolve<IDataService<MilestoneModel>>();
                BindingContext = new AddMilestoneViewModel(_dataStore, AttachmentStack);
            }
        }
	}
}