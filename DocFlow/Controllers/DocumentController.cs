using System;
using Microsoft.AspNetCore.Mvc;
using DocFlow.Models.FlowWordFile;
using DocFlow.BusinessLayer.Interfaces;
using Microsoft.Graph;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DocFlow.Data;
using DocFlow.BusinessLayer.Interfaces.GraphServices;
using DocFlow.BusinessLayer.Models.FlowWordFile;
using System.Collections.Generic;
using DocFlow.BusinessLayer.Models.DigitalSignature;

namespace DocFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    abstract class ASd
    {
        public abstract void Method();
    }

    class A : ASd
    {

    }

    public class DocumentController : BaseController
    {
        public readonly IDocumentService _documentService;
        public readonly IGraphServiceFiles _graphServiceFiles;
        private readonly IReportService _reportService;
        private readonly IDigitalSignature _digitalSignature;

        public DocumentController(
                DocFlowCotext context,
                IDocumentService documentService,
                IGraphServiceFiles graphServiceFiles,
                IReportService reportService,
                IDigitalSignature digitalSignature
            ) : base(context)
        {
            _reportService = reportService;
            _documentService = documentService;
            _graphServiceFiles = graphServiceFiles;
            _digitalSignature = digitalSignature;
        }
        
        [HttpGet("drive")]
        public async Task<ActionResult> GetDriveUser()
        {
            try
            {
                Drive drive = await _graphServiceFiles.GetMyDrive(Token);

                return Json(drive);
            }
            catch (ServiceException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost("createFiles")]
        public async Task<ActionResult> CreateFiles([FromBody] ReportValuesViewModel reportValuesViewModel)
        {
            try
            {
                DocumentBytes bytes = _documentService.GeneretaNewFiles(reportValuesViewModel);
                List<DriveItem> drive = await _graphServiceFiles.CreateFilesInDrive(Token, reportValuesViewModel.Name, bytes);

                return Json(drive);
            }
            catch (ServiceException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("download")]
        public ActionResult DownloadReport(int reportId)
        {
            try
            {
                var item = _reportService.GetReport(reportId);
                var document = _documentService.GetPdfDocument(item);
                return File(document, "application/pdf");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [HttpPost("createFilesInSharedDrive")]
        public async Task<ActionResult> CreateFilesInSharedDrive([FromBody] ReportValuesViewModel reportValuesViewModel)
        {
            try
            {
                DocumentBytes bytes = _documentService.GeneretaNewFiles(reportValuesViewModel);
                List<DriveItem> drive = await _graphServiceFiles.CreateFilesInSharedDrive(Token, reportValuesViewModel.Name, bytes);

                return Json(drive);
            }
            catch (ServiceException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getItemsInSharedDirectory")]
        public async Task<ActionResult> GetItemsInSharedDirectory()
        {
            try
            {
                IList<ListItem> result = await _graphServiceFiles.GetItemsInSharedDirectory(Token);

                return Json(result);
            }
            catch (ServiceException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost("digitalSignatureReport")]
        public async Task<ActionResult> DigitalSignatureReport(int reportId, [FromBody]DigitalSignatureViewModel digitalSignatureViewModel)
        {
            try
            {
                var report = _reportService.GetReport(reportId);

                byte[] signedPdfDocument = _digitalSignature.SetDigitalSignature(digitalSignatureViewModel, reportId);

                await _reportService.SignedReport(reportId);
                await _graphServiceFiles.CreateSignedFileInSharedDrive(Token, report.ReportType.Name, signedPdfDocument);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}