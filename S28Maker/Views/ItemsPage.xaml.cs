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

        public S28MonthItem Month { get; set; }

        public ItemsPage()
        {
            InitializeComponent();


        }

        private void _viewModel_OnToolbarUpdateRequired(object sender, EventArgs e)
        {
            //btnNextMonth.IsEnabled = S28Document.Current != null && S28Document.Current.Month < 11;
            //btnPrevMonth.IsEnabled = S28Document.Current != null && S28Document.Current.Month > 0;
            //btnShare.IsEnabled = S28Document.Current != null;
            //Title = MonthRenderer.GetMonthName(S28Document.Current?.Month ?? 0);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Month == null)
            {
                Month = (S28MonthItem) BindingContext;
                Title = Month.Name;

                BindingContext = _viewModel = new FillS28FormsModel(Month);
                _viewModel.ToolbarUpdateRequired += _viewModel_OnToolbarUpdateRequired;
            }
            _viewModel.OnAppearing();
        }
    }
}