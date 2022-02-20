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
            
            if (TryLoadLoadPdfDocument())
                NavigateToMonthCarousel();
            else
                NavigateToOpenS28View();
        }

        private static bool TryLoadLoadPdfDocument()
        {
            var localPdfDocument = S28PdfDocument.TryLoadLocalPdfDocument();
            if (localPdfDocument != null)
            {
                S28Document.Current = localPdfDocument;
            }
            return localPdfDocument != null;
        }

        private static void NavigateToMonthCarousel()
        {
            Shell.Current.GoToAsync("//" + nameof(MonthCarouselPage)).ConfigureAwait(false);
        }

        private static void NavigateToOpenS28View()
        {
            Shell.Current.GoToAsync("//" + nameof(OpenS28)).ConfigureAwait(false);
        }
    }
}