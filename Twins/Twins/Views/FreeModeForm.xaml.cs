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
    public partial class FreeModeForm : ContentPage
    {
        public FreeModeForm()
        {
            InitializeComponent();
        }
        void OnBackMainMenu(object sender, EventArgs e)
        {
            ///resume
            ///Go Back to Main Menu
        }

        void OnStartGame(object sender, EventArgs e)
        {
            ///resume
            ///Start the game with de parameter of the form
        }

    }
}