using iText.Forms;
using iText.Forms.Fields;
using S28Maker.Services;

namespace S28Maker.Models
{
    public class S28AcroFormField : S28FieldBase
    {
        private const int ColumnCountPerMonth = 3;
        private const int ColumnStartIndex = 1;
        private const int RowStartIndex = 1;

        private PdfFormField pdfFormFieldAvailable;
        private PdfFormField pdfFormFieldReceived;
        private PdfFormField pdfFormFieldCalculated;
        private PdfFormField pdfFormFieldPrevious;

        private readonly int rowIndex;
        private readonly int monthIndex;
        private readonly PdfAcroForm acroForm;

        public S28AcroFormField(PdfAcroForm acroForm, PublicationName publicationName, int rowIndex, int monthIndex)
        {
            this.acroForm = acroForm ?? throw new System.ArgumentNullException(nameof(acroForm));
            this.rowIndex = rowIndex;
            this.monthIndex = monthIndex;
            Name = publicationName.Name;
            Description = publicationName.Description;
        }
        public override string Value
        {
            get => AvailableFormField.GetValueAsString();
            set
            {
                AvailableFormField.SetValue(value);
                OnPropertyChanged();
            }
        }
        public override string PreviousValue
        {
            get => PreviousFormField.GetValueAsString();
        }

        public override string ReceivedValue
        {
            get => ReceivedFormField.GetValueAsString();
            set 
            { 
                ReceivedFormField.SetValue(value); 
                OnPropertyChanged(); 
            }
        }
        public override string CalculatedValue => CalculatedFormField.GetValueAsString();

        private PdfFormField AvailableFormField => pdfFormFieldAvailable ??= GetFormField(S28ColumnType.Available);
        private PdfFormField ReceivedFormField => pdfFormFieldReceived ??= GetFormField(S28ColumnType.Recieved);
        private PdfFormField CalculatedFormField => pdfFormFieldCalculated ??= GetFormField(S28ColumnType.Calculated);
        private PdfFormField PreviousFormField => pdfFormFieldPrevious ??= GetPreviousFormField();

        private PdfFormField GetFormField(S28ColumnType columnIndex)
        {
            return acroForm.GetField(GetAcroFormFieldName(columnIndex, monthIndex));
        }
        private PdfFormField GetPreviousFormField()
        {
            return acroForm.GetField(GetPreviousAcroFormFieldName());
        }

        private string GetPreviousAcroFormFieldName()
        {
            return monthIndex == 0 ? GetAcroFormFieldName() : GetAcroFormFieldName(S28ColumnType.Available, monthIndex - 1);
        }

        private string GetAcroFormFieldName()
        {
            return $"901_{GetRelativeRowIndex()}_S28Value";
        }

        private string GetAcroFormFieldName(S28ColumnType columnIndex, int month)
        {
            int s28ColumnIndex = GetRelativeColumnIndex(columnIndex, month);

            return $"9{s28ColumnIndex:00}_{GetRelativeRowIndex()}" + GetColumnValueOrCalcSuffix(columnIndex);
        }
        private int GetRelativeColumnIndex(S28ColumnType columnIndex, int monthIndex)
        {
            return ColumnStartIndex + (int)columnIndex + monthIndex * ColumnCountPerMonth;
        }
        private int GetRelativeRowIndex()
        {
            return RowStartIndex + rowIndex;
        }

        private string GetColumnValueOrCalcSuffix(S28ColumnType columnIndex)
        {
            return columnIndex == S28ColumnType.Calculated ? "_S28Calc" : "_S28Value";
        }
    }
}
