using Twins.Persistence;
using Xamarin.Forms;

namespace Twins
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var _ = Database.Instance;
            MainPage = new NavigationPage(new OnAppearingAsync());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
