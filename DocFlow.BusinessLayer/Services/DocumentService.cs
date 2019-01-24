using DocFlow.BusinessLayer.Interfaces;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;
using System.IO;
using DocFlow.BusinessLayer.Models.FlowWordFile;
using System.Collections.Generic;
using DocFlow.Models.FlowWordFile;
using DocFlow.Data;
using System.Linq;
using DocFlow.Data.Entities;
using System;

namespace DocFlow.BusinessLayer.ImplementInterfaces
{
    public class DocumentService : IDocumentService
    {
        private readonly DocFlowCotext _context;

        public DocumentService(
                DocFlowCotext context
            )
        {
            _context = context;
        }

        #region Helpers

        private WordDocument ReplaceUserInitials(WordDocument document, string firstName, string lastName, string currentDate)
        {
            document.Replace("{{firtName}}", firstName, false, true);
            document.Replace("{{lastName}}", lastName, false, true);
            document.Replace("{{signedDate}}", currentDate, false, true);

            return document;
        }

        private WordDocument ReplaceTemplate(WordDocument document, string template, Dictionary<string, string> list)
        {
            try
            {
                string pathDir = string.Concat(Path.Combine(Directory.GetCurrentDirectory(), "Templates"), $"\\{template}");

                Stream stream = new FileStream(pathDir, FileMode.Open);
                document.Open(stream, FormatType.Docx);

                foreach (var item in list)
                {
                    DateTime dateTime;
                    if (DateTime.TryParse(item.Value, out dateTime))
                    {
                        string date = Helpers.Helpers.ConvertToEnUsShortDateFormat(dateTime);
                        document.Replace($"{{{{{item.Key}}}}}", date, false, true);
                    }
                    else
                    {
                        document.Replace($"{{{{{item.Key}}}}}", string.IsNullOrEmpty(item.Value) ? string.Empty : item.Value, false, true);
                    }
                }

                ReplaceUserInitials(document, string.Empty, string.Empty, string.Empty);

                stream.Close();

                return document;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private WordDocument ReplaceSignedTemplate(WordDocument document, string template, Dictionary<string, string> list, string firstName, string lastName)
        {
            string pathDir = string.Concat(Path.Combine(Directory.GetCurrentDirectory(), "Templates"), $"\\{template}");

            Stream stream = new FileStream(pathDir, FileMode.Open);
            document.Open(stream, FormatType.Docx);

            foreach (var item in list)
            {
                DateTime dateTime;
                if (DateTime.TryParse(item.Value, out dateTime))
                {
                    string date = Helpers.Helpers.ConvertToEnUsShortDateFormat(dateTime);
                    document.Replace($"{{{{{item.Key}}}}}", date, false, true);
                }
                else
                {
                    document.Replace($"{{{{{item.Key}}}}}", string.IsNullOrEmpty(item.Value) ? string.Empty : item.Value, false, true);
                }
            }

            document = ReplaceUserInitials(document, firstName, lastName, DateTime.Now.ToShortDateString());

            return document;
        }

        #endregion

        public WordDocument OpenAndReplaceTemplate(ReportValuesViewModel reportValuesViewModel)
        {
            WordDocument document = new WordDocument();

            ReportType reportLabels = _context.ReportTypes.FirstOrDefault(x => x.Id == reportValuesViewModel.ReportTypeId);

            if (reportLabels != null)
            {
                document = ReplaceTemplate(document, reportLabels.Template, reportValuesViewModel.Values.ToDictionary(t => t.Key, t => t.Value));
            }

            return document;
        }

        public byte[] CreateNewFile(WordDocument document)
        {
            byte[] byteArray;

            using (var stream = new MemoryStream())
            {
                document.Save(stream, FormatType.Docx);
                byteArray = stream.ToArray();
            }

            return byteArray;
        }

        public byte[] ConvertToPdf(WordDocument document)
        {
            DocIORenderer render = new DocIORenderer();
            PdfDocument pdfDocument = render.ConvertToPDF(document);
            render.Dispose();
            document.Dispose();

            byte[] byteArray;

            using (var stream = new MemoryStream())
            {
                pdfDocument.Save(stream);
                byteArray = stream.ToArray();
                stream.Dispose();
            }

            pdfDocument.Close();
            document.Close();

            return byteArray;

        }

        public byte[] GetSignedPdfDocument(Report report, string firstName, string lastName)
        {
            WordDocument document = new WordDocument();
            document = ReplaceSignedTemplate(document, report.ReportType.Template, report.Values.ToDictionary(t => t.ReportLabel.Name, t => t.Value), firstName, lastName);
            byte[] documentBytes = ConvertToPdf(document);

            return documentBytes;
        }

        public byte[] GetPdfDocument(Report report)
        {
            WordDocument document = new WordDocument();
            document = ReplaceTemplate(document, report.ReportType.Template, report.Values.ToDictionary(t => t.ReportLabel.Name, t => t.Value));
            byte[] documentBytes = ConvertToPdf(document);

            return documentBytes;
        }

        public byte[] GetWordDocument(Report report)
        {
            WordDocument document = new WordDocument();
            document = ReplaceTemplate(document, report.ReportType.Template, report.Values.ToDictionary(t => t.ReportLabel.Name, t => t.Value));
            byte[] documentBytes = CreateNewFile(document);

            return documentBytes;
        }

        public DocumentBytes GeneretaNewFiles(ReportValuesViewModel reportValuesViewModel)
        {
            WordDocument document = OpenAndReplaceTemplate(reportValuesViewModel);

            byte[] documentBytes = CreateNewFile(document);
            byte[] pdfBytes = ConvertToPdf(document);

            return new DocumentBytes
            {
                DocumentsBytes = documentBytes,
                PdfBytes = pdfBytes
            };
        }
    }
}
