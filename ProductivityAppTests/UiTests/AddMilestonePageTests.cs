using System;
using NUnit.Framework;
using Xamarin.UITest;
using ProductivityApp.Models;

namespace ProductivityAppTests.UiTests
{
    public class AddMilestonePageTests : BaseTestFixture
    {
        public AddMilestonePageTests(Platform platform) : base(platform) { }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            AppManager.StartApp();

            milestonesPage.NavigateMenu("Milestones");
            milestonesPage.SelectAddButton();

            addMilestonePage.AssertOnPage();
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

            addMilestonePage.AssertOnPage();
            var actualText = addMilestonePage.GetText(expectedText);
            Assert.That(actualText, Is.EqualTo(expectedText));
        }

        [Test]
        public void SaveButtonIsUnavailableWhenFormIsPartiallyFilledWithDescription()
        {
            var expectedText = "Test Description";

            addMilestonePage.EnterDescriptionText(expectedText);
            addMilestonePage.SelectSaveButton();

            addMilestonePage.AssertOnPage();
            var actualText = addMilestonePage.GetText(expectedText);
            Assert.That(actualText, Is.EqualTo(expectedText));
        }

        [Test]
        public void SaveButtonCreatesMilestoneWhenFormIsFilled()
        {
            var expectedMilestone = new Milestone()
            {
                Label = "Test Milestone",
                Description = "Test Description"
            };

            addMilestonePage.EnterTitleText(expectedMilestone.Label);
            addMilestonePage.EnterDescriptionText(expectedMilestone.Description);
            addMilestonePage.SelectSaveButton();

            milestonesPage.AssertOnPage();
            var milestone = milestonesPage.GetMilestone(0);
            Assert.Multiple(() =>
            {
                Assert.That(milestone.Label, Is.EqualTo(expectedMilestone.Label));
                Assert.That(milestone.Description, Is.EqualTo(expectedMilestone.Description));
            });
        }
    }
}
