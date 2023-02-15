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

        private string GetImage(string contentType)
        {
            var source = "";

            var fileType = contentType.ToLower().Split('/')[0];
            if(fileType == "image")
            {
                source = "icon_image.png";
            }
            else if (fileType == "audio")
            {
                source = "icon_audio.png";
            }
            else if(fileType == "video")
            {
                source = "icon_video.png";
            }
            else
            {
                switch (contentType.ToLower())
                {
                    case "text/plain":
                        source = "icon_txt.png";
                        break;
                    case "text/html":
                        source = "icon_html.png";
                        break;
                    case "text/css":
                        source = "icon_css.png";
                        break;
                    case "application/javascript":
                        source = "icon_java.png";
                        break;
                    case "application/json":
                        source = "icon_json.png";
                        break;
                    case "application/xml":
                        source = "icon_xml.png";
                        break;
                    case "application/pdf":
                        source = "icon_pdf.png";
                        break;
                    case "application/msword":
                    case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                        source = "icon_doc.png";
                        break;
                    case "application/vnd.ms-excel":
                    case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                        source = "icon_xls.png";
                        break;
                    case "application/vnd.ms-powerpoint":
                    case "application/vnd.openxmlformats-officedocument.presentationml.presentation":
                        source = "icon_ppt.png";
                        break;
                    default:
                        source = "icon_none.png";
                        break;
                }
            }
            return source;
        }

        private async void OnAddAttachment()
        {
            var file = await FilePicker.PickAsync();

            if (file != null)
            {
                await attachmentService.AddItemAsync(file.FullPath);

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
                    Source = GetImage(file.ContentType),
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
