using DocFlow.BusinessLayer.Models.FlowWordFile;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocFlow.BusinessLayer.Interfaces.GraphServices
{
    public interface IGraphServiceFiles
    {
        Task<Drive> GetMyDrive(string token);
        Task<List<DriveItem>> CreateFilesInDrive(string token, string nameFile, DocumentBytes documentBytes);
        Task<IList<ListItem>> GetItemsInSharedDirectory(string token);
        Task<List<DriveItem>> CreateFilesInSharedDrive(string token, string nameFile, DocumentBytes documentBytes);
        Task<DriveItem> CreateSignedFileInSharedDrive(string token, string nameFile, byte[] pdfBytes);
    }
}
