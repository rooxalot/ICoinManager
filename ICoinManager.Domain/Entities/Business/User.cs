using ICoinManager.Domain.Base.Entities;
using ICoinManager.Domain.Busiess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICoinManager.Domain.Entities.Business
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Coin> FavoriteCoins { get; set; }
    }
}
