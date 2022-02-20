using S28Maker.Models;
using S28Maker.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using S28Maker.Services;
using Xamarin.Forms;

namespace S28Maker.ViewModels
{
    public class FillS28FormsModel : BaseViewModel
    {
        private readonly IS28MonthColumn month;

        public ObservableCollection<IS28FieldRow> Items { get; }
        public Command LoadItemsCommand { get; }
        
        public FillS28FormsModel(IS28MonthColumn month)
        {
            this.month = month ?? throw new ArgumentNullException(nameof(month));
            Title = month.Name;
            Items = new ObservableCollection<IS28FieldRow>();
            IsBusy = true;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
            IsBusy = false;
            
        }
        
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                if (S28PdfDocument.Current == null) return;
                Items.Clear();
                foreach (var monthItem in month.MonthRows)
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