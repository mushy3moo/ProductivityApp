using System;
using NUnit.Framework;
using Xamarin.UITest;
using System.Linq;
using ProductivityAppTests.UiTests.Pages;
using ProductivityApp.Models;

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
        }

        /*
        [Test]
        public void FindStuff()
        {
            app.Repl();
        }
        */

        [Test]
        public void MenuLoadsMilestonePage()
        {
            milestonesPage.NavigateMenu("Milestones");

            milestonesPage.AssertOnPage();
        }

        [Test]
        public void MilestoneAddButtonLoadsNewMilestonePage()
        {
            milestonesPage.NavigateMenu("Milestones");
            milestonesPage.SelectAddButton();

            addMilestonePage.AssertOnPage();
        }


        [Test]
        public void MilestonesButtonLoadsMilestonePage()
        {
            var testTitle = "Test Milestone";
            var testDescription = "Test Description";

            app.Tap(c => c.Class("AppCompatImageButton"));

            app.WaitForElement(c => c.Marked("Milestones"), "Timed out waiting for milestones page", TimeSpan.FromSeconds(30));
            app.Tap(c => c.Marked("Milestones"));

            app.WaitForElement(c => c.Marked("Add"), "Timed out waiting for add button on milestones page", TimeSpan.FromSeconds(30));
            app.Tap(c => c.Marked("Add"));

            app.WaitForElement(c => c.Id("Tilte"), "Timed out waiting for new milestone page", TimeSpan.FromSeconds(30));
            app.Tap(c => c.Marked("Label"));
            app.EnterText(testTitle);

            app.Tap(c => c.Marked("Description"));
            app.EnterText(testDescription);

            app.Tap(c => c.Marked("Save"));

            app.WaitForElement(c => c.Id("MilestonesPage"));
        }
    }
}
