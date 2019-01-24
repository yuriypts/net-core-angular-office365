using Microsoft.Graph;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DocFlow.BusinessLayer.Services
{
    public class GraphService
    {
        public GraphServiceClient GetGraphServiceClient(string token)
        {
            return new GraphServiceClient(
                new DelegateAuthenticationProvider(
                    (requestMessage) =>
                    {
                        requestMessage.Headers.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);

                        return Task.FromResult(0);
                    }));
        }
    }
}
