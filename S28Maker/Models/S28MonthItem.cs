using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using iText.Forms.Fields;
using S28Maker.Services;
using S28Maker.ViewModels;

namespace S28Maker.Models
{
    public class S28MonthItem
    {
        public string Name { get; set; }
        public int Index { get; set; }

        private S28MonthItem() { }

        public static readonly List<S28MonthItem> All = MonthRenderer.MonthNames.Select((name, idx) => new S28MonthItem { Index = idx, Name = name }).ToList();

        public static S28MonthItem Current => All[MonthRenderer.GetMonthPos(DateTime.Today.AddMonths(-1).Month)];

        private PdfFormField[] CurrentValues { get; set; }

        //private FillS28FormsModel _fillModel;
        //public FillS28FormsModel FillModel => _fillModel ??= new FillS28FormsModel(this);

        private PdfFormField GetFormField(int itemId)
        {
            
            if (itemId >= S28Document.Current.Items.Count) throw new ArgumentOutOfRangeException(nameof(itemId));

            CurrentValues ??= new PdfFormField[S28Document.Current.Items.Count];

            var field = CurrentValues[itemId] ??= S28Document.Current.GetFormField(itemId, 2 + Index * 3);
            return field;
        }

        public string GetFormFieldValue(int itemId)
        {
            if (itemId >= S28Document.Current.Items.Count) throw new ArgumentOutOfRangeException(nameof(itemId));

            CurrentValues ??= new PdfFormField[S28Document.Current.Items.Count];

            var field = CurrentValues[itemId] ??= S28Document.Current.GetFormField(itemId, 2 + Index * 3);
            return field?.GetValueAsString();
        }

        public void SetFormFieldValue(int itemId, string value)
        {
            GetFormField(itemId)?.SetValue(value);
        }
    }
}
