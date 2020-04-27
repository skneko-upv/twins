﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PauseView : AbsoluteLayout
    {

        public PauseView(bool isTimeHalted)
        {
            InitializeComponent();
            timeNotHaltedWarning.IsVisible = isTimeHalted;
        }

        public PauseView() : this(false) { }

        public void OnResume(object sender, EventArgs e) { window.IsVisible = false; }

        public void OnPause() { window.IsVisible = true; }

        public void OnAbandon(object sender, EventArgs e) {
        }

        public void OnOptions(object sender, EventArgs e)
        {
            CommingSoonView.ButtonNotImplemented();
        }
    }
}