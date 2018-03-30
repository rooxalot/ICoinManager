using ICoinManager.Domain.Contracts.Gateways;
using ICoinManager.Infra.CryptoCompareGateway.Gateways.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICoinManager.Domain.Entities.Business;

namespace ICoinManager.Infra.CryptoCompareGateway.Gateways.Business
{
    public class CryptoCoinGateway : GatewayBase, ICryptoCoinGateway
    {
        Dictionary<string, decimal> ICryptoCoinGateway.GetCoinAveragePrice(CryptoCoin coin)
        {
            throw new NotImplementedException();
        }
    }
}
