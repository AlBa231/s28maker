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
        private static readonly Regex FormValueRegex = new Regex(@"9(\d+)_(\d+)_S28Value");
        private PdfDocument _pdfDocument;
        //private int _month = DateTime.Today.AddMonths(-1).Month;

        private static S28Document _instance;

        public static S28Document Current => _instance ??= LoadLocally();

        public List<Item> Items { get; private set; }

        public static string TmpFileName { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "S-28.pdf.tmp");
        public static string NewFileName { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "S-28-new.pdf");

        public static string FileName { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "S-28.pdf");

        private PdfAcroForm _acroForm;

        private PdfAcroForm AcroForm => _acroForm ??= (_pdfDocument != null ? PdfAcroForm.GetAcroForm(_pdfDocument, false) : null);

        //public int Month
        //{
        //    get => _month;
        //    set
        //    {
        //        if (value == _month) return;
        //        _month = value;
        //        if (Items == null) return;

        //        var monthIdx = 2 + (MonthRenderer.GetMonthPos(_month) * 3);

        //        //Items.ForEach(i=>i.Value = Month.ToString());
        //        //Items.ForEach(i=>i.Value = i.RowValues[monthIdx]?.GetValueAsString());
        //    }
        //}

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

        public void Parse()
        {
            //PdfDocument doc = new PdfDocument(stream);
            //var page = doc.Pages[0];
            // doc.AcroForm.Fields.Names

            var txt = PdfTextExtractor.GetTextFromPage(_pdfDocument.GetFirstPage()) + "\n" + PdfTextExtractor.GetTextFromPage(_pdfDocument.GetLastPage());

            var lines = BookNameRegex.Matches(txt);

            Items = lines.OfType<Match>().Select((m, idx) => new Item {Text = m.Groups[1].Value, Id = idx, Description = m.Value }).ToList();
            
            //var acroForms = PdfAcroForm.GetAcroForm(_pdfDocument, false).GetFormFields();
            //foreach (var pdfFormField in acroForms)
            //{
            //    var match = FormValueRegex.Match(pdfFormField.Key);
            //    if (match.Success)
            //    {
            //        try
            //        {
            //            var rowIdx = Convert.ToInt32(match.Groups[2].Value) - 1;
            //            var cellIdx = Convert.ToInt32(match.Groups[1].Value) - 1;
            //            Items[rowIdx].RowValues[cellIdx] = pdfFormField.Value;
            //        }
            //        catch (Exception e)
            //        {
            //            Debug.WriteLine(e);
            //        }
            //    }
            //}
            
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
