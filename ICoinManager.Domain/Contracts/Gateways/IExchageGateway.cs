using ICoinManager.Domain.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICoinManager.Domain.Contracts.Gateways
{
    public interface IExchageGateway
    {
        decimal GetCryptoCoinActualPrice(Exchange exchange, CryptoCoin coin, string currencySymbol);
    }
}
