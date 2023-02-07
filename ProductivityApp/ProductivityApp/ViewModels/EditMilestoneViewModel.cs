﻿using ProductivityApp.Models;
using ProductivityApp.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProductivityApp.ViewModels
{
    [QueryProperty(nameof(MilestoneId), nameof(MilestoneId))]
    public class EditMilestoneViewModel : BaseViewModel
    {
        private string milestoneId;
        private string label;
        private string description;
        private DateTime deadline;
        private readonly IDataStore<Milestone> _dataStore;
        public string Id { get; set; }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }

        public EditMilestoneViewModel(IDataStore<Milestone> dataStore)
        {
            _dataStore = dataStore;
            SaveCommand = new Command(OnSave, ValidateSave);
            DeleteCommand = new Command(OnDelete);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string MilestoneId
        {
            get
            {
                return milestoneId;
            }
            set
            {
                milestoneId = value;
                LoadMilestoneId(value);
            }
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

        private async void LoadMilestoneId(string milestoneId)
        {
            try
            {
                var Milestone = await _dataStore.GetItemAsync(milestoneId);
                Id = Milestone.Id;
                Label = Milestone.Label;
                Description = Milestone.Description;
                Deadline = Milestone.Deadline;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Milestone");
            }
        }

        private async void OnDelete()
        {
            var alertResult = await Application.Current.MainPage.DisplayAlert("Confirm", "Are you sure you want to delete this milestone?", "Yes", "No");

            if (alertResult)
            {
                await _dataStore.DeleteItemAsync(MilestoneId);

                // This will pop the current page off the navigation stack
                await Shell.Current.GoToAsync("..");
            }
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(label)
                && !string.IsNullOrWhiteSpace(description);
        }

        private async void OnSave()
        {
            var alertResult = await Application.Current.MainPage.DisplayAlert("Confirm", "Are you sure you want to save this milestone?", "Yes", "No");

            if(alertResult)
            {
                Milestone newMilestone = new Milestone()
                {
                    Id = milestoneId,
                    Label = label,
                    Description = Description,
                    Deadline = Deadline
                };

                await _dataStore.UpdateItemAsync(newMilestone);
                _dataStore.SaveItemsLocal();

                // This will pop the current page off the navigation stack
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}