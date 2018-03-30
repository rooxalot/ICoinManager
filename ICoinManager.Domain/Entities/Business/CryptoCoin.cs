using ICoinManager.Domain.Base.Entities;

namespace ICoinManager.Domain.Entities.Business
{
    public class CryptoCoin : BaseEntity
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
    }
}
