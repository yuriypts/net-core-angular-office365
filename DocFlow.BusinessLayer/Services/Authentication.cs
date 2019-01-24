using DocFlow.BusinessLayer.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocFlow.BusinessLayer.Services.ApplicationServices;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using DocFlow.BusinessLayer.Services.AuthorizationServices;
using Newtonsoft.Json;
using DocFlow.BusinessLayer.Helpers;
using DocFlow.Data;
using System.Linq;
using DocFlow.Data.Entities;

namespace DocFlow.BusinessLayer.ImplementInterfaces
{
    public class Authentication : IAuthentication
    {
        private readonly DocFlowCotext _docFlowContext;

        private string postBody = "grant_type=password&scope=openid";
        private const string mediType = "application/x-www-form-urlencoded";
        private readonly string tokenEndpoint;

        private readonly string validIssuer;
        private readonly string validAudience;
        private readonly string issuerSigningKey;

        public Authentication(AzureADService azureADService, JwtService jwtService, DocFlowCotext docFlowContext)
        {
            postBody = postBody + $"&resource={azureADService.Resource}&client_id={azureADService.ClientId}&client_secret={azureADService.AppKey}";
            tokenEndpoint = azureADService.TokenEndpoint;
            validIssuer = jwtService.ValidIssuer;
            validAudience = jwtService.ValidAudience;
            issuerSigningKey = jwtService.IssuerSigningKey;
            _docFlowContext = docFlowContext;
        }

        public async Task<AuthorizationMicrosoftModel> GetAccessToken(string userName, string password)
        {
            postBody = postBody + $"&username={userName}&password={password}";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.PostAsync(tokenEndpoint, new StringContent(postBody, Encoding.UTF8, mediType)))
                    {
                        string result = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            return JsonConvert.DeserializeObject<AuthorizationMicrosoftModel>(result);
                        }

                        return new AuthorizationMicrosoftModel();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        public async Task<string> AuthorizationUser(string userName, string password)
        {
            try
            {
                AuthorizationMicrosoftModel response = await GetAccessToken(userName, password);

                User user = _docFlowContext.Users.FirstOrDefault(u => u.UserName == userName);

                double unixTimeStamp = Convert.ToDouble(response.Expires_On);
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();

                Claim[] claimsdata = new[] {
                    new Claim(ClaimTypes.Email, userName),
                    new Claim(ClaimTypes.Authentication, response.Access_Token),
                    new Claim(ClaimTypes.Actor, user.Id.ToString())
                };

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
                SigningCredentials signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                        issuer: validIssuer,
                        audience: validAudience,
                        expires: dtDateTime,
                        claims: claimsdata,
                        signingCredentials: signInCred
                    );

                string tokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                return tokenString;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
