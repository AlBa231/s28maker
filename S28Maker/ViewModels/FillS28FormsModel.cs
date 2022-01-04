using S28Maker.Models;
using S28Maker.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using S28Maker.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace S28Maker.ViewModels
{
    public class FillS28FormsModel : BaseViewModel
    {
        private readonly S28MonthItem _month;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command ShareCommand { get; }

        public event EventHandler ToolbarUpdateRequired;
        
        public FillS28FormsModel(S28MonthItem month)
        {
            _month = month;
            Title = _month.Name;
            Items = new ObservableCollection<Item>();
            IsBusy = true;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
            ShareCommand = new Command(async () =>
            {
                if (S28Document.Current == null) return;
                if (!File.Exists(S28Document.NewFileName)) return;
                OnToolbarUpdateRequired();
                S28Document.Current.Close();
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Отправить S-28",
                    File = new ShareFile(S28Document.NewFileName)
                });
            });
            IsBusy = false;
        }
        
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Items.Clear();
                if (S28Document.Current == null) return;
                
                foreach (var item in S28Document.Current.Items)
                {
                    Items.Add(new Item(item, _month));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.GoToAsync(nameof(OpenS28));
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
        
        protected virtual void OnToolbarUpdateRequired()
        {
            ToolbarUpdateRequired?.Invoke(this, EventArgs.Empty);
        }
    }
}