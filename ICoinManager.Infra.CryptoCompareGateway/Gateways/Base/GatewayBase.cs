using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICoinManager.Infra.CryptoCompareGateway.Gateways.Base
{
    public class GatewayBase
    {
        public string BaseURL { get { return "https://min-api.cryptocompare.com/data/"; } }
    }
}
