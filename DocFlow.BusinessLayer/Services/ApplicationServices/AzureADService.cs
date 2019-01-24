namespace DocFlow.BusinessLayer.Services.ApplicationServices
{
    public class AzureADService
    {
        public string ClientId { get; set; }
        public string Resource { get; set; }
        public string AppKey { get; set; }
        public string TokenEndpoint { get; set; }
    }
}
