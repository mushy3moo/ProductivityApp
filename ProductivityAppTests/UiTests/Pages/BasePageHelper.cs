using System;
using NUnit.Framework;
using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace ProductivityAppTests.UiTests.Pages
{
    public abstract class BasePageHelper
    {
        protected IApp app => AppManager.App;
        protected bool OnAndroid => AppManager.Platform == Platform.Android;
        protected bool OniOS => AppManager.Platform == Platform.iOS;

        protected abstract PlatformQuery Trait { get; }

        public void AssertOnPage(TimeSpan? timeout = default)
        {
            var page = this.GetType().Name.Replace("Helper", "");
            var message = $"Unable to verify on page: {page}";

            if (timeout == null)
                Assert.IsNotEmpty(app.Query(Trait.Current), message);
            else
                Assert.DoesNotThrow(() => app.WaitForElement(Trait.Current, timeout: timeout), message);
        }

        public void WaitForPageToLeave(TimeSpan? timeout = default)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(10);
            var page = this.GetType().Name.Replace("Helper", "");
            var message = $"Unable to verify *not* on page: {page}";

            Assert.DoesNotThrow(() => app.WaitForNoElement(Trait.Current, timeout: timeout), message);
        }

        public void NavigateMenu(string element, TimeSpan? timeout = default)
        {
            app.Tap(c => c.Class("AppCompatImageButton"));

            SelectElement(element, timeout);
        }

        protected void SelectElement(Query element, TimeSpan? timeout = default)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(10);
            var page = this.GetType().Name.Replace("Helper", "");
            var message = $"Timeout Value {timeout.Value} on page: {page}";

            app.WaitForElement(element, message, timeout);
            app.Tap(element);
        }

        protected void SelectElement(string element, TimeSpan? timeout = default)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(10);
            var page = this.GetType().Name.Replace("Helper", "");
            var message = $"Timeout Value {timeout.Value} on page: {page}";

            app.WaitForElement(c => c.Marked(element), message, timeout);
            app.Tap(c => c.Marked(element));
        }
    }
}
