using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using iText.Forms;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using S28Maker.Error;
using S28Maker.Models;

namespace S28Maker.Services
{
    public interface IS28Document
    {
        IReadOnlyCollection<PublicationName> PublicationRows { get; }
    }

    public class S28PdfDocument : S28Document
    {
        private PdfDocument pdfDocument;

        public static string TmpFileName { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "S-28.pdf.tmp");
        public static string NewFileName { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "S-28-new.pdf");

        public static string FileName { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "S-28.pdf");

        private PdfAcroForm AcroForm { get; set; }

        public static S28PdfDocument TryLoadLocalPdfDocument()
        {
            try
            {
                if (File.Exists(FileName))
                    return LoadLocalPdfDocument();
            }
            catch (Exception ex)
            {
                S28ErrorHandler.Current.ShowError(ex);
            }
            return null;
        }

        private static S28PdfDocument LoadLocalPdfDocument()
        {
            try
            {
                var document = new S28PdfDocument();
                document.LoadAndParse();
                return document;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw new S28Exception("Error to load PDF file.", ex);
            }
        }

        private void LoadAndParse()
        {
            pdfDocument = new PdfDocument(new PdfReader(FileName), new PdfWriter(NewFileName));
            Parse();
            AcroForm = PdfAcroForm.GetAcroForm(pdfDocument, false);
        }

        private void Parse()
        {
            var pdfText = PdfTextExtractor.GetTextFromPage(pdfDocument.GetFirstPage()) + "\n" + PdfTextExtractor.GetTextFromPage(pdfDocument.GetLastPage());

            PublicationRows = S28PdfTextExtractor.ExtractNames(pdfText);

            Monthes = new List<IS28MonthColumn>(CreateMonthItemsForAllColumns());
        }

        private IList<IS28MonthColumn> CreateMonthItemsForAllColumns()
        {
            var months = new List<IS28MonthColumn>();
            months.AddRange(MonthRenderer.MonthNames.Select((name, monthColumnIndex) => new S28MonthColumn(name, CreateRowsForMonth(monthColumnIndex))));

            return months;
        }

        private IEnumerable<IS28FieldRow> CreateRowsForMonth(int monthColumnIndex)
        {
            return PublicationRows.Select((pubName, rowIndex) => GetFormField(pubName, rowIndex, monthColumnIndex));
        }

        private IS28FieldRow GetFormField(PublicationName pubName, int rowIndex, int monthColumnIndex)
        {
            return new S28AcroFormField(AcroForm, pubName, rowIndex, monthColumnIndex);
        }

        public static void SavePdfLocally(Stream stream)
        {

            try
            {
                using (var fileStream = new FileStream(TmpFileName, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(fileStream);
                }
                Current = null;

                File.Delete(FileName);
                File.Move(TmpFileName, FileName);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        public override void Close()
        {
            if (pdfDocument == null) return;
            pdfDocument.Close();

            if (File.Exists(NewFileName))
            {
                File.Delete(FileName);
                File.Move(NewFileName, FileName);
            }

            Current = null;
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
