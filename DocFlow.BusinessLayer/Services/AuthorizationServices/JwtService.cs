namespace DocFlow.BusinessLayer.Services.AuthorizationServices
{
    public class JwtService
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string IssuerSigningKey { get; set; }
    }
}
