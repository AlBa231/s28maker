using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S28Maker.Models;
using S28Maker.Services;
using S28Maker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace S28Maker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthCarouselPage : CarouselPage
    {
        public string MonthName => ((S28MonthItem) SelectedItem)?.Name;

        public MonthCarouselPage()
        {
            InitializeComponent();
            BindingContext = new MonthCarouselViewModel();
            ItemsSource = S28MonthItem.All;
            SelectedItem = S28MonthItem.Current;
        }
        
        private void MonthCarouselPage_OnCurrentPageChanged(object sender, EventArgs e)
        {
            Title = MonthName;
        }
    }
}