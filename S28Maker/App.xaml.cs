using S28Maker.Error;
using S28Maker.Services;
using Xamarin.Forms;

namespace S28Maker
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            InitErrorHandler();

            MainPage = new AppShell();
        }

        private void InitErrorHandler()
        {
            S28ErrorHandler.Current = new S28ToastErrorHandler();
        }

        protected override void OnSleep()
        {
            S28Document.Current?.Close();
        }
    }
}
