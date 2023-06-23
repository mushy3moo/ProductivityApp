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


        public App()
        {
            InitializeComponent();
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

            builder.RegisterType<MilestoneService>().As<IDataService<MilestoneModel>>().SingleInstance();
            //builder.RegisterInstance(localDataPath).As<string>();

            container = builder.Build();
        }
    }
}
