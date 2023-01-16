using System;
using System.Collections.Generic;
using System.Threading;
using Xamarin.UITest.Queries;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace ProductivityAppTests.UiTests.Pages
{
    public class MilestonesPageHelper : BasePageHelper
    {
        readonly string addButton;
        readonly Query title;
        readonly Query description;
        readonly Query deadline;

        public MilestonesPageHelper()
        {
            addButton = "Add";
            title = c => c.Marked("Title");
            description = c => c.Marked("Description");
            deadline = c => c.Marked("Deadline");
        }
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = c => c.Marked("Milestones"),
            iOS = c => c.Marked("Milestones")
        };

        public void SelectAddButton(TimeSpan? timeout = default)
        { 
            SelectElement(addButton, timeout);
        }

        public AppResult[] GetMilestone(int index, TimeSpan? timeout = default)
        {
            var milestone = new AppResult[3];
            milestone[0] = GetElements(title)[index];
            milestone[1] = GetElements(description)[index];
            milestone[2] = GetElements(deadline)[index];

            return milestone;
        }
    }
}
