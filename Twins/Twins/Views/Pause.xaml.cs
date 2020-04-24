﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pause : ContentPage
    {
        public Pause(bool pausaTiempo)
        {
            InitializeComponent();
            notPause.IsVisible = pausaTiempo;
        }
        
    }
}