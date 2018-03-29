using ICoinManager.Domain.Contracts.Repositories.Business;
using ICoinManager.Domain.Entities.Business;
using ICoinManager.Infra.EntityData.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ICoinManager.Infra.EntityData.Context;

namespace ICoinManager.Infra.EntityData.Repositories.Business
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ICoinManagerContext context) : base(context) { }


        public User Authenticate(string login, string password)
        {
            var user = Context.Users.FirstOrDefault(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase) && u.Password.Equals(password));
            return user;
        }

        public IEnumerable<Coin> GetUserFavoriteCoinsByLogin(string login)
        {
            var user = Context.Users.FirstOrDefault(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
            if (user == null)
                return null;

            return user.FavoriteCoins;
        }
    }
}
