using System;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace ProductivityAppTests.UiTests.Pages
{
    public class AddMilestonePageHelper : BasePageHelper
    {
        readonly Query label;
        readonly Query description;
        readonly Query deadline;
        readonly Query cancelButton;
        readonly Query saveButton;

        public AddMilestonePageHelper() 
        {
            label = c => c.Marked("Label");
            description = c => c.Marked("Description");
            deadline = c => c.Marked("Deadline");
            cancelButton = c => c.Marked("Cancel");
            saveButton = c => c.Marked("Save");
        }
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = label,
            iOS = label
        };

        public void SelectLabel(TimeSpan? timeout = default)
        {
            SelectElement(label, timeout);
        }
    }
}
