using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICoinManager.Domain.Entities.Business;
using ICoinManager.Domain.Contracts.Repositories.Business;
using ICoinManager.Domain.Contracts.UnitOfWork;

namespace ICoinManager.Domain.DomainServices
{
    public class UserService
    {
        private IUnitOfWork _uow;


        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public void MarkCryptoCoinAsFavorite(User user, CryptoCoin cryptoCoin)
        {
            var isCoinAlreadyMarked = user.FavoriteCoins.Any(c => c.Name.Equals(cryptoCoin.Name));
            if (!isCoinAlreadyMarked)
            {
                using (_uow)
                {
                    user.FavoriteCoins.Add(cryptoCoin);
                    _uow.UserRepository.SaveOrAdd(user);

                    _uow.Commit();
                }
            }
        }
    }
}
