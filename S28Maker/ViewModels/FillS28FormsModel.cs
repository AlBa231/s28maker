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
        
        public FillS28FormsModel(S28MonthItem month)
        {
            _month = month;
            Title = _month.Name;
            Items = new ObservableCollection<Item>();
            IsBusy = true;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
            IsBusy = false;
        }
        
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                if (S28Document.Current == null) return;
                Items.Clear();
                foreach (var monthItem in _month.Items)
                {
                    Items.Add(monthItem);
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
    }
}