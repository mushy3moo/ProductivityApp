using ProductivityApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ProductivityApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}