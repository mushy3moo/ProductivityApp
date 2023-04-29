﻿using ProductivityApp.Models;
using ProductivityApp.Services;
using ProductivityApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProductivityApp.ViewModels
{
    public class MilestonesViewModel : BaseViewModel
    {
        private Milestone _selectedMilestone;
        private readonly IService<Milestone> _dataStore;
        public ObservableCollection<Milestone> Milestones { get; }
        public Command LoadMilestonesCommand { get; }
        public Command AddMilestoneCommand { get; }
        public Command<Milestone> MilestoneTapped { get; }

        public MilestonesViewModel(IService<Milestone> dataStore)
        {
            _dataStore = dataStore;

            Milestones = new ObservableCollection<Milestone>();
            LoadMilestonesCommand = new Command(async () => await ExecuteLoadMilestonesCommand());
            MilestoneTapped = new Command<Milestone>(OnMilestoneSelected);
            AddMilestoneCommand = new Command(OnAddMilestone);
        }

        public Milestone SelectedMilestone
        {
            get => _selectedMilestone;
            set
            {
                SetProperty(ref _selectedMilestone, value);
                OnMilestoneSelected(value);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedMilestone = null;
        }

        private async void OnAddMilestone(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddMilestonePage));
        }

        private async void OnMilestoneSelected(Milestone Milestone)
        {
            if (Milestone == null)
                return;

            // This will push the EditMilestonePage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EditMilestonePage)}?{nameof(EditMilestoneViewModel.MilestoneId)}={Milestone.Id}");
        }

        private async Task ExecuteLoadMilestonesCommand()
        {
            IsBusy = true;

            try
            {
                Milestones.Clear();
                var milestones = await _dataStore.GetItemsAsync(true);
                foreach (var milestone in milestones)
                {
                    Milestones.Add(milestone);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}