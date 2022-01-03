using S28Maker.Models;
using S28Maker.ViewModels;
using S28Maker.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S28Maker.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace S28Maker.Views
{
    public partial class ItemsPage : ContentPage
    {
        FillS28FormsModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new FillS28FormsModel();
            _viewModel.ToolbarUpdateRequired += _viewModel_OnToolbarUpdateRequired;
        }

        private void _viewModel_OnToolbarUpdateRequired(object sender, EventArgs e)
        {
            btnNextMonth.IsEnabled = S28Document.Current != null && S28Document.Current.Month < 11;
            btnPrevMonth.IsEnabled = S28Document.Current != null && S28Document.Current.Month > 0;
            btnShare.IsEnabled = S28Document.Current != null;
            Title = MonthRenderer.GetMonthName(S28Document.Current?.Month ?? 0);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}