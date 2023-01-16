using System;
using Xamarin.UITest.Queries;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace ProductivityAppTests.UiTests.Pages
{
    public class AddMilestonePageHelper : BasePageHelper
    {
        readonly Query title;
        readonly Query titleTextbox;
        readonly Query description;
        readonly Query descriptionTextbox;
        readonly Query deadline;
        readonly Query cancelButton;
        readonly Query saveButton;

        public AddMilestonePageHelper() 
        {
            title = c => c.Marked("Title");
            titleTextbox = c => c.ClassFull("FormsAppCompatEditText").Index(0);
            description = c => c.Marked("Description");
            descriptionTextbox = c => c.ClassFull("FormsAppCompatEditText").Index(1);
            deadline = c => c.Marked("Deadline");
            cancelButton = c => c.Marked("Cancel");
            saveButton = c => c.Marked("Save");
        }
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = title,
            iOS = title
        }; 
        
        public void SelectTitleText(TimeSpan? timeout = default)
        {
            SelectElement(titleTextbox, timeout);            
        }

        public void SelectDescriptionText(TimeSpan? timeout = default)
        {
            SelectElement(descriptionTextbox, timeout);
        }

        public void SelectDeadline(TimeSpan? timeout = default)
        {
            SelectElement(deadline, timeout);
        }

        public void SelectCancelButton(TimeSpan? timeout = default)
        {
            SelectElement(cancelButton, timeout);
        }

        public void SelectSaveButton(TimeSpan? timeout = default)
        {
            SelectElement(saveButton, timeout);
        }

        public AppResult GetTitleText(string element, TimeSpan? timeout = default)
        {
            return GetElementFromQuery(element, 0, timeout);
        }        
        
        public AppResult GetDescriptionText(string element, TimeSpan? timeout = default)
        {
            return GetElementFromQuery(element, 1, timeout);
        }
    }
}
