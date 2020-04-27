﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotImplementedMessage : AbsoluteLayout
    {
        public NotImplementedMessage()
        {
            InitializeComponent();
        }

        public void OnAcceptButton(object sender, EventArgs e)
        {
            CommingSoonView.IsVisible = false;
        }

        public void ButtonNotImplemented()
        {
            CommingSoonView.IsVisible = true;
        }
    }
}