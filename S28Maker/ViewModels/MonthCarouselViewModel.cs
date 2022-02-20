using System.IO;
using S28Maker.Models;
using S28Maker.Services;
using S28Maker.Views;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace S28Maker.ViewModels
{
    public class MonthCarouselViewModel : BaseViewModel
    {
        public Command ShareCommand { get; } 
        public Command CloseCommand { get; }
        public Command CopyPrevMonthCommand { get; }
        public Command SaveCommand { get; } 

        public IS28MonthColumn SelectedItem { get; set; } = S28DocumentBase.Current.CurrentMonth;

        public MonthCarouselViewModel()
        {
            ShareCommand = new Command(async () =>
            {
                if (S28DocumentBase.Current == null) return;
                S28DocumentBase.Current.Close();
                if (!File.Exists(S28PdfDocument.FileName)) return;
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Отправить S-28",
                    File = new ShareFile(S28PdfDocument.FileName)
                });
            });

            SaveCommand = new Command(async () =>
            {
                if (S28DocumentBase.Current == null) return;
                IsBusy = true;
                S28DocumentBase.Current.Close();
                if (S28DocumentBase.Current == null)
                {
                    await Shell.Current.GoToAsync("//" + nameof(LoadingPage));
                }

                IsBusy = false;

                await Shell.Current.DisplayToastAsync("Сохранено");
            });

            CloseCommand = new Command(async () =>
            {
                if (S28DocumentBase.Current == null) return;
                S28DocumentBase.Current.Close();
                await Shell.Current.GoToAsync("//" + nameof(OpenS28));
            });

            CopyPrevMonthCommand = new Command(async () =>
            {
                //S28Document.Current.GetMonth()
                //if (!await Shell.Current.DisplayAlert("Автозаполнение", 
                //    SelectedItem.PreviousMonth == null ? "Заполнить на основании начальной колонки В наличии?" : "Переписать все цифры с месяца " + SelectedItem.PreviousMonth.Name,
                //    "Скопировать", "Нет, не надо")) return;

                //SelectedItem.CopyFromPreviousMonth();
                await Shell.Current.DisplayToastAsync("Скопировано");
            });
        }
    }
}
