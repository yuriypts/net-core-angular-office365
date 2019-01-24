using DocFlow.BusinessLayer.Helpers;
using System.Threading.Tasks;

namespace DocFlow.BusinessLayer.Interfaces
{
    public interface IAuthentication
    {
        Task<AuthorizationMicrosoftModel> GetAccessToken(string userName, string password);
        Task<string> AuthorizationUser(string userName, string password);
    }
}
