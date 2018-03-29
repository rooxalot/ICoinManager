using ICoinManager.Domain.Contracts.UnitOfWork;
using ICoinManager.Infra.EntityData.Context;
using System;
using System.Data.Entity;
using ICoinManager.Domain.Contracts.Repositories.Business;
using ICoinManager.Infra.EntityData.Repositories.Business;

namespace ICoinManager.Infra.EntityData.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Construtor

        public UnitOfWork(ICoinManagerContext context)
        {
            _context = context;

            _userRepository = new UserRepository(context);
        }

        #endregion

        #region Variáveis Privadas

        private readonly ICoinManagerContext _context;
        private readonly IUserRepository _userRepository;
        private bool _isDisposed;

        #endregion

        #region Propriedades

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository;
            }
        }

        #endregion

        #region Métodos

        public void Commit()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            _context.SaveChanges();
        }

        public void Rollback()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Unchanged;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            _isDisposed = true;
        }

        #endregion
    }
}