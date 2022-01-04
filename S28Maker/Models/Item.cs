using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace S28Maker.Models
{
    public class Item : INotifyPropertyChanged
    {
        private readonly S28MonthItem _month;
        private string _value;

        public Item() { }

        public Item(Item item, S28MonthItem month)
        {
            _month = month ?? throw new ArgumentNullException(nameof(month));
            Id = item.Id;
            Text = item.Text;
            Description = item.Description;
            _value = _month.GetFormFieldValue(item.Id);
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public string Value
        {
            get => _value;
            set
            {
                if (value == _value) return;
                _value = value;
                _month.SetFormFieldValue(Id, value);
                OnPropertyChanged(nameof(Value));
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