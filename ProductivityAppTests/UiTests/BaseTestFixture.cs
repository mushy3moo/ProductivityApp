using System;
using NUnit.Framework;
using ProductivityAppTests.UiTests.Pages;
using Xamarin.UITest;

namespace ProductivityAppTests.UiTests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public abstract class BaseTestFixture
    {
        protected IApp App => AppManager.App;
        protected bool OnAndroid => AppManager.Platform == Platform.Android;
        protected bool OniOS => AppManager.Platform == Platform.iOS;

        protected readonly MilestonesPageHelper milestonesPage;
        protected readonly AddMilestonePageHelper addMilestonePage;
        protected readonly EditMilestonePageHelper editMilestonePage;

        protected BaseTestFixture(Platform platform)
        {
            AppManager.Platform = platform;
            milestonesPage = new MilestonesPageHelper();
            addMilestonePage = new AddMilestonePageHelper();
            editMilestonePage = new EditMilestonePageHelper();
        }
    }
}
