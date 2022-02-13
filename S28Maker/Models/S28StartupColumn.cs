using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace S28Maker.Models
{
    internal class S28StartupColumn : IS28MonthColumn
    {
        internal S28StartupColumn()
        {
        }

        public int Index => 0;
        public string Name => "";

        public ObservableCollection<IS28FieldRow> MonthRows => new ObservableCollection<IS28FieldRow>();

        public void CopyFromPreviousMonth(IS28MonthColumn month)
        {
            
        }
    }
}
