using ProductivityApp.Models;
using ProductivityApp.Services;
using System;
using System.Collections.Generic;
using Autofac;
using Xamarin.Forms;
using ProductivityApp.ViewModels;

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

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MilestoneDataStore>().As<IDataStore<Milestone>>();

            container = builder.Build();
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
