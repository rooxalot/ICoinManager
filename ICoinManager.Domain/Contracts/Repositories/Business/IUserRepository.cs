using ICoinManager.Domain.Contracts.Repositories.Base;
using ICoinManager.Domain.Entities.Business;
using System.Collections.Generic;

namespace ICoinManager.Domain.Contracts.Repositories.Business
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User Authenticate(string login, string password);
        IEnumerable<Coin> GetUserFavoriteCoinsByLogin(string login);
    }
}
