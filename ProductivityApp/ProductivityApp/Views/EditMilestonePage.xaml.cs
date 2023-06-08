using Autofac;
using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.ViewModels;
using Xamarin.Forms;

namespace ProductivityApp.Views
{
	public partial class EditMilestonePage : ContentPage
	{
        public EditMilestonePage()
		{
			InitializeComponent();
            using (var scope = App.container.BeginLifetimeScope())
            {
                var _dataStore = scope.Resolve<IService<MilestoneModel>>();
                BindingContext = new EditMilestoneViewModel(_dataStore);
            }

            double marginValue = Application.Current.MainPage.Height * 0.03;
            titleEntry.Margin = new Thickness(0, 0, 0, marginValue);
            descriptionEntry.Margin = new Thickness(0, 0, 0, marginValue);
            deadlineDate.Margin = new Thickness(0, 0, 0, marginValue);
        }
	}
}