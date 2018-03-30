using ICoinManager.Domain.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ICoinManager.Domain.Contracts.WebRequests
{
    public interface IWebRequester
    {
        Task<string> GetToken(string tokenUrl);

        Task<HttpResponseMessage> Get(string url, params KeyValuePair<string, string>[] urlParameters);
        Task<T> Get<T>(string url, params KeyValuePair<string, string>[] urlParameters) where T : class;

        Task<HttpResponseMessage> Post(string url, object postObject);
        Task<T> Post<T>(string url, object postObject) where T : class;
    }
}
