using DocFlow.BusinessLayer.Interfaces;
using DocFlow.BusinessLayer.Models.DigitalSignature;
using DocFlow.Data;
using DocFlow.Data.Entities;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System;
using System.IO;

namespace DocFlow.BusinessLayer.Services
{
    public class DigitalSignature : IDigitalSignature
    {
        private readonly IDocumentService _documentService;
        private readonly IReportService _reportService;

        private readonly string CertificatePath;
        private readonly string CertificatePassword;
        private readonly string ImagePath;

        public DigitalSignature(DigitalSignatureCertificate digitalSignatureCertificate, IDocumentService documentService, IReportService reportService)
        {
            _documentService = documentService;
            _reportService = reportService;

            CertificatePath = string.Concat(Path.Combine(Directory.GetCurrentDirectory(), "Certificates"), $"\\{digitalSignatureCertificate.Certificate}");
            ImagePath = string.Concat(Path.Combine(Directory.GetCurrentDirectory(), "Certificates"), $"\\{digitalSignatureCertificate.ImageForSignature}");
            CertificatePassword = digitalSignatureCertificate.CertificatePassword;
        }

        public byte[] SetDigitalSignature(DigitalSignatureViewModel digitalSignature, int reportId)
        {
            try
            {
                Report report = _reportService.GetReport(reportId);
                byte[] pdfDocumentBytes = _documentService.GetSignedPdfDocument(report, report.SignerUser.FirstName, report.SignerUser.LastName);

                Stream streamCertificate = new FileStream(CertificatePath, FileMode.Open);

                PdfLoadedDocument pdfLoadedDocument = new PdfLoadedDocument(pdfDocumentBytes);
                PdfCertificate pdfCertificate = new PdfCertificate(streamCertificate, CertificatePassword);
                
                FileStream jpgFile = new FileStream(ImagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                PdfBitmap bmp = new PdfBitmap(jpgFile);
                PdfPageBase page = pdfLoadedDocument.Pages[0];

                PdfSignature signature = new PdfSignature(pdfLoadedDocument, page, pdfCertificate, "Signature");
                signature.Bounds = new RectangleF(new PointF(5, 5), page.Size);

                signature.ContactInfo = report.SignerUser.UserName;
                signature.LocationInfo = digitalSignature.Location;
                signature.Reason = digitalSignature.Reason;

                MemoryStream stream = new MemoryStream();

                pdfLoadedDocument.Save(stream);
                pdfLoadedDocument.Close(true);

                return stream.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
