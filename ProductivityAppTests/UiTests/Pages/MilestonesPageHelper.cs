using System;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
using ProductivityApp.Models;

namespace ProductivityAppTests.UiTests.Pages
{
    public class MilestonesPageHelper : BasePageHelper
    {
        private readonly string addButton;
        private readonly Query pageTitle;
        private readonly Query milestoneElements;
        private readonly int milestoneEnumCount;

        public MilestonesPageHelper()
        {
            pageTitle = c => c.Marked("Milestones");
            addButton = "Add";
            milestoneElements = c => c.ClassFull("LabelAppCompatRenderer");
            milestoneEnumCount = ClassIndex.GetNames(typeof(ClassIndex)).Length;
        }































        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = pageTitle,
            iOS = pageTitle
        };

        public void SelectAddButton(TimeSpan? timeout = default)
        { 
            SelectElement(addButton, timeout);
        }
        
        public void SelectMilestone(int index, TimeSpan? timeout = default)
        {
            SelectElement(c => c.ClassFull("LabelAppCompatRenderer").Index(index), timeout);
        }

        public MilestoneModel GetMilestone(int index, TimeSpan? timeout = default)
        {
            var allMilestones = GetElements(milestoneElements, timeout);
            var indexMod = (index * milestoneEnumCount);

            var label = allMilestones[(int)ClassIndex.label + indexMod].Text;
            var description = allMilestones[(int)ClassIndex.description + indexMod].Text;
            var deadline = allMilestones[(int)ClassIndex.deadline + indexMod].Text;

            var milestone = new MilestoneModel()
            {
                Label = label,
                Description = description,
                Deadline = DateTime.Parse(deadline)
            };

            return milestone;
        }
    }
}
