using ICoinManager.Domain.Base.Entities;
using ICoinManager.Domain.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICoinManager.Domain.Busiess.Entities
{
    public class Exchange : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Coin> Coins { get; set; }
    }
}
