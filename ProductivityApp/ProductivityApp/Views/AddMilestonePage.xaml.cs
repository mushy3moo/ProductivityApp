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
        private readonly IService<Milestone> _dataStore;
        public AddMilestonePage()
		{
			InitializeComponent();
            using (var scope = App.container.BeginLifetimeScope())
            {
                _dataStore = scope.Resolve<IService<Milestone>>();
            }
            BindingContext = new AddMilestoneViewModel(_dataStore, AttachmentStack);
            
        }
	}
}