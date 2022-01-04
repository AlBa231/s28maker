using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using S28Maker.Services;
using S28Maker.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace S28Maker.ViewModels
{
    public class OpenS28ViewModel : BaseViewModel
    {
        public Command OpenFileCommand { get; }

        public OpenS28ViewModel()
        {
            OpenFileCommand = new Command(async () => await OpenS28());
        }

        async Task OpenS28()
        {
            var customFileType =
                new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { ".pdf" } },
                    { DevicePlatform.Android, new[] { "application/pdf" } }
                });
            var options = new PickOptions
            {
                PickerTitle = "Please select a S-28.pdf",
                FileTypes = customFileType,
            };

            await PickAndShow(options);
        }

        async Task<FileResult> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    S28Document.SavePdfLocally(await result.OpenReadAsync());
                    await Shell.Current.GoToAsync("//" + nameof(LoadingPage));
                }
        
                return result;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка", "Файл не распознан. Если вы уверены в нем - обратись к разработчику. \nError:" + ex.Message, "OK");
                // The user canceled or something went wrong
            }
    
            return null;
        }
    }
}