using System;

namespace ProductivityAppTests.UiTests.Pages
{
    public class MilestonesPageHelper : BasePageHelper
    {
        readonly string addButton;

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = c => c.Marked("Milestones"),
            iOS = c => c.Marked("Milestones")
        };

        public MilestonesPageHelper()
        {
            addButton = "Add";
        }

        public void SelectAddButton(TimeSpan? timeout = default)
        { 
            SelectElement(addButton, timeout);
        }
    }
}
