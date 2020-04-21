using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Twins.Models
{
    public partial class Card
    {
        public ImageSource Image { get; set; }
        public int Id { get; set; }
        public Card Pair { get; set; }
    }
}
