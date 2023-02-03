using ProductivityApp.Models;
using ProductivityApp.Services;
using System;
using Xamarin.Forms;

namespace ProductivityApp.ViewModels
{
    public class AddMilestoneViewModel : BaseViewModel
    {
        private string label;
        private string description;
        private DateTime deadline;
        private readonly IDataStore<Milestone> _dataStore;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public AddMilestoneViewModel(IDataStore<Milestone> dataStore)
        {
            _dataStore = dataStore;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
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

        public DateTime Deadline
        {
            get => deadline;
            set => SetProperty(ref deadline, value);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(label)
                && !String.IsNullOrWhiteSpace(description);
        }

        private async void OnSave()
        {
            Milestone newMilestone = new Milestone()
            {
                Id = Guid.NewGuid().ToString(),
                Label = label,
                Description = Description,
                Deadline = Deadline
            };

            await _dataStore.AddItemAsync(newMilestone);
            _dataStore.SaveItemsLocal();

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
