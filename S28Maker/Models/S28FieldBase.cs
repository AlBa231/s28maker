using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace S28Maker.Models
{
    public abstract class S28FieldBase : IS28FieldRow, INotifyPropertyChanged
    {
        private const string ZeroColumnValue = "0";
        public abstract string Value { get; set; }
        public abstract string ReceivedValue { get; set; }
        public abstract string CalculatedValue { get; }
        public abstract string PreviousValue { get; }

        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public void CopyFrom(IS28FieldRow itemFrom)
        {
            Value = GetFirstNotEmpty(Value, itemFrom.Value, ZeroColumnValue);
            ReceivedValue = GetFirstNotEmpty(ReceivedValue, itemFrom.ReceivedValue, ZeroColumnValue);
        }

        private string GetFirstNotEmpty(params string[] strs)
        {
            return strs.FirstOrDefault(IsNotEmpty);
        }

        private bool IsNotEmpty(string str)
        {
            return !string.IsNullOrEmpty(str);
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