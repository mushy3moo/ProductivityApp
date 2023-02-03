using System;
using NUnit.Framework;
using Xamarin.UITest;
using ProductivityAppTests.UiTests.Pages;
using ProductivityApp.Models;

namespace ProductivityAppTests.UiTests
{
    public class MilestonePageTests : BaseTestFixture
    {
        private MilestonesPageHelper milestonesPage;
        private AddMilestonePageHelper addMilestonePage;
        private EditMilestonePageHelper editMilestonePage;
        private readonly Platform platform;

        public MilestonePageTests(Platform platform) : base(platform) 
        {
            this.platform = platform;
        }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            milestonesPage = new MilestonesPageHelper();
            addMilestonePage = new AddMilestonePageHelper();
            editMilestonePage = new EditMilestonePageHelper();
            milestonesPage.NavigateMenu("Milestones");
        }

        [Test]
        public void MenuLoadsMilestonePage()
        {
            milestonesPage.AssertOnPage();
        }

        [Test]
        public void MilestonePageCanDisplayMilestone()
        {
            var milestone = new Milestone 
            {
                Id = Guid.NewGuid().ToString(),
                Label = "Label",
                Description = "Description",
                Deadline= DateTime.Now
            };

            milestonesPage.CreateMilestone(milestone, platform);
            milestonesPage.RefreshPage();

            milestonesPage.GetMilestone(0);
        }

        [Test]
        public void AddButtonLoadsAddMilestonePage()
        {
            milestonesPage.SelectAddButton();

            addMilestonePage.AssertOnPage();
        }

        [Test]
        public void TappingMilestoneLoadsEditMilestonePage()
        {
            //milestonesPage.SelectMilestone();
        }
    }
}
