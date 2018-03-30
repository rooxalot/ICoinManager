using ICoinManager.Domain.Base.Entities;
using System.Collections.Generic;

namespace ICoinManager.Domain.Entities.Business
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<CryptoCoin> FavoriteCoins { get; set; }
    }
}
