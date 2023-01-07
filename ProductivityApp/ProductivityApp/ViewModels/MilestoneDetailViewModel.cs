using ProductivityApp.Models;
using ProductivityApp.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProductivityApp.ViewModels
{
    [QueryProperty(nameof(MilestoneId), nameof(MilestoneId))]
    class MilestoneDetailViewModel : BaseViewModel
    {
        private string milestoneId;
        private string label;
        private string description;
        public string Id { get; set; }
        public IDataStore<Milestone> DataStore => DependencyService.Get<IDataStore<Milestone>>();

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

        public async void LoadMilestoneId(string milestoneId)
        {
            try
            {
                var Milestone = await DataStore.GetItemAsync(milestoneId);
                Id = Milestone.Id;
                Label = Milestone.Label;
                Description = Milestone.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Milestone");
            }
        }
    }
}
