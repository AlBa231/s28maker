using System;
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