using Xamarin.Forms;

namespace SnapNPack
{
    public partial class App : Application
    {
        internal static readonly int MISCELLANEOUS_ITEM_BOX = -1;

        //Just the apps entry point
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
