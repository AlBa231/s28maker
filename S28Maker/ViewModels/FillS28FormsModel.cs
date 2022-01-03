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
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command ShareCommand { get; }
        public Command PrevMonthCommand { get; }
        public Command NextMonthCommand { get; }
        public Command<Item> ItemTapped { get; }

        public event EventHandler ToolbarUpdateRequired;
        
        public FillS28FormsModel()
        {
            Title = "";
            Items = new ObservableCollection<Item>();
            IsBusy = true;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);
            
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

            PrevMonthCommand = new Command(() => ChangeMonth(-1));
            NextMonthCommand = new Command(() => ChangeMonth(1));
            IsBusy = false;
        }

        private void ChangeMonth(int increment)
        {
            if (S28Document.Current == null) return;

            var month = S28Document.Current.Month + increment;
            if (month < 0) month = 11;
            if (month > 11) month = 0;
            S28Document.Current.Month = month;
            OnToolbarUpdateRequired();
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
                    Items.Add(item);
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
            SelectedItem = null;

        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        
        //private async void OnAddItem(object obj)
        //{
        //    await Shell.Current.GoToAsync(nameof(NewItemPage));
        //}

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        protected virtual void OnToolbarUpdateRequired()
        {
            ToolbarUpdateRequired?.Invoke(this, EventArgs.Empty);
        }
    }
}