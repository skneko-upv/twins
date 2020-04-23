using System;
using Xamarin.Forms;

namespace Twins.Models
{
    public partial class Card : IEquatable<Card>
    {
        public int Id { get; }

        public Deck Deck { get; }

        public ImageSource Image { get; }
    }
}
