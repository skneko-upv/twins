using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twins.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeckViewComponent : StackLayout
    {
        public Deck Deck { get; }

        public DeckViewComponent(Deck deck)
        {
            InitializeComponent();
            Deck = deck;
            BuildDeckView();
        }

        private void BuildDeckView()
        {
            Device.SetFlags(new string[] { "RadioButton_Experimental" });
            BackCardImage.Source = Deck.BackImage;
            FrontCardImage.Source = Deck.Cards[0].Image;
        }

        public bool IsChecked()
        {
            return checkedRadioButton.IsChecked;
        }

        public void MarkChecked()
        {
            checkedRadioButton.IsChecked = true;
        }
    }
}