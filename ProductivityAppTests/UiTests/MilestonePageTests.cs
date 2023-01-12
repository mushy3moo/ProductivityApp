using System;
using NUnit.Framework;
using ProductivityApp;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using ProductivityApp.Views;
using System.Linq;
using System.Reflection.Emit;
using System.IO;

namespace ProductivityAppTests.UiTests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class MilestonePageTests
    {
        IApp app;
        readonly Platform platform;
        public MilestonePageTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void Setup()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void TestMilestonesFlyoutItemLoadsMilestonePage()
        {
            //var screenshotName = $"{TestContext.CurrentContext.Test.Name}";
            app.Tap(c => c.Class("AppCompatImageButton"));
            app.WaitForElement(c => c.Marked("Milestones"), "Timed out waiting for milestones page", TimeSpan.FromSeconds(30));
            app.Tap(c => c.Marked("Milestones"));

            var result = app.Query(c => c.Marked("Milestones")).FirstOrDefault().Text;
            //app.Screenshot(screenshotName);

            Assert.That(result, Is.EqualTo("Milestones"));
        }

        [Test]
        public void TestMilestoneAddButtonLoadsNewMilestonePage()
        {
            //var screenshotName = $"{TestContext.CurrentContext.Test.Name}";

            app.Tap(c => c.Class("AppCompatImageButton"));
            app.WaitForElement(c => c.Marked("Milestones"), "Timed out waiting for milestones page", TimeSpan.FromSeconds(30));
            app.Tap(c => c.Marked("Milestones"));

            app.WaitForElement(c => c.Marked("MilestonesPage_AddButton"), "Timed out waiting for add button on milestones page", TimeSpan.FromSeconds(30));
            app.Tap(c => c.Marked("MilestonesPage_AddButton"));

            app.WaitForElement(c => c.Marked("Title"), "Timed out waiting for New Milestone Page", TimeSpan.FromSeconds(30));
        }

        
        [Test]
        public void TestMilestonesButtonLoadsMilestonePage()
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
