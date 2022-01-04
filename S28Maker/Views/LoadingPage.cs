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
            Content = new StackLayout
            {
                Children = {
                    new ActivityIndicator() { IsRunning = true,
                        VerticalOptions = LayoutOptions.CenterAndExpand }
                }
            };
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