using Twins.Persistence;
using Xamarin.Forms;

namespace Twins
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Database _ = Database.Instance;
            MainPage = new NavigationPage(new MainPage());
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
