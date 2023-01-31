using ProductivityApp.Models;
using ProductivityApp.Services;
using System;
using Autofac;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.PlatformConfiguration;

namespace ProductivityApp
{
    public partial class App : Application
    {
        public static IContainer container;
        private string localDirectoryPath;
        private string localDataPath;

        public App()
        {
            InitializeComponent();
            SetLocalFilePaths();
            CreateLocalAppFolders();
            RegisterDependencies();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MilestoneDataStore>().As<IDataStore<Milestone>>().SingleInstance();
            builder.RegisterInstance(localDataPath).As<string>();

            container = builder.Build();
        }

        private void SetLocalFilePaths()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                localDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                localDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "ApplicationSupport", "ProductivityApp");
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                localDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProductivityApp");
            }
            localDataPath = Path.Combine(localDirectoryPath, "data");
        }

        private void CreateLocalAppFolders()
        {
            if (!Directory.Exists(localDirectoryPath))
            {
                Directory.CreateDirectory(localDirectoryPath);
            }

            if (!Directory.Exists(localDataPath))
            {
                Directory.CreateDirectory(localDataPath);
            }
            
        }
    }
}
