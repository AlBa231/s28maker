using S28Maker.Services;
using Xamarin.Forms;

namespace S28Maker
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            S28PdfDocument.Current?.Close();
        }

        protected override void OnResume()
        {
        }
    }
}
