
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICoinManager.Domain.Entities.Business;
using ICoinManager.Domain.Contracts.Gateways;

namespace ICoinManager.Domain.DomainServices
{
    public class CryptoCoinService
    {
        private ICryptoCoinGateway _cryptoCoinGateway;


        public CryptoCoinService(ICryptoCoinGateway cryptoCoinGateway)
        {
            _cryptoCoinGateway = cryptoCoinGateway;
        }


        public decimal GetCryptoCoinAvarageValue(CryptoCoin cryptoCoin, string currency)
        {
            var dicCoinAveragePrice = _cryptoCoinGateway.GetCoinAveragePrice(cryptoCoin);
            if (dicCoinAveragePrice.Count == 0)
                return 0;

            if (!dicCoinAveragePrice.ContainsKey(currency))
                return 0;

            return dicCoinAveragePrice[currency];
        }
    }
}
