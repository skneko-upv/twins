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
    public partial class EditDeckView : ContentPage
    {
        public EditDeckView()
        {
            InitializeComponent();
        }

        
        private void OnSave(object sender, EventArgs e)
        {

        }
        private void OnCancel(object sender, EventArgs e)
        {

        }
    }
}