using ProductivityApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ProductivityApp.Views
{
	public partial class MilestoneDetailPage : ContentView
	{
		public MilestoneDetailPage ()
		{
			InitializeComponent ();
			BindingContext = new MilestoneDetailViewModel();
		}
	}
}