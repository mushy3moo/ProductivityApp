using NUnit.Framework;
using ProductivityAppTests.UiTests.Pages;
using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

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
            addMilestonePage.SelectTitleText();
            app.EnterText(expectedText);

            addMilestonePage.SelectSaveButton();
            var resultText = addMilestonePage.GetTitleText(expectedText).Text;

            addMilestonePage.AssertOnPage();
            Assert.That(expectedText, Is.EqualTo(resultText));
        }

        [Test]
        public void SaveButtonIsUnavailableWhenFormIsPartiallyFilledWithDescription()
        {
            var expectedText = "Test Description";
            addMilestonePage.SelectDescriptionText();
            app.EnterText(expectedText);

            addMilestonePage.SelectSaveButton();
            var resultText = addMilestonePage.GetDescriptionText(expectedText).Text;

            addMilestonePage.AssertOnPage();
            Assert.That(expectedText, Is.EqualTo(resultText));
        }

        [Test]
        public void SaveButtonCreatesMilestoneWhenFormIsFilled()
        {
            var expectedTitle = "Test Milestone";
            var expectedDescription = "Test Description";
            AppResult[] milestone;
 
            addMilestonePage.SelectTitleText();
            app.EnterText(expectedTitle);
            addMilestonePage.SelectDescriptionText();
            app.EnterText(expectedDescription);
            addMilestonePage.SelectSaveButton();
            milestone = milestonesPage.GetMilestone(0);

            milestonesPage.AssertOnPage();
            Assert.That(milestone[0].Text, Is.EqualTo(expectedTitle));
            Assert.That(milestone[1].Text, Is.EqualTo(expectedDescription));
        }
    }
}
