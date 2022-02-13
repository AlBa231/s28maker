using System;
using S28Maker.Models;
using S28Maker.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace S28Maker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthCarouselPage : CarouselPage
    {
        public string MonthName => ((S28MonthColumn) SelectedItem)?.Name;

        public MonthCarouselPage()
        {
            InitializeComponent();
            ItemsSource = S28Document.Current.Monthes;
        }
        
        private void MonthCarouselPage_OnCurrentPageChanged(object sender, EventArgs e)
        {
            Title = MonthName;
        }
    }
}