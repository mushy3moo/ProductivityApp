using System;
using System.IO;
using NUnit.Framework;
using ProductivityApp;
using ProductivityApp.Models;
using ProductivityApp.Services;
using Xamarin.UITest;
using Xamarin.UITest.Configuration;

namespace ProductivityAppTests.UiTests
{
    public class EditMIlestonePageTests : BaseTestFixture
    {
        MilestoneModel expectedMilestone;
        public EditMIlestonePageTests(Platform platform) : base(platform) 
        {
            expectedMilestone = new MilestoneModel()
            {
                Label = "Test Milestone",
                Description = "Description of Milestone"
            };
        }

        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            AppManager.StartApp(AppDataMode.DoNotClear);
            
            milestonesPage.NavigateMenu("Milestones");

            milestonesPage.SelectAddButton();

            addMilestonePage.AssertOnPage();
            addMilestonePage.EnterTitleText(expectedMilestone.Label);
            addMilestonePage.EnterDescriptionText(expectedMilestone.Description);
            addMilestonePage.SelectSaveButton();
        }


        [OneTimeTearDown]
        public virtual void AfterAllTests()
        {
            AppManager.StartApp(AppDataMode.Clear);
        }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            AppManager.StartApp(AppDataMode.DoNotClear);

            milestonesPage.NavigateMenu("Milestones");
            milestonesPage.SelectMilestone(0);

            editMilestonePage.AssertOnPage();
        }

        [Test]
        public void PageLoadsValidMilestone()
        {
            var resultLabel = editMilestonePage.GetTitleText();
            var resultDescription = editMilestonePage.GetDescriptionText();

            Assert.Multiple(() =>
            {
                Assert.That(resultLabel, Is.EqualTo(expectedMilestone.Label));
                Assert.That(resultDescription, Is.EqualTo(expectedMilestone.Description));
            });
        }

        [Test]
        public void BackButtonNavigatesToMilestonesPage()
        {
            editMilestonePage.SelectBackButton();

            milestonesPage.AssertOnPage();
        }

        [Test]
        public void SaveButtonIsUnavailableIfMilestoneTitleIsEmpty()
        {
            var alertTitle = "Confirm";

            editMilestonePage.ClearTitleText();
            editMilestonePage.SelectSaveButton();

            editMilestonePage.AssertOnPage();
            var resultExecption = Assert.Throws<Exception>(() => editMilestonePage.GetAlertTitle());
            var result = resultExecption.Message.Contains(alertTitle);
            Assert.That(result, Is.True);
        }

        [Test]
        public void SaveButtonIsUnavailableIfMilestoneDescriptionIsEmpty()
        {
            var alertTitle = "Confirm";

            editMilestonePage.ClearDescriptionText();
            editMilestonePage.SelectSaveButton();

            editMilestonePage.AssertOnPage();
            var resultExecption = Assert.Throws<Exception>(() => editMilestonePage.GetAlertTitle());
            var result = resultExecption.Message.Contains(alertTitle);
            Assert.That(result, Is.True);
        }

        [Test]
        public void SaveButtonDoesNothingWhenNoIsSelectedOnAlert()
        {
            var expectedTitle = editMilestonePage.GetTitleText();
            var expectedDescription = editMilestonePage.GetDescriptionText();

            editMilestonePage.SelectSaveButton();
            editMilestonePage.SelectNoButton();

            editMilestonePage.AssertOnPage();
            var actualTitle = editMilestonePage.GetTitleText();
            var actualDescription = editMilestonePage.GetDescriptionText();
            Assert.Multiple(() =>
            {
                Assert.That(actualTitle, Is.EqualTo(expectedTitle));
                Assert.That(actualDescription, Is.EqualTo(expectedDescription));
            });
        }

        [Test]
        public void SaveButtonUpdatesMilestoneWhenYesIsSelectedOnAlert()
        {
            var expectedTitle = "New Title";
            var expectedDescription = "New Title";
            var milestoneIndex = 0;

            editMilestonePage.ClearTitleText();
            editMilestonePage.ClearDescriptionText();
            editMilestonePage.EnterTitleText(expectedTitle);
            editMilestonePage.EnterDescriptionText(expectedDescription);
            App.Back();

            editMilestonePage.SelectSaveButton();
            editMilestonePage.SelectYesButton();

            milestonesPage.AssertOnPage();
            var milestone = milestonesPage.GetMilestone(milestoneIndex);
            var resultTitle = milestone.Label;
            var resultDescription = milestone.Description;

            Assert.Multiple(() =>
            {
                Assert.That(resultTitle, Is.EqualTo(expectedTitle));
                Assert.That(resultDescription, Is.EqualTo(expectedDescription));
            });
        }

        [Test]
        public void DeleteButtonDoesNothingWhenNoIsSelectedOnAlert()
        {
            var expectedTitle = editMilestonePage.GetTitleText();
            var expectedDescription = editMilestonePage.GetDescriptionText();

            editMilestonePage.SelectDeleteButton();
            editMilestonePage.SelectNoButton();

            editMilestonePage.AssertOnPage();
            var actualTitle = editMilestonePage.GetTitleText();
            var actualDescription = editMilestonePage.GetDescriptionText();
            Assert.Multiple(() =>
            {
                Assert.That(actualTitle, Is.EqualTo(expectedTitle));
                Assert.That(actualDescription, Is.EqualTo(expectedDescription));
            });
        }

        [Test]
        public void DeleteButtonDeletesMilestoneWhenYesIsSelectedOnAlert()
        {
            editMilestonePage.SelectDeleteButton();
            editMilestonePage.SelectYesButton();

            milestonesPage.AssertOnPage();
            Assert.Throws<Exception>(() => milestonesPage.GetMilestone(0));
        }
    }
}