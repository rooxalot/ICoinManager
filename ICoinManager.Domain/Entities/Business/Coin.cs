using ICoinManager.Domain.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICoinManager.Domain.Entities.Business
{
    public class Coin : BaseEntity
    {
        public string Name { get; set; }
    }
}
