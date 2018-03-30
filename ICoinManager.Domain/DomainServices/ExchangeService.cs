using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICoinManager.Domain.Entities.Business;
using ICoinManager.Domain.Contracts.Gateways;
using ICoinManager.Domain.Contracts.Repositories.Business;
using ICoinManager.Domain.Contracts.UnitOfWork;

namespace ICoinManager.Domain.DomainServices
{
    public class ExchangeService
    {
        private IExchageGateway _exchageGateway;
        private IUnitOfWork _unitOfWork;


        public ExchangeService(IUnitOfWork unitOfWork, IExchageGateway exchageGateway)
        {
            _unitOfWork = unitOfWork;
            _exchageGateway = exchageGateway;
        }

        public decimal GetCryptoCoinActualValue(Exchange exchange, CryptoCoin cryptoCoin)
        {
            var actualPrice = _exchageGateway.GetCryptoCoinActualPrice(exchange, cryptoCoin);
            return actualPrice;
        }

        public List<CryptoCoin> GetExchangeCryptoCoins(Exchange exchange)
        {
            var coins = _unitOfWork.ExchangeRepository.GetExchangeCryptoCoins(exchange);
            return coins.ToList();
        }
    }
}
