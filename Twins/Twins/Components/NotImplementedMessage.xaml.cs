using System;

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
            MainPage.EffectsPlayer.Play();
        }

        public void ButtonNotImplemented()
        {
            CommingSoonView.IsVisible = true;
        }

        public void ChangeMessage(string message)
        {
            Message.Text = message;
        }
    }
}