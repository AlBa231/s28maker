using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using S28Maker.Models;
using Xamarin.Essentials;

namespace S28Maker.Services
{
    public class S28Document
    {
        private static readonly Regex BookNameRegex = new Regex(@"\d{4}\s(.+)");
        private PdfDocument _pdfDocument;

        private static S28Document _instance;

        public static S28Document Current => _instance ??= LoadLocally();

        public List<Item> Items { get; private set; }

        public static string TmpFileName { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "S-28.pdf.tmp");
        public static string NewFileName { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "S-28-new.pdf");

        public static string FileName { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "S-28.pdf");

        private PdfAcroForm _acroForm;

        private PdfAcroForm AcroForm => _acroForm ??= (_pdfDocument != null ? PdfAcroForm.GetAcroForm(_pdfDocument, false) : null);
        
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

        private static S28Document LoadLocally()
        {
            if (!File.Exists(FileName)) return null;
            try
            {
                S28MonthItem.ResetCache();
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

        public PdfFormField GetFormField(int row, int col)
        {
            return AcroForm?.GetField($"9{col + 1:00}_{row + 1}_S28Value");
        }
        public PdfFormField GetCalcFormField(int row, int col)
        {
            return AcroForm?.GetField($"9{col + 1:00}_{row + 1}_S28Calc");
        }

        public void Parse()
        {
            var txt = PdfTextExtractor.GetTextFromPage(_pdfDocument.GetFirstPage()) + "\n" + PdfTextExtractor.GetTextFromPage(_pdfDocument.GetLastPage());
            var lines = BookNameRegex.Matches(txt);

            Items = lines.OfType<Match>().Select((m, idx) => new Item {Text = m.Groups[1].Value, Id = idx, Description = m.Value }).ToList();
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
}
