using ProductivityApp.Models;
using ProductivityApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProductivityApp.ViewModels
{
    public class AddMilestoneViewModel : BaseViewModel
    {
        private string label;
        private string description;
        private DateTime deadline;
        private List<Attachment> attachments;
        private StackLayout attackmentStack;
        private readonly AttachmentService attachmentService;
        private readonly IService<Milestone> _dataStore;
        public Command AddAttachmentCommand { get; }
        public Command DeleteAttachmentCommand { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public AddMilestoneViewModel(IService<Milestone> dataStore, StackLayout attackmentStackLayout)
        {
            _dataStore = dataStore;
            attachments = new List<Attachment>();
            attachmentService = new AttachmentService();
            attackmentStack = attackmentStackLayout;

            AddAttachmentCommand = new Command(OnAddAttachment);
            DeleteAttachmentCommand = new Command(OnDeleteAttachment);
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

        public List<Attachment> Attachments
        {
            get => attachments;
            set => SetProperty(ref attachments, value);
        }

        private async void OnAddAttachment()
        {
            var file = await FilePicker.PickAsync();
            
            if(file != null)
            {
                await attachmentService.AddItemAsync(file.FullPath);

                var stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };
                var attachmentLabel = new Label { Text = file.FileName };
                var deleteAttachmentButton = new Button
                { 
                    Text = "Delete Attachment",
                    Command = DeleteAttachmentCommand,
                    CommandParameter = stackLayout
                };
                stackLayout.Children.Add(attachmentLabel);
                stackLayout.Children.Add(deleteAttachmentButton);
                attackmentStack.Children.Add(stackLayout);
            }
        }

        private async void OnDeleteAttachment(object obj)
        {
            var stackLayout = obj as StackLayout;
            var label = stackLayout.Children.FirstOrDefault() as Label;
            var filename = label.Text;

            var isSuccessful = await attachmentService.DeleteItemAsync(filename);
            if(isSuccessful)
            {
                attackmentStack.Children.Remove(stackLayout);
            }
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(label)
                && !string.IsNullOrWhiteSpace(description);
        }

        private async void OnSave()
        {
            var data = await attachmentService.GetItemsAsync();
            Attachments = data.ToList();

            Milestone newMilestone = new Milestone()
            {
                Id = Guid.NewGuid().ToString(),
                Label = label,
                Description = Description,
                Deadline = Deadline,
                Attachments = Attachments
            };

            await _dataStore.AddItemAsync(newMilestone);
            _dataStore.SaveItemsLocal();

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
