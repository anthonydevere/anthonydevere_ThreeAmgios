﻿using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Threading.Tasks;
using ThreeAmgios.Services;
using ThreeAmgios.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThreeAmgios
{
    public partial class App : Application
    {
        private string ThreeAmigos_iOS = "f59a2031-9641-48c9-be98-c40db1cac3c7";
        private string ThreeAmigos_UWP = "15ef0072-2dd3-4910-9c17-773375e6f213";        
        private string ThreeAmigos_Android = "549c3db4-f00b-434b-8c66-24f7c340c920";

        private System.Timers.Timer _heartBeat;

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();

            _heartBeat = new System.Timers.Timer(30000);
            _heartBeat.Elapsed += _heartBeat_Elapsed;
            _heartBeat.Enabled = true;
           
        }

        private void _heartBeat_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Analytics.TrackEvent("_heartBeat_Elapsed");
        }

        //protected override void OnStart()
        //{
        //    AppCenter.LogLevel = LogLevel.Verbose;

        //    AppCenter.Start($"ios={ThreeAmigos_iOS};android={ThreeAmigos_Android};uwp={ThreeAmigos_UWP}", typeof(Analytics), typeof(Crashes));

        //    if (AppCenter.Configured)
        //    {
        //        Analytics.TrackEvent($"AppCenter Configured at {DateTime.Now.Ticks.ToString()}");
        //    }
        //    else
        //    {
        //        Analytics.TrackEvent($"AppCenter NOT Configured at {DateTime.Now.Ticks.ToString()}");
        //    }
        //}

        protected override async void OnStart()
        {
            AppCenter.LogLevel = LogLevel.Verbose;

            AppCenter.Start($"ios={ThreeAmigos_iOS};android={ThreeAmigos_Android};uwp={ThreeAmigos_UWP}", typeof(Analytics), typeof(Crashes));

            if (AppCenter.Configured)
            {
                Analytics.TrackEvent($"AppCenter Configured at {DateTime.Now.Ticks.ToString()}");
                _heartBeat.Start();
            }
            else
            {
                Analytics.TrackEvent($"AppCenter NOT Configured at {DateTime.Now.Ticks.ToString()}");
            }


            await GetSomethingAsync(); //Simulating await work
        }

        private async Task GetSomethingAsync()
        {
            await Task.Run(() => DoWork());
        }

        private void DoWork()
        {
            for (int i = 0; i <= 1000; i++)
            {
                var results = i * i * i;
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
