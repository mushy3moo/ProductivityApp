using ProductivityApp.Models;
using ProductivityApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProductivityApp.ViewModels
{
    public class NewMilestoneViewModel : BaseViewModel
    {
        private string label;
        private string description;
        public IDataStore<Milestone> DataStore => DependencyService.Get<IDataStore<Milestone>>();

        public NewMilestoneViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(label)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Label
        {
            get => label;
            set => SetProperty(ref label, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Milestone newMilestone = new Milestone()
            {
                Id = Guid.NewGuid().ToString(),
                Label = label,
                Description = Description
            };

            await DataStore.AddItemAsync(newMilestone);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
