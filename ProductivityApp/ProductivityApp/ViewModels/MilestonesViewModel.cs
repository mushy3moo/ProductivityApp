using ProductivityApp.Models;
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
        private MilestoneModel _selectedMilestone;
        private readonly IDataService<MilestoneModel> _dataStore;
        public ObservableCollection<MilestoneModel> Milestones { get; }
        public Command LoadMilestonesCommand { get; }
        public Command AddMilestoneCommand { get; }
        public Command<MilestoneModel> MilestoneTapped { get; }

        public MilestonesViewModel(IDataService<MilestoneModel> dataStore)
        {
            _dataStore = dataStore;

            Milestones = new ObservableCollection<MilestoneModel>();
            LoadMilestonesCommand = new Command(async () => await ExecuteLoadMilestonesCommand());
            MilestoneTapped = new Command<MilestoneModel>(OnMilestoneSelected);
            AddMilestoneCommand = new Command(OnAddMilestone);
        }

        public MilestoneModel SelectedMilestone
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

        private async void OnMilestoneSelected(MilestoneModel Milestone)
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