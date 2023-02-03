using System;
using Xamarin.UITest.Queries;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace ProductivityAppTests.UiTests.Pages
{
    public class EditMilestonePageHelper : BasePageHelper
    {
        private readonly Query title;

        public EditMilestonePageHelper()
        {
            title = c => c.Marked("Edit Milestone");
        }

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = title,
            iOS = title
        };
    }
}
