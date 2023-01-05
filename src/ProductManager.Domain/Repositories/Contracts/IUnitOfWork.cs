using System;
namespace ProductManager.Domain.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        public void Commit();
        public void Rollback();
    }
}

