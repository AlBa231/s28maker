using S28Maker.Models;
using S28Maker.ViewModels;
using Xamarin.Forms;

namespace S28Maker.Views
{
    public partial class ItemsPage : ContentPage
    {
        FillS28FormsModel _viewModel;

        public IS28MonthColumn Month { get; set; }

        public ItemsPage()
        {
            InitializeComponent();
        }
        

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Month == null)
            {
                Month = (IS28MonthColumn) BindingContext;
                BindingContext = _viewModel = new FillS28FormsModel(Month);
            }
            _viewModel.OnAppearing();
        }
    }
}