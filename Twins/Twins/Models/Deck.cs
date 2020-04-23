using System.Collections.Generic;
using Xamarin.Forms;

namespace Twins.Models
{
    public partial class Deck
    {
        public ImageSource BackImage { get; }
        public List<Card> Cards { get; }
    }
}
