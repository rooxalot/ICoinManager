using ICoinManager.Domain.Entities;
using ICoinManager.Infra.ExternalAccess.WebRequests;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace ICoinManager.Spammer
{
    class Program
    {
        static void Main(string[] args)
        {
            //const string exchangesUrl = "https://min-api.cryptocompare.com/data/all/exchanges";
            //var requester = new WebRequester();
            //var response = requester.Get(exchangesUrl)
            //    .GetAwaiter()
            //    .GetResult();

            const string appHostUrl = "http://localhost:23520/";

            var requester = new WebRequester("Fulano", "1234");

            var tokenResponse = requester.GetToken(appHostUrl + "token")
                .GetAwaiter()
                .GetResult();

            var testList = requester.Get<List<string>>(appHostUrl + "api/Test/")
                .GetAwaiter()
                .GetResult();
        }
    }
}
