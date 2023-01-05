using System;
using ProductManager.Domain.Repositories.Contracts;
using ProductManager.Infra.DBContext;

namespace ProductManager.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
        }

        private readonly ProductManagerDBContext context;

        public UnitOfWork(ProductManagerDBContext context)
        {
            this.context = context;
        }
        public void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
            //
        }
    }
}

