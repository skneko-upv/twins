using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BubblePoint : Grid
    {
        private double posX;
        private double posY;

        public BubblePoint()
        {
            InitializeComponent();
        }

        public BubblePoint SetRedBubble()
        {
            ImageBubble.Source = "Assets/Backgrounds/bubbleRBackground.png";
            return this;
        }
        public BubblePoint SetGreenBubble()
        {
            ImageBubble.Source = "Assets/Backgrounds/bubbleGBackground.png";
            return this;
        }

        public BubblePoint SetPoints(int points)
        {
            if (points <= 0)
            {
                LabelText.Text = "" + points;
            }
            else
            {
                LabelText.Text = "+" + points;
            }

            return this;
        }

        public async void SetPosition(double x, double y)
        {
            posX = x;
            posY = y;
            await this.TranslateTo(x, y, 0);
        }

        public async Task GoUp()
        {
            Opacity = 1;
            IsVisible = true;
            await this.TranslateTo(posX, posY - 15, 700);
            await this.FadeTo(0);
            IsVisible = false;
        }
    }
}