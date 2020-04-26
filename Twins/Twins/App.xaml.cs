﻿using System;
using Twins.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new FreeModeForm();
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
