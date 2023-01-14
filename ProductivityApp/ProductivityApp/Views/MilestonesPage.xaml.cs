using ProductivityApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductivityApp.Views
{
	public partial class MilestonesPage : ContentPage
	{
        MilestonesViewModel _viewModel;

        public MilestonesPage()
        {
            InitializeComponent();
            
            BindingContext = _viewModel = new MilestonesViewModel();
            var entry = new Entry();
            AutomationProperties.SetIsInAccessibleTree(entry, true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}