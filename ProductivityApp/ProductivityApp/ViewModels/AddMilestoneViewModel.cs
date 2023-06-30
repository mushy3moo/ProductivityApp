using ProductivityApp.Helpers;
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
        private List<AttachmentModel> attachments;
        private StackLayout attackmentStack;
        private readonly AttachmentService attachmentService;
        private readonly IDataService<MilestoneModel> _dataStore;
        public Command AddAttachmentCommand { get; }
        public Command DeleteAttachmentCommand { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public AddMilestoneViewModel(IDataService<MilestoneModel> dataStore, StackLayout attackmentStackLayout)
        {
            _dataStore = dataStore;
            attachments = new List<AttachmentModel>();
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

        public List<AttachmentModel> Attachments
        {
            get => attachments;
            set => SetProperty(ref attachments, value);
        }

        private async void OnAddAttachment()
        {
            var file = await FilePicker.PickAsync();
            var attachment = new AttachmentModel();
            using(var attachmentHelper = new AttachmentHelper())
            {
                attachment = await attachmentHelper.CreateAttachmentAsync(file.FileName);
            }
            if (file != null)
            {
                var attachmentFrame = new Frame
                {
                    BackgroundColor = Color.LightSlateGray,
                    Padding = 15,
                    Margin = 10
                };
                var stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };
                var image = new Image
                {
                    Source = attachment.FileTypeIcon,
                    Margin = new Thickness(3, 0, 7, 0),
                    HorizontalOptions = LayoutOptions.Start
                };
                var label = new Label
                {
                    Text = file.FileName,
                    TextColor = Color.White,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };
                var deleteButton = new ImageButton
                {
                    AutomationId = "Delete " + file.FileName,
                    Source = "icon_close.png",
                    BackgroundColor = Color.LightSlateGray,
                    HorizontalOptions = LayoutOptions.End,
                    Command = DeleteAttachmentCommand,
                    CommandParameter = attachmentFrame
                };
                stackLayout.Children.Add(image);
                stackLayout.Children.Add(label);
                stackLayout.Children.Add(deleteButton);
                attachmentFrame.Content = stackLayout;
                attackmentStack.Children.Add(attachmentFrame);

                var isSuccessful = await attachmentService.AddItemAsync(attachment);
                if (!isSuccessful)
                {
                    var errorMessage = "";
                    throw new Exception(errorMessage);
                }
            }
        }

        private async void OnDeleteAttachment(object obj)
        {
            var frame = obj as Frame;
            var stackLayout = frame.Children.FirstOrDefault() as StackLayout;
            var label = stackLayout.Children[1] as Label;
            var filename = label.Text;

            var isSuccessful = await attachmentService.DeleteItemAsync(filename);
            if(isSuccessful)
            {
                attackmentStack.Children.Remove(frame);
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

            MilestoneModel newMilestone = new MilestoneModel()
            {
                Id = Guid.NewGuid().ToString(),
                Label = label,
                Description = Description,
                Deadline = Deadline,
                Attachments = Attachments
            };

            await _dataStore.AddItemAsync(newMilestone);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
