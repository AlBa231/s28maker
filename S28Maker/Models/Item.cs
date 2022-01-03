using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using iText.Forms.Fields;
using iText.Layout.Element;

namespace S28Maker.Models
{
    public class Item : INotifyPropertyChanged
    {
        private string _value;
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public PdfFormField[] RowValues { get; set; } = new PdfFormField[19];

        
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