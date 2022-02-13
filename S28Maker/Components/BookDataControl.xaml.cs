using System;
using System.Globalization;
using System.Threading.Tasks;
using S28Maker.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace S28Maker.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDataControl : StackLayout
    {
        public Command CopyOldValue { get; private set; }

        public BookDataControl()
        {
            InitializeComponent();
            LastCountLabel.GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    Command = new Command(() =>
                    {
                        if (InStockCountStepper.Value <= 0 && int.TryParse(LastCountLabel.Text, out var value))
                            InStockCountStepper.Value = value;
                    })
                }
            );

            AddRemCountLabel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () => await ShowAddCounter())
            });
        }

        private async Task ShowAddCounter()
        {
            var countStr = await Shell.Current.DisplayPromptAsync("Получено публикаций",
                "Введите кол-во полученных публикаций",
                initialValue: ((IS28FieldRow) BindingContext).ReceivedValue, 
                keyboard: Keyboard.Numeric);
            if (int.TryParse(countStr, out var count))
            {
                ((IS28FieldRow) BindingContext).ReceivedValue = count.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void InStockCountStepper_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            InStockCountLabel.Text = ((int) InStockCountStepper.Value).ToString();
        }

        private void ButtonDecrease10CounterClick(object sender, EventArgs e)
        {
            InStockCountStepper.Value = Math.Max(0, InStockCountStepper.Value - 10);
        }
        private void ButtonIncrease10CounterClick(object sender, EventArgs e)
        {
            InStockCountStepper.Value += 10;
        }
        
    }
}