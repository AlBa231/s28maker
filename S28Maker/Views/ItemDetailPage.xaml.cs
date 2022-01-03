using S28Maker.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace S28Maker.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}