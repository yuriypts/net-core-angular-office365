using DocFlow.BusinessLayer.Models.FlowWordFile;
using DocFlow.Data.Entities;
using DocFlow.Models.FlowWordFile;
using Syncfusion.DocIO.DLS;
using Syncfusion.Pdf;
using System.Collections.Generic;

namespace DocFlow.BusinessLayer.Interfaces
{
    public interface IDocumentService
    {
        WordDocument OpenAndReplaceTemplate(ReportValuesViewModel flowWordFile);
        byte[] CreateNewFile(WordDocument document);
        byte[] ConvertToPdf(WordDocument document);
        DocumentBytes GeneretaNewFiles(ReportValuesViewModel flowWordFile);
        byte[] GetPdfDocument(Report report);
        byte[] GetWordDocument(Report report);
        byte[] GetSignedPdfDocument(Report report, string firstName, string lastName);
    }
}
