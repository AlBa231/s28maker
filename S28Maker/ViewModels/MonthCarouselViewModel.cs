using System.IO;
using S28Maker.Services;
using S28Maker.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace S28Maker.ViewModels
{
    public class MonthCarouselViewModel : BaseViewModel
    {
        public Command ShareCommand { get; } 
        public Command CloseCommand { get; }

        public MonthCarouselViewModel()
        {
            ShareCommand = new Command(async () =>
            {
                if (S28Document.Current == null) return;
                S28Document.Current.Close();
                if (!File.Exists(S28Document.FileName)) return;
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Отправить S-28",
                    File = new ShareFile(S28Document.FileName)
                });
            });

            CloseCommand = new Command(async () =>
            {
                if (S28Document.Current == null) return;
                S28Document.Current.Close();
                await Shell.Current.GoToAsync("//" + nameof(OpenS28));
            });
        }
    }
}
