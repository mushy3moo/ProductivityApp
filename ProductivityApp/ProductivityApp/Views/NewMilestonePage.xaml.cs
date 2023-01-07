using ProductivityApp.Models;
using ProductivityApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductivityApp.Views
{
	public partial class NewMilestonePage : ContentPage
	{
		public Milestone Milestone { get; set; }

		public NewMilestonePage()
		{
			InitializeComponent();
            BindingContext = new NewMilestoneViewModel();
        }
	}
}