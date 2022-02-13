using System.Collections.ObjectModel;

namespace S28Maker.Models
{
    public interface IS28MonthColumn
    {
        string Name { get; }

        void CopyFromPreviousMonth(IS28MonthColumn month);

        ObservableCollection<IS28FieldRow> MonthRows {get; }
    }
}