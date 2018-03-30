using ICoinManager.Domain.Contracts.Repositories.Base;
using ICoinManager.Domain.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICoinManager.Domain.Contracts.Repositories.Business
{
    public interface IExchangeRepository : IRepositoryBase<Exchange>
    {
        IEnumerable<CryptoCoin> GetExchangeCryptoCoins(Exchange exchange);
    }
}
