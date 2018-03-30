using ICoinManager.Domain.Contracts.Gateways;
using ICoinManager.Infra.CryptoCompareGateway.Gateways.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICoinManager.Domain.Entities.Business;
using ICoinManager.Common.WebRequester;
using Newtonsoft.Json;

namespace ICoinManager.Infra.CryptoCompareGateway.Gateways.Business
{
    public class ExchangeGateway : GatewayBase, IExchageGateway
    {
        private WebRequester _requester;


        public ExchangeGateway(WebRequester requester)
        {
            _requester = requester;
        }

        public decimal GetCryptoCoinActualPrice(Exchange exchange, CryptoCoin coin, string currencySymbol)
        {
            var url = BaseURL + "price";
            var parameters = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("fsym", coin.Symbol),
                new KeyValuePair<string, string>("tsyms", currencySymbol),
                new KeyValuePair<string, string>("e", exchange.Name)
            };

            var response = _requester.Get(url, parameters).GetAwaiter().GetResult();
            var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var dcRiesponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

            if (dcRiesponse.ContainsKey(currencySymbol))
                return Convert.ToDecimal(dcRiesponse[currencySymbol]);
            else
                return 0;
        }

    }
}
