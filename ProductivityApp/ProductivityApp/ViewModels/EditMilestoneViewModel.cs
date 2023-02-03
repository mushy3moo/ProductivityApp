using ProductivityApp.Models;
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
        public string Id { get; set; }
        private readonly IDataStore<Milestone> _dataStore;

        public EditMilestoneViewModel(IDataStore<Milestone> dataStore)
        {
            _dataStore = dataStore;
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
    }
}