using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using S28Maker.Services;
using Xamarin.Forms;

namespace S28Maker.Views
{
    public class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            Content = new ActivityIndicator
            { IsRunning = true, Scale = 4,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (S28Document.Current != null)
            {
                Shell.Current.GoToAsync("//" + nameof(MonthCarouselPage));
            }
            else 
                Shell.Current.GoToAsync("//" + nameof(OpenS28));
        }
    }
}