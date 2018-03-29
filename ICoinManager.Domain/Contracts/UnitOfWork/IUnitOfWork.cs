using ICoinManager.Domain.Contracts.Repositories.Business;
using System;


namespace ICoinManager.Domain.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        //IClinicRepository ClinicRepository { get; }
        //ICovenantRepository CovenantRepository { get; }
        //IDoctorRepository DoctorRepository { get; }
        //IMedicalRecordRepository MedicalRecordRepository { get; }
        //IPacientRepository PacientRepository { get; }
        //ISecretaryRepository SecretaryRepository { get; }
        //IUserRepository UserRepository { get; }

        void Commit();
        void Rollback();
    }
}