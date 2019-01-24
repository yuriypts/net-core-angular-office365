using DocFlow.BusinessLayer.Interfaces.GraphServices;
using DocFlow.BusinessLayer.Models.FlowWordFile;
using DocFlow.BusinessLayer.Services;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocFlow.BusinessLayer.ImplementInterfaces.GraphServices
{
    public class GraphServiceFiles : GraphService, IGraphServiceFiles
    {
        string ReplaceName(string value)
        {
            return value.Replace(" ", "-").Replace(":", "-").Replace(".", "-");
        }

        public async Task<Drive> GetMyDrive(string token)
        {
            GraphServiceClient graphClient = GetGraphServiceClient(token);
            return await graphClient.Me.Drive.Request().GetAsync();
        }

        public async Task<List<DriveItem>> CreateFilesInDrive(string token, string nameFile, DocumentBytes documentBytes)
        {
            nameFile = ReplaceName(nameFile);

            GraphServiceClient graphClient = GetGraphServiceClient(token);

            List<DriveItem> driveItems = new List<DriveItem>(); 

            using (MemoryStream fileContentStream = new MemoryStream(documentBytes.DocumentsBytes))
            {
                try
                {
                    DriveItem resultSavedDocx = await graphClient.Me.Drive.Root.ItemWithPath(nameFile + ".docx").Content.Request().PutAsync<DriveItem>(fileContentStream);
                    driveItems.Add(resultSavedDocx);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }

            using (MemoryStream fileContentStream = new MemoryStream(documentBytes.PdfBytes))
            {
                try
                {
                    DriveItem resultSavePdf = await graphClient.Me.Drive.Root.ItemWithPath(nameFile + ".pdf").Content.Request().PutAsync<DriveItem>(fileContentStream);
                    driveItems.Add(resultSavePdf);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }

            return driveItems;
        }

        public async Task<IList<ListItem>> GetItemsInSharedDirectory(string token)
        {
            GraphServiceClient graphClient = GetGraphServiceClient(token);

            try
            {
                return await graphClient.Sites["root"].Lists["Documents"].Items.Request().Expand("fields").GetAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public async Task<List<DriveItem>> CreateFilesInSharedDrive(string token, string nameFile, DocumentBytes documentBytes)
        {
            nameFile = ReplaceName(nameFile);

            GraphServiceClient graphClient = GetGraphServiceClient(token);

            List<DriveItem> driveItems = new List<DriveItem>();

            using (MemoryStream fileContentStream = new MemoryStream(documentBytes.DocumentsBytes))
            {
                try
                {
                    DriveItem resultSavedDocx = await graphClient.Drive.Root.ItemWithPath(nameFile + ".docx").Content.Request().PutAsync<DriveItem>(fileContentStream);
                    driveItems.Add(resultSavedDocx);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }

            using (MemoryStream fileContentStream = new MemoryStream(documentBytes.PdfBytes))
            {
                try
                {
                    DriveItem resultSavePdf = await graphClient.Drive.Root.ItemWithPath(nameFile + ".pdf").Content.Request().PutAsync<DriveItem>(fileContentStream);
                    driveItems.Add(resultSavePdf);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }

            return driveItems;
        }

        public async Task<DriveItem> CreateSignedFileInSharedDrive(string token, string nameFile, byte[] pdfBytes)
        {
            nameFile = Helpers.Helpers.GetSignedNameFileWithCurrentDate(nameFile);
            nameFile = ReplaceName(nameFile);

            GraphServiceClient graphClient = GetGraphServiceClient(token);

            DriveItem driveItem;

            using (MemoryStream fileContentStream = new MemoryStream(pdfBytes))
            {
                try
                {
                    driveItem = await graphClient.Drive.Root.ItemWithPath(nameFile + ".pdf").Content.Request().PutAsync<DriveItem>(fileContentStream);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }

            return driveItem;
        }
    }
}
