using ProductivityApp.Models;
using ProductivityApp.Services;
using System;
using Autofac;
using Xamarin.Forms;
using System.IO;

namespace ProductivityApp
{
    public partial class App : Application
    {
        public static IContainer container;
        private string LocalDataPath;

        public App()
        {
            InitializeComponent();
            RegisterDependencies();
            SetLocalAppDataPath();

            MainPage = new AppShell();
        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MilestoneDataStore>().As<IDataStore<Milestone>>().SingleInstance();
            builder.RegisterInstance(LocalDataPath).As<string>();

            container = builder.Build();
        }

        private void SetLocalAppDataPath()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                LocalDataPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                LocalDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library");
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                LocalDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProductivityApp");
            }
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
    }
}
