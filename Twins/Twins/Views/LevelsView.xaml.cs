using Twins.Persistence;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LevelsView : ContentPage
    {
        public LevelsView()
        {
            InitializeComponent();
            var database = Database.Instance;
        }
    }
}