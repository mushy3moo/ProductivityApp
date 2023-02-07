using System;
using Xamarin.UITest.Queries;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace ProductivityAppTests.UiTests.Pages
{
    public class EditMilestonePageHelper : BasePageHelper
    {
        private readonly Query backButton;
        private readonly Query pageTitle;
        private readonly Query titleTextbox;
        private readonly Query descriptionTextbox;
        private readonly Query saveButton;
        private readonly Query deleteButton;
        private readonly Query alertTitle;
        private readonly Query confirmSaveText;
        private readonly Query confirmDeleteText;
        private readonly Query confirmYesButton;
        private readonly Query confirmNoButton;


        public EditMilestonePageHelper()
        {
            backButton = c => c.Class("AppCompatImageButton");
            pageTitle = c => c.Marked("Edit Milestone");
            titleTextbox = c => c.ClassFull("FormsAppCompatEditText").Index((int)ClassIndex.label);
            descriptionTextbox = c => c.ClassFull("FormsAppCompatEditText").Index((int)ClassIndex.description);
            saveButton = c => c.Marked("Save");
            deleteButton = c => c.Marked("Delete");
            alertTitle = c => c.Marked("Confirm");
            confirmSaveText = c => c.Marked("Are you sure you want to save this milestone?");
            confirmDeleteText = c => c.Marked("Are you sure you want to delete this milestone?");
            confirmYesButton = c => c.Marked("Yes");
            confirmNoButton = c => c.Marked("No");
        }

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = pageTitle,
            iOS = pageTitle
        };

        public string GetTitleText()
        {
            return GetElementFromQuery(titleTextbox).Text;
        }

        public string GetDescriptionText()
        {
            return GetElementFromQuery(descriptionTextbox).Text;
        }
        
        public string GetAlertTitle()
        {
            return GetElementFromQuery(alertTitle).Text;
        }

        public void SelectBackButton(TimeSpan? timeout = default)
        {
            SelectElement(backButton);
        }
        
        public void SelectSaveButton(TimeSpan? timeout = default)
        {
            SelectElement(saveButton);
        }        

        public void SelectDeleteButton(TimeSpan? timeout = default)
        {
            SelectElement(deleteButton);
        }

        public void SelectYesButton(TimeSpan? timeout = default)
        {
            SelectElement(confirmYesButton);
        }

        public void SelectNoButton(TimeSpan? timeout = default)
        {
            SelectElement(confirmNoButton);
        }

        public void EnterTitleText(string text, TimeSpan? timeout = default)
        {
            SelectElement(titleTextbox, timeout);
            app.EnterText(text);
        }

        public void EnterDescriptionText(string text, TimeSpan? timeout = default)
        {
            SelectElement(descriptionTextbox, timeout);
            app.EnterText(text);
        }

        public void ClearTitleText(TimeSpan? timeout = default)
        {
            SelectElement(titleTextbox, timeout);
            app.ClearText();
        }


        public void ClearDescriptionText(TimeSpan? timeout = default)
        {
            SelectElement(descriptionTextbox, timeout);
            app.ClearText();
        }
    }
}
