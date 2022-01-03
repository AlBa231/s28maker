using S28Maker.Views;
using Xamarin.Forms;

namespace S28Maker
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //
            // if (S28Document.Current != null)
            // {
            //     GoToAsync("//ItemsPage");
            // }
        }
    }
}
