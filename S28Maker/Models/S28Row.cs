using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace S28Maker.Models
{
    public class S28Row : INotifyPropertyChanged
    {
        private readonly S28MonthColumn month;
        private readonly S28MonthColumn previousMonth;
        private string value;
        private string recieveValue;
        private string calcValue;

        public S28Row() { }

        public S28Row(S28Row item, S28MonthColumn month, S28MonthColumn previousMonth)
        {
            this.month = month ?? throw new ArgumentNullException(nameof(month));
            this.previousMonth = previousMonth ?? throw new ArgumentNullException(nameof(month));
            Id = item.Id;
            Text = item.Text;
            Description = item.Description;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public string Value
        {
            get => value ??= month.GetFormFieldValue(Id, S28ColumnType.Available);
            set
            {
                if (SetProperty(ref this.value, value))
                {
                    SetFormFieldValue(S28ColumnType.Available, value);
                }
            }
        }
        public string PreviousValue => previousMonth.GetFormFieldValue(Id, S28ColumnType.Available);

        public string ReceiveValue
        {
            get => recieveValue ??= month.GetFormFieldValue(Id, S28ColumnType.Recieved);
            set
            {
                if (SetProperty(ref recieveValue, value))
                {
                    SetFormFieldValue(S28ColumnType.Recieved, value);
                }
            }
        }

        private void SetFormFieldValue(S28ColumnType column, string value)
        {
            month.SetFormFieldValue(Id, column, value);
            UpdateCalculations();
        }

        private void UpdateCalculations()
        {
            int.TryParse(PreviousValue, out var prevValue);
            int.TryParse(ReceiveValue, out var receive);
            int.TryParse(Value, out var currentValue);

            CalcValue = (prevValue + receive - currentValue).ToString(CultureInfo.InvariantCulture);
        }

        public string CalcValue
        {
            get => calcValue ??= month.GetFormFieldValue(Id, S28ColumnType.Calculated);
            set
            {
                if (SetProperty(ref calcValue, value)) 
                    month.SetFormFieldValue(Id, S28ColumnType.Calculated, value);
            }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}