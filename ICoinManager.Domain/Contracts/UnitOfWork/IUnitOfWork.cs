using ICoinManager.Domain.Contracts.Repositories.Business;
using System;


namespace ICoinManager.Domain.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IExchangeRepository ExchangeRepository { get; }
        ICryptoCoinRepository CryptoCoinRepository { get; }

        void Commit();
        void Rollback();
    }
}