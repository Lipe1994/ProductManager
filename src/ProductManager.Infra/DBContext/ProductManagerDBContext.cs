using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductManager.Domain.Entities;

namespace ProductManager.Infra.DBContext
{
    public class ProductManagerDBContext : DbContext
    {

        public ProductManagerDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //procura os arquivos de mapeamento
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductManagerDBContext).Assembly);


            //procura todos os campos string sem definição no mappings e seta varchar(100) 
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");


            //não deixa o entity fazer delete em cascata
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.Entity<Fornecedor>().HasData(Seeds.ListaDeFornecedores());
            modelBuilder.Entity<Produto>().HasData(Seeds.ListaDeProdutos());

            base.OnModelCreating(modelBuilder);
        }


    }
}

