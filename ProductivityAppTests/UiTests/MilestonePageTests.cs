using System;
using NUnit.Framework;
using Xamarin.UITest;
using ProductivityAppTests.UiTests.Pages;

namespace ProductivityAppTests.UiTests
{
    public class MilestonePageTests : BaseTestFixture
    {
        MilestonesPageHelper milestonesPage;
        AddMilestonePageHelper addMilestonePage;
        public MilestonePageTests(Platform platform) : base(platform) { }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            milestonesPage = new MilestonesPageHelper();
            addMilestonePage = new AddMilestonePageHelper();
            milestonesPage.NavigateMenu("Milestones");

        }

        [Test]
        public void MenuLoadsMilestonePage()
        {
            milestonesPage.AssertOnPage();
        }

        [Test]
        public void AddButtonLoadsAddMilestonePage()
        {
            milestonesPage.SelectAddButton();

            addMilestonePage.AssertOnPage();
        }
    }
}
