using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ProductManager.Domain.Commons.ObjectValues;
using ProductManager.Domain.Entities;

namespace ProductManager.Infra.DBContext
{
    public class ProviderMappings : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.CNPJ)
                .IsRequired()
                .HasConversion<string>( v => v.Value, v => new CNPJ(v))
                .HasColumnType("varchar(14)");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(180)");

            builder.HasMany(p => p.Products)
                 .WithOne(prod => prod.Provider)
                 .HasForeignKey(prod => prod.ProviderId);
        }
    }

    public class ProductMappings : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(180)");

            builder.HasOne(p => p.Provider)
                .WithMany(p => p.Products);

        }
    }

}

