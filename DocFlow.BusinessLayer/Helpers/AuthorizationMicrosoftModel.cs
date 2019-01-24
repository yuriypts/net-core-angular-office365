namespace DocFlow.BusinessLayer.Helpers
{
    public class AuthorizationMicrosoftModel
    {
        public string Token_Type { get; set; }
        public string Scope { get; set; }
        public string Expires_In { get; set; }
        public string Ext_Expires_In { get; set; }
        public string Expires_On { get; set; }
        public string Not_Before { get; set; }
        public string Resource { get; set; }
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
    }
}
