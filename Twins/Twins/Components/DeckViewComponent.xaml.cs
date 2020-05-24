using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeckViewComponent : StackLayout
    {
        public DeckViewComponent()
        {
            InitializeComponent();
            //BuildDeckView();
        }

        private void BuildDeckView()
        {
            //BackCardImage.Source = "Assets/Decks/Deck1/backimage.png";
            //FrontCardImage.Source = "Assets/Decks/Deck1/card1.png";
        }
    }
}