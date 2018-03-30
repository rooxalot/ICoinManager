using ICoinManager.Domain.Contracts.Repositories.Business;
using ICoinManager.Domain.Entities.Business;
using ICoinManager.Infra.EntityData.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICoinManager.Infra.EntityData.Context;

namespace ICoinManager.Infra.EntityData.Repositories.Business
{
    public class ExchangeRepository : RepositoryBase<Exchange>, IExchangeRepository
    {
        public ExchangeRepository(ICoinManagerContext context) : base(context)
        {
        }


        public IEnumerable<CryptoCoin> GetExchangeCryptoCoins(Exchange exchange)
        {
            var recoveredExchange = Context.Exchanges
                .Include("CryptoCoins")
                .FirstOrDefault(e => e.ID == exchange.ID);

            return recoveredExchange.Coins;
        }
    }
}
