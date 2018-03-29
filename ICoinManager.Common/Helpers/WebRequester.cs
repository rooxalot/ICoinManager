using ICoinManager.Common.ExtensionMethods;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ICoinManager.Common.Helpers
{
    public class WebRequester
    {
        #region Construtor

        public WebRequester() { }

        public WebRequester(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public WebRequester(string token)
        {
            Token = token;
        }

        #endregion

        #region Propriedades

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Token { get; private set; }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Obtem o client a partir de uma autenticação basica
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private HttpClient GetClient(string username, string password)
        {
            var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
            var client = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue }
            };
            return client;
        }
        /// <summary>
        /// Obtem o client a partir de um token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private HttpClient GetClient(string token)
        {
            var authValue = new AuthenticationHeaderValue("Bearer", token);
            var client = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue }
            };
            return client;
        }
        /// <summary>
        /// Obtem o client mais adequado baseando-se nas propriedades do objeto
        /// </summary>
        /// <returns></returns>
        private HttpClient GetClient()
        {
            if (!string.IsNullOrEmpty(this.Token))
            {
                return GetClient(this.Token);
            }
            else if (!string.IsNullOrEmpty(this.Username) && !string.IsNullOrEmpty(this.Password))
            {
                return GetClient(this.Username, this.Password);
            }
            else
            {
                return new HttpClient();
            }
        }
        /// <summary>
        /// Obtem a url tratada com todos os seus parametros adicionados
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string TreatUrlParameters(string url, params KeyValuePair<string, string>[] parameters)
        {
            if (parameters.Length == 0)
                return url;

            var stringBuilder = new StringBuilder("?");
            var uriBuilder = new UriBuilder(url);

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                stringBuilder.AppendFormat("{0}{1}={2}", (i > 0 ? "&" : ""), parameter.Key, parameter.Value);
            }

            uriBuilder.Query = stringBuilder.ToString();
            return uriBuilder.ToString();
        }

        #endregion

        #region Métodos Publicos

        /// <summary>
        /// Obtem um Token baseando-se no usuario e senha da instância e caso obtenha sucesso, configura a instância com esse token e o retorna;
        /// </summary>
        /// <param name="tokenUrl">Url a se obter o token</param>
        /// <returns>token obtido na requisição</returns>
        public async Task<string> GetToken(string tokenUrl)
        {
            using (var client = new HttpClient())
            {
                var token = string.Empty;
                var contentInfo = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", this.Username),
                    new KeyValuePair<string, string>("password", this.Password),
                };

                var content = new FormUrlEncodedContent(contentInfo);
                var response = await client.PostAsync(tokenUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var obj = new { access_token = string.Empty, token_type = string.Empty, expires_in = 0d };
                        obj = JsonConvert.DeserializeAnonymousType(contentResponse, obj);

                        token = obj.access_token;
                    }
                    catch (Exception) { }

                }

                this.Token = token;
                return token;
            }
        }

        /// <summary>
        /// Realiza uma operação HTTP GET para uma determinada URL
        /// </summary>
        /// <param name="url">URL a ser acessada</param>
        /// <param name="urlParameters">Parâmetros a serem enviados na QueryString</param>
        /// <returns>Objeto HTTPResponseMessage comas informações retornadas.</returns>
        public async Task<HttpResponseMessage> Get(string url, params KeyValuePair<string, string>[] urlParameters)
        {
            using (var client = GetClient())
            {
                var newUrl = TreatUrlParameters(url, urlParameters);
                var response = await client.GetAsync(url);

                return response;
            }
        }
        /// <summary>
        /// Realiza uma operação HTTP GET para uma determinada URL
        /// </summary>
        /// <param name="url">URL a ser acessada</param>
        /// <param name="urlParameters">Parâmetros a serem enviados na QueryString</param>
        /// <returns>Objeto Tipado retornado pela operação de GET</returns>
        public async Task<T> Get<T>(string url, params KeyValuePair<string, string>[] urlParameters) where T : class
        {
            var response = await Get(url, urlParameters);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                var objectResponse = JsonConvert.DeserializeObject<T>(stringResponse);

                return objectResponse;
            }

            return default(T);
        }

        /// <summary>
        /// Realiza uma operação HTTP POST para uma determinada URL
        /// </summary>
        /// <param name="url">URL a ser acessada</param>
        /// <param name="postObject">Objeto a ser enviado via POST</param>
        /// <returns>Objeto HTTPResponseMessage comas informações retornadas.</returns>
        public async Task<HttpResponseMessage> Post(string url, object postObject)
        {
            using (var client = GetClient())
            {
                var json = postObject.ToJson();
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                return response;
            }
        }
        /// <summary>
        /// Realiza uma operação HTTP POST para uma determinada URL
        /// </summary>
        /// <param name="url">URL a ser acessada</param>
        /// <param name="postObject">Objeto a ser enviado via POST</param>
        /// <returns>Objeto Tipado retornado pela operação de POST</returns>
        public async Task<T> Post<T>(string url, object postObject) where T : class
        {
            var response = await Post(url, postObject);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                var objectResponse = JsonConvert.DeserializeObject<T>(stringResponse);

                return objectResponse;
            }

            return default(T);
        }

        #endregion
    }
}
