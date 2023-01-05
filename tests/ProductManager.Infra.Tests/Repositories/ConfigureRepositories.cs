using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using ProductManager.Domain.Commons.Helpers;
using ProductManager.Domain.Commons.ObjectValues;
using ProductManager.Domain.Entities;
using ProductManager.Infra.DBContext;
using ProductManager.Infra.Repositories;

namespace ProductManager.Infra.Tests.Repositories
{
    public class ConfigureRepositories
    {
        public ConfigureRepositories()
        {
        }

        public static ProductManagerDBContext Context { get; set; }



        public static ProductManagerDBContext Configure()
        {
            if (Context == null)
            {
                var options = new DbContextOptionsBuilder<ProductManagerDBContext>()
                    .UseInMemoryDatabase(databaseName: "FakeDatabase")
                    .Options;

                Context = Context ?? new ProductManagerDBContext(options);
            }

            return Context;
        }

        public static void InitialInserts(List<Fornecedor> providers, List<Produto> products)
        {
            var uow = new UnitOfWork(Context);


            Context.Fornecedores.AddRange(providers);
            Context.Produtos.AddRange(products);

            uow.Commit();
        }

        public static void ClearDatabase(List<Fornecedor> providers, List<Produto> products)
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();

            Context = null;
            Configure();
        }
    }
}

