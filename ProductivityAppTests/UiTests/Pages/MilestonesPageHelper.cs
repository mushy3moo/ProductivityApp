using System;
using Xamarin.UITest.Queries;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace ProductivityAppTests.UiTests.Pages
{
    public class MilestonesPageHelper : BasePageHelper
    {
        private readonly string addButton;
        private readonly Query milestoneElements;
        private readonly int milestoneEnumCount;
        public enum ClassIndex 
            {
                title = 0,
                description = 1,
                deadline = 2
            };

        public MilestonesPageHelper()
        {
            addButton = "Add";
            milestoneElements = c => c.ClassFull("LabelAppCompatRenderer");
            milestoneEnumCount = ClassIndex.GetNames(typeof(ClassIndex)).Length;
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

        public AppResult[] GetMilestone(int milestoneNum, TimeSpan? timeout = default)
        {
            var allMilestones = GetElements(milestoneElements, timeout);
            var indexMod = (milestoneNum * milestoneEnumCount);

            var milestone = new AppResult[milestoneEnumCount];
            milestone[(int)ClassIndex.title] = allMilestones[(int)ClassIndex.title + indexMod];
            milestone[(int)ClassIndex.description] = allMilestones[(int)ClassIndex.description + indexMod];
            milestone[(int)ClassIndex.deadline] = allMilestones[(int)ClassIndex.deadline + indexMod];

            return milestone;
        }
    }
}
