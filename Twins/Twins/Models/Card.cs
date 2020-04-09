using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Twins.Models
{
    public partial class Card
    {
        private ImageSource image;
        private bool flipped;

        public bool Flipped { get => flipped; set => flipped = value; }
        public ImageSource Image { get => image; set => image = value; }
    }
}
