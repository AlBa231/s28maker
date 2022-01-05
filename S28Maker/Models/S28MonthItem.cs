using System;
using System.Collections.Generic;
using System.Linq;
using iText.Forms.Fields;
using S28Maker.Services;

namespace S28Maker.Models
{
    public class S28MonthItem
    {
        public string Name { get; set; }
        public int Index { get; set; }

        private S28MonthItem() { }

        public static readonly List<S28MonthItem> All = MonthRenderer.MonthNames.Select((name, idx) => new S28MonthItem { Index = idx, Name = name }).ToList();

        public static S28MonthItem Current => All[MonthRenderer.GetMonthPos(DateTime.Today.AddMonths(-1).Month)];

        /// <summary>
        /// The first column (в наличии) of whole table.
        /// </summary>
        private PdfFormField[] StartFieldsValues { get; set; }
        private PdfFormField[] CurrentValues { get; set; }
        private PdfFormField[] ReceiveValues { get; set; }
        private PdfFormField[] CalcValues { get; set; }

        private S28MonthItem _prevMonth;

        public S28MonthItem PreviousMonth => _prevMonth ??= Index > 0 ? All[Index - 1] : null;
        
        private PdfFormField GetFormField(int itemId)
        {
            if (itemId >= S28Document.Current.Items.Count) throw new ArgumentOutOfRangeException(nameof(itemId));

            CurrentValues ??= new PdfFormField[S28Document.Current.Items.Count];

            return CurrentValues[itemId] ??= S28Document.Current.GetFormField(itemId, (int)S28Column.CurrentItems + Index * 3);
        }

        public PdfFormField GetReceiveFormField(int itemId)
        {
            if (itemId >= S28Document.Current.Items.Count) throw new ArgumentOutOfRangeException(nameof(itemId));

            ReceiveValues ??= new PdfFormField[S28Document.Current.Items.Count];

            return ReceiveValues[itemId] ??= S28Document.Current.GetFormField(itemId, (int)S28Column.NewItems + Index * 3);
        }
        public PdfFormField GetCalcFormField(int itemId)
        {
            if (itemId >= S28Document.Current.Items.Count) throw new ArgumentOutOfRangeException(nameof(itemId));

            CalcValues ??= new PdfFormField[S28Document.Current.Items.Count];

            return CalcValues[itemId] ??= S28Document.Current.GetCalcFormField(itemId, (int)S28Column.CalcItems + Index * 3);
        }
        public string GetPreviousMonthValue(int itemId)
        {
            if (itemId >= S28Document.Current.Items.Count) throw new ArgumentOutOfRangeException(nameof(itemId));

            if (PreviousMonth == null)
            {
                StartFieldsValues ??= new PdfFormField[S28Document.Current.Items.Count];
                return (StartFieldsValues[itemId] ??= S28Document.Current.GetFormField(itemId, 0))?.GetValueAsString();
            }

            return PreviousMonth.GetFormFieldValue(itemId);
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

        /// <summary>
        /// Clear all loaded PdfFormFields.
        /// </summary>
        public static void ResetCache()
        {
            foreach (var month in All)
            {
                month.StartFieldsValues = null;
                month.CurrentValues = null;
                month.ReceiveValues = null;
                month.CalcValues = null;
            }
        }
    }
}
