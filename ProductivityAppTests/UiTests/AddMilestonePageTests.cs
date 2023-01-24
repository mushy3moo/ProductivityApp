using NUnit.Framework;
using ProductivityAppTests.UiTests.Pages;
using System;
using Xamarin.UITest;
using ProductivityApp.Models;

namespace ProductivityAppTests.UiTests
{
    public class AddMilestonePageTests : BaseTestFixture
    {
        MilestonesPageHelper milestonesPage;
        AddMilestonePageHelper addMilestonePage;
        public AddMilestonePageTests(Platform platform) : base(platform) { }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            milestonesPage = new MilestonesPageHelper();
            addMilestonePage = new AddMilestonePageHelper();
            milestonesPage.NavigateMenu("Milestones");
            milestonesPage.SelectAddButton();
            addMilestonePage.AssertOnPage();
        }

        [Test]
        public void FindStuff()
        {
            app.Repl();
        }

        [Test]
        public void CancelButtonNavigatsToMilestone()
        {
            addMilestonePage.SelectCancelButton();

            milestonesPage.AssertOnPage();
        } 
        
        [Test]
        public void SaveButtonIsUnavailableWhenFormIsEmptyFilled()
        {
            addMilestonePage.SelectSaveButton();

            addMilestonePage.AssertOnPage();
        }

        [Test]
        public void SaveButtonIsUnavailableWhenFormIsPartiallyFilledWithTitle()
        {
            var expectedText = "Test Milestone";
            addMilestonePage.EnterTitleText(expectedText);
            
            addMilestonePage.SelectSaveButton();

            var resultText = addMilestonePage.GetText(expectedText).Text;
            addMilestonePage.AssertOnPage();
            Assert.That(expectedText, Is.EqualTo(resultText));
        }

        [Test]
        public void SaveButtonIsUnavailableWhenFormIsPartiallyFilledWithDescription()
        {
            var expectedText = "Test Description";
            addMilestonePage.EnterDescriptionText(expectedText);

            addMilestonePage.SelectSaveButton();

            var resultText = addMilestonePage.GetText(expectedText).Text;
            addMilestonePage.AssertOnPage();
            Assert.That(expectedText, Is.EqualTo(resultText));
        }

        [Test]
        public void SaveButtonCreatesMilestoneWhenFormIsFilled()
        {
            var expectedMilestone = new Milestone()
            {
                Label = "Test Milestone",
                Description = "Test Description",
                Deadline = DateTime.Now,
            };

            addMilestonePage.EnterTitleText(expectedMilestone.Label);
            addMilestonePage.EnterDescriptionText(expectedMilestone.Description);
            addMilestonePage.SelectDeadline(expectedMilestone.Deadline);
            addMilestonePage.SelectSaveButton();

            milestonesPage.AssertOnPage();
            var milestone = milestonesPage.GetMilestone(0);
            Assert.Multiple(() =>
            {
                Assert.That(milestone[0].Text, Is.EqualTo(expectedMilestone.Label));
                Assert.That(milestone[1].Text, Is.EqualTo(expectedMilestone.Description));
                Assert.That(milestone[2].Text, Is.EqualTo(expectedMilestone.Deadline.ToString()));
            });
        }
    }
}
