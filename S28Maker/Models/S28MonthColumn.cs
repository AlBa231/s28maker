using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S28Maker.Models
{
    public class S28MonthColumn : IS28MonthColumn
    {
        private readonly LazyList<IS28FieldRow> lazyMonthRows;

        public string Name { get; }
        public ObservableCollection<IS28FieldRow> MonthRows => lazyMonthRows.Items;

        internal S28MonthColumn(string name, IEnumerable<IS28FieldRow> monthRows)
        {
            Name = name;
            lazyMonthRows = new LazyList<IS28FieldRow>(monthRows);
        }

        public void CopyFromPreviousMonth(IS28MonthColumn month)
        {
            for (int i = 0; i < MonthRows.Count; i++)
            {
                IS28FieldRow itemTo = MonthRows[i];
                IS28FieldRow itemFrom = month.MonthRows[i];
                itemTo.CopyFrom(itemFrom);
            }
        }
    }
}
