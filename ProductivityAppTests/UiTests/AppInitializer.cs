using System;
using System.IO;
using Xamarin.UITest;

namespace ProductivityAppTests.UiTests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android
                    .InstalledApp("com.companyname.productivityapp")
                    .EnableLocalScreenshots()
                    .StartApp();
            }
            return ConfigureApp.iOS.StartApp();
        }
    }
}
