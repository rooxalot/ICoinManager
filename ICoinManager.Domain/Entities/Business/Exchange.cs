﻿using ICoinManager.Domain.Base.Entities;
using System.Collections.Generic;

namespace ICoinManager.Domain.Entities.Business
{
    public class Exchange : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<CryptoCoin> Coins { get; set; }
    }
}
