using System;
using NUnit.Framework;
using Xamarin.UITest;
using ProductivityApp.Models;

namespace ProductivityAppTests.UiTests
{
    public class MilestonePageTests : BaseTestFixture
    {
        public MilestonePageTests(Platform platform) : base(platform) { }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            AppManager.StartApp();

            milestonesPage.NavigateMenu("Milestones");
        }

        [Test]
        public void NavigationMenuLoadsMilestonePage()
        {
            milestonesPage.AssertOnPage();
        }

        [Test]
        public void TappingMilestoneLoadsEditMilestonePage()
        {
            milestonesPage.SelectAddButton();

            addMilestonePage.AssertOnPage();
            addMilestonePage.EnterTitleText("Test Milestone");
            addMilestonePage.EnterDescriptionText("Description of Milestone");
            addMilestonePage.SelectSaveButton();

            milestonesPage.AssertOnPage();
            milestonesPage.SelectMilestone(0);

            editMilestonePage.AssertOnPage();
        }

        [Test]
        public void AddButtonLoadsAddMilestonePage()
        {
            milestonesPage.SelectAddButton();

            addMilestonePage.AssertOnPage();
        }
    }
}
