﻿using System;
using System.IO;
using Xamarin.UITest;
using Xamarin.UITest.Configuration;

namespace ProductivityAppTests.UiTests
{
    public class AppManager
    {
        static IApp app;
        static Platform? platform;

        public static IApp App
        {
            get
            {
                if (app == null)
                    throw new NullReferenceException("'AppManager.App' not set. Call 'AppManager.StartApp()' before trying to access it.");
                return app;
            }
        }

        public static Platform Platform
        {
            get
            {
                if (platform == null)
                    throw new NullReferenceException("'AppManager.Platform' not set.");
                return platform.Value;
            }

            set
            {
                platform = value;
            }
        }

        public static void StartApp(AppDataMode? dataMode = default)
        {
            var mode = dataMode ?? AppDataMode.Clear;

            if (platform == Platform.Android)
            {
                app = ConfigureApp.Android
                    .InstalledApp("com.companyname.productivityapp")
                    .EnableLocalScreenshots()
                    .StartApp(mode);
            }
            else app = ConfigureApp.iOS.StartApp();
        }
    }
}
