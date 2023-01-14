using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace ProductivityAppTests.UiTests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public abstract class BaseTestFixture
    {
        protected IApp app => AppManager.App;
        protected bool OnAndroid => AppManager.Platform == Platform.Android;
        protected bool OniOS => AppManager.Platform == Platform.iOS;

        protected BaseTestFixture(Platform platform)
        {
            AppManager.Platform = platform;
        }

        [SetUp]
        public virtual void StartApp()
        {
            AppManager.StartApp();
        }
    }

}
