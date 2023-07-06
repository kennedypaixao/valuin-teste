using Edesoft.ERP.Hangfire.Models.Hotmart;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Edesoft.ERP.Hangfire.Service.Hotmart
{
    public static class HotmartService
    {
        #region Consultas

        public static ResponseCredentialsGrant ObterToken()
        {
            try
            {
                string clienteId = WebConfigurationManager.AppSettings["hotmart:ClienteID"];
                string clientSecret = WebConfigurationManager.AppSettings["hotmart:ClientSecret"];
                string basic = WebConfigurationManager.AppSettings["hotmart:Basic"];
                string endPointAutohorization = WebConfigurationManager.AppSettings["hotmart:BaseUrl"];

                RestClient client = new RestClient(endPointAutohorization);
                RestRequest request = new RestRequest("security/oauth/token", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", $"Basic {basic}");
                request.AddQueryParameter("grant_type", "client_credentials");
                request.AddQueryParameter("client_id", clienteId);
                request.AddQueryParameter("client_secret", clientSecret);

                ResponseCredentialsGrant responseCredentials = null;
                RestResponse response = client.Execute(request);
                responseCredentials = JsonConvert.DeserializeObject<ResponseCredentialsGrant>(response.Content);

                return responseCredentials;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<ResponseSubscriptions> ListarSubscriptionsAsync(List<string> listaStatus)
        {
            int timeOut = int.Parse(WebConfigurationManager.AppSettings["hotmart:TimeOut"]);
            string endPointPayments = WebConfigurationManager.AppSettings["hotmart:BaseUrlPayments"];
            var url = $"{endPointPayments}subscriptions?status={string.Join(",", listaStatus)}";

            string token = ObterToken().AccessToken;
            var request = JsonConvert.SerializeObject("");

            using (var httpClientHandler = new HttpClientHandler())
            {
                using (var httpClient = new HttpClient(httpClientHandler, false))
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(timeOut);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    var content = new StringContent(request, Encoding.UTF8);
                    content.Headers.Remove("Content-Type");
                    content.Headers.Add("Content-Type", "application/json");

                    HttpResponseMessage response;
                    ResponseSubscriptions responseSubscriptions;

                    try
                    {
                        response = await httpClient.GetAsync(url);

                        var conteudo = await response.Content.ReadAsStringAsync();
                        responseSubscriptions = JsonConvert.DeserializeObject<ResponseSubscriptions>(conteudo);

                        return responseSubscriptions;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }
        #endregion
    }
}