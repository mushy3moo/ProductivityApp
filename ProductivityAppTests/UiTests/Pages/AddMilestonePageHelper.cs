using NUnit.Framework;
using System;
using Xamarin.UITest.Queries;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace ProductivityAppTests.UiTests.Pages
{
    public class AddMilestonePageHelper : BasePageHelper
    {
        readonly Query titleTextbox;
        readonly Query descriptionTextbox;
        readonly Query deadlineTextbox;
        readonly Query saveButton;
        readonly Query cancelButton;

        public AddMilestonePageHelper() 
        {
            titleTextbox = c => c.ClassFull("FormsAppCompatEditText").Index((int)ClassIndex.label);
            descriptionTextbox = c => c.ClassFull("FormsAppCompatEditText").Index((int)ClassIndex.description);
            deadlineTextbox = c => c.Class("PickerEditText");
            saveButton = c => c.Marked("Save");
            cancelButton = c => c.Marked("Cancel");
        }
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = cancelButton,
            iOS = cancelButton
        };

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

        public void SelectDeadline(DateTime selectDate, TimeSpan? timeout = default)
        {
            SelectElement(deadlineTextbox, timeout);
            app.Query(x => x.Class("DatePicker").Invoke("updateDate", selectDate.Year, selectDate.Month, selectDate.Day));
            app.Tap("OK");
        }

        public void SelectCancelButton(TimeSpan? timeout = default)
        {
            SelectElement(cancelButton, timeout);
        }

        public void SelectSaveButton(TimeSpan? timeout = default)
        {
            SelectElement(saveButton, timeout);
        }

        public string GetText(string element, TimeSpan? timeout = default)
        {
            return GetElementFromQuery(element, 0, timeout).Text;
        }
    }
}
