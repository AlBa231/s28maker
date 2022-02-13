using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using iText.Forms;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using S28Maker.Models;

namespace S28Maker.Services
{
    public interface IS28Document
    {
        IReadOnlyCollection<PublicationName> PublicationRows { get; }
    }

    public class S28Document: IS28Document
    {
        private static readonly Regex BookNameRegex = new Regex(@"\d{4}\s(.+)");
        private PdfDocument _pdfDocument;

        private static S28Document _instance;

        public static S28Document Current => _instance ??= LoadLocally();

        public IReadOnlyCollection<PublicationName> PublicationRows { get; private set; }

        public static string TmpFileName { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "S-28.pdf.tmp");
        public static string NewFileName { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "S-28-new.pdf");

        public static string FileName { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "S-28.pdf");

        private PdfAcroForm _acroForm;

        private PdfAcroForm AcroForm => _acroForm ??= (_pdfDocument != null ? PdfAcroForm.GetAcroForm(_pdfDocument, false) : null);

        private List<IS28MonthColumn> monthes;

        public IS28MonthColumn CurrentMonth => monthes[MonthRenderer.GetMonthPos(MonthNumberBeforeCurrent)];
        private int MonthNumberBeforeCurrent => DateTime.Today.AddMonths(-1).Month;

        public IList<IS28MonthColumn> Monthes => monthes;

        public static void SavePdfLocally(Stream stream)
        {

            try
            {
                using (var fileStream = new FileStream(TmpFileName, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(fileStream);
                }
                _instance = null;

                File.Delete(FileName);
                File.Move(TmpFileName, FileName);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        public void Parse()
        {
            var txt = PdfTextExtractor.GetTextFromPage(_pdfDocument.GetFirstPage()) + "\n" + PdfTextExtractor.GetTextFromPage(_pdfDocument.GetLastPage());
            var lines = BookNameRegex.Matches(txt);

            monthes = new List<IS28MonthColumn>(CreateMonthItemsForAllColumns());

            PublicationRows = lines.OfType<Match>().Select((m, idx) => new PublicationName(m.Groups[1].Value, m.Value)).ToList();
        }

        public IS28MonthColumn GetMonth(int pos)
        {
            return monthes[pos];
        }

        public IList<IS28MonthColumn> CreateMonthItemsForAllColumns()
        {
            var months = new List<IS28MonthColumn>();
            months.AddRange(MonthRenderer.MonthNames.Select((name, monthColumnIndex) => new S28MonthColumn(name, CreateRowsForMonth(monthColumnIndex))));

            return months;
        }

        private IEnumerable<IS28FieldRow> CreateRowsForMonth(int monthColumnIndex)
        {
            return PublicationRows.Select((pubName, rowIndex)=> GetFormField(pubName, rowIndex, monthColumnIndex));
        }

        private IS28FieldRow GetFormField(PublicationName pubName, int rowIndex, int monthColumnIndex)
        {
            return new S28AcroFormField(AcroForm, pubName, rowIndex, monthColumnIndex);
        }

        private static S28Document LoadLocally()
        {
            if (!File.Exists(FileName)) return null;
            try
            {
                var doc = new S28Document {_pdfDocument = new PdfDocument(new PdfReader(FileName), new PdfWriter(NewFileName))};
                doc.Parse();

                return doc;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }
        public void Close()
        {
            if (_pdfDocument == null) return;
            _pdfDocument.Close();

            if (File.Exists(NewFileName))
            {
                File.Delete(FileName);
                File.Move(NewFileName, FileName);
            }

            _instance = null;
        }
    }

    public class PublicationName
    {
        public string Name { get; }
        public string Description { get; }

        public PublicationName(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
