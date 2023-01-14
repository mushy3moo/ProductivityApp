using ProductivityApp.Models;
using ProductivityApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductivityApp.Views
{
	public partial class AddMilestonePage : ContentPage
	{
		public Milestone Milestone { get; set; }

		public AddMilestonePage()
		{
			InitializeComponent();
            BindingContext = new AddMilestoneViewModel();
        }
	}
}